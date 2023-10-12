using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLayer.Core.DTOs;
using NLayer.Core.DTOs.UserDTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;
using NLayer.Service.Helper;
using NLayer.Service.Services;

namespace NLayer.API.Controllers
{
    public class UserController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly JwtService _jwtService;
        private readonly CustomImageProcessing _image;

        public UserController(IMapper mapper, IUserService userService, JwtService jwtService, CustomImageProcessing image)
        {
            _mapper = mapper;
            _userService = userService;
            _jwtService = jwtService;
            _image = image;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> UserProfile(string userid)
        {
            return CreateActionResult(await _userService.GetUserProfileByUserId(userid));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(string id, [FromForm] UserUpdateDto userUpdateDto, List<IFormFile> uploadImage)
        {      
            var user = _userService.Where(x=>x.UserId == id).FirstOrDefault();
            var imageList = await _image.ImageProcessing(uploadImage, "user", user.UserPhoto != "defaultuser.png", user.UserPhoto);
            var updatedUser = _mapper.Map<UserUpdateDto, User>(userUpdateDto, user);
            updatedUser.UserPhoto = imageList.Count() > 0 ? imageList.FirstOrDefault() : "defaultuser.png";
            await _userService.UpdateAsync(updatedUser);
            var userWithTokenLogin = _mapper.Map<UserWithTokenDto>(updatedUser);
            userWithTokenLogin.Token = _jwtService.Generate(user.Id);
            return CreateActionResult(CustomResponseDto<UserWithTokenDto>.Success(201, userWithTokenLogin));
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
                userWithTokenLogin.Token = _jwtService.Generate(user.Id);

                return CreateActionResult(CustomResponseDto<UserWithTokenDto>.Success(201, userWithTokenLogin));
            }

            userGoogleRegisterDto.GoogleCredential = BCrypt.Net.BCrypt.HashPassword(userGoogleRegisterDto.GoogleCredential);
            var adduser = await _userService.AddAsync(_mapper.Map<User>(userGoogleRegisterDto));
            var userWithTokenRegister = _mapper.Map<UserWithTokenDto>(adduser);

            userWithTokenRegister.Token = _jwtService.Generate(adduser.Id);

            return CreateActionResult(CustomResponseDto<UserWithTokenDto>.Success(201, userWithTokenRegister));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            userRegisterDto.Password = BCrypt.Net.BCrypt.HashPassword(userRegisterDto.Password);
            var user = await _userService.AddAsync(_mapper.Map<User>(userRegisterDto));
            var userWithToken = _mapper.Map<UserWithTokenDto>(user);

            userWithToken.Token = _jwtService.Generate(user.Id);

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
            userWithTokenLogin.Token = _jwtService.Generate(user.Id);

            return CreateActionResult(CustomResponseDto<UserWithTokenDto>.Success(201, userWithTokenLogin));
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var user = _userService.GetByUsername(email);
            if (user == null)
            {
                return BadRequest("User not found.");
            }

            user.PasswordResetToken = Guid.NewGuid().ToString();
            user.ResetTokenExpires = DateTime.Now.AddMinutes(30);
            await _userService.UpdateAsync(user);

            return Ok("You may now reset your password.");
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(UserResetPasswordDto request)
        {
            var user = await _userService.Where(u => u.UserId == request.UserId && u.PasswordResetToken == request.Token).FirstOrDefaultAsync();
            if (user == null || user.ResetTokenExpires < DateTime.Now)
            {
                return BadRequest("Invalid Token.");
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
            user.PasswordResetToken = null;
            user.ResetTokenExpires = null;

            await _userService.UpdateAsync(user);

            return Ok("Password successfully reset.");
        }

        [HttpGet("user")]
        public async Task<IActionResult> User(string jwt)
        {
            try
            {
                var token = _jwtService.Verify(jwt);

                int id = int.Parse(token.Issuer);

                var getuser = await _userService.GetByIdAsync(id);
                var user = _mapper.Map<UserWithTokenDto>(getuser);

                return Ok(user);
            }
            catch (Exception e)
            {
                return Unauthorized(e);
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
