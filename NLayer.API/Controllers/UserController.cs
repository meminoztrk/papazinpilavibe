using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;
using NLayer.Core.DTOs.UserDTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;
using NLayer.Service.Services;

namespace NLayer.API.Controllers
{
    public class UserController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly JwtService _jwtService;

        public UserController(IMapper mapper, IUserService userService, JwtService jwtService)
        {
            _mapper = mapper;
            _userService = userService;
            _jwtService = jwtService;
        }

        [HttpPost("gSign")]
        public async Task<IActionResult> gSign(UserGoogleRegisterDto userGoogleRegisterDto)
        {
            if(!_userService.UniqueEmail(userGoogleRegisterDto.Email))
            {
                var user = _userService.GetByUsername(userGoogleRegisterDto.Email);

                if (user == null) return BadRequest(new { message = "Invalid Credentials" });

                if (!BCrypt.Net.BCrypt.Verify(userGoogleRegisterDto.GoogleCredential, user.GoogleCredential))
                {
                    return BadRequest(new { message = "Invalid Credentials" });
                }

                var userWithTokenLogin = _mapper.Map<UserWithTokenDto>(user);
                userWithTokenLogin.Token = _jwtService.Generate(userWithTokenLogin.Id);

                return CreateActionResult(CustomResponseDto<UserWithTokenDto>.Success(201, userWithTokenLogin));
            }

            userGoogleRegisterDto.GoogleCredential = BCrypt.Net.BCrypt.HashPassword(userGoogleRegisterDto.GoogleCredential);
            var adduser = await _userService.AddAsync(_mapper.Map<User>(userGoogleRegisterDto));
            var userWithTokenRegister = _mapper.Map<UserWithTokenDto>(adduser);

            userWithTokenRegister.Token = _jwtService.Generate(userWithTokenRegister.Id);

            return CreateActionResult(CustomResponseDto<UserWithTokenDto>.Success(201, userWithTokenRegister));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            userRegisterDto.Password = BCrypt.Net.BCrypt.HashPassword(userRegisterDto.Password);
            var user = await _userService.AddAsync(_mapper.Map<User>(userRegisterDto));
            var userWithToken = _mapper.Map<UserWithTokenDto>(user);

            userWithToken.Token = _jwtService.Generate(userWithToken.Id);

            return CreateActionResult(CustomResponseDto<UserWithTokenDto>.Success(201, userWithToken));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            var user = _userService.GetByUsername(userLoginDto.Email);        

            if (user == null) return BadRequest(CustomResponseDto<NoContentDto>.Fail(400, "Böyle bir kullanıcı yok!"));

            if (user.IsGoogle)
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(400, "Google hesabı ile giriş yapın!"));
            }

            if (!BCrypt.Net.BCrypt.Verify(userLoginDto.Password, user.Password))
            {
                return BadRequest(CustomResponseDto<NoContentDto>.Fail(400, "E-posta veya parola yanlış!"));
            }

            var userWithTokenLogin = _mapper.Map<UserWithTokenDto>(user);
            userWithTokenLogin.Token = _jwtService.Generate(userWithTokenLogin.Id);

            return CreateActionResult(CustomResponseDto<UserWithTokenDto>.Success(201, userWithTokenLogin));
        }

        [HttpGet("user")]
        public async Task<IActionResult> User(string jwt)
        {
            try
            {
                var token = _jwtService.Verify(jwt);

                int id = int.Parse(token.Issuer);

                var getuser = await _userService.GetByIdAsync(id);
                var user = _mapper.Map<UserDto>(getuser);

                return Ok(user);
            }
            catch (Exception e)
            {
                return Unauthorized();
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {

            Response.Cookies.Delete("jwt", new CookieOptions
            {
                //HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(-1),
                IsEssential = true,
                SameSite = SameSiteMode.Lax,
                //Secure = true
            });

            return Ok(new { message = "success" });
        }

        [HttpPatch("{id}")]

        public async Task<IActionResult> UpdatePatch(int id, JsonPatchDocument user)
        {
            await _userService.UpdatePatchAsync(id, user);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpPatch("[action]")]

        public async Task<IActionResult> UpdatePasswordPatch(string email, string password, JsonPatchDocument userpatch)
        {
            var user = _userService.GetByUsername(email);
            if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return BadRequest(new { message = "Passwords not equals." });
            }
            userpatch.Operations.FirstOrDefault().value = BCrypt.Net.BCrypt.HashPassword((string)userpatch.Operations.FirstOrDefault().value);
            await _userService.UpdatePatchAsync(user.Id, userpatch);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

    }
}
