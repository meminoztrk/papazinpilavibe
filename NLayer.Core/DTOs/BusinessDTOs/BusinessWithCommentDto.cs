using NLayer.Core.DTOs.BusinessCommentDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs.BusinessDTOs
{
    public class BusinessWithCommentDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string BusinessName { get; set; }
        public string MinHeader { get; set; }
        public string About { get; set; }
        public string FoodTypes { get; set; }
        public string BusinessProps { get; set; }
        public string BusinessServices { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
        public string MapIframe { get; set; }
        public string CommentTypes { get; set; }
        public string BusinessType { get; set; }
        public string City { get; set; }
        public int CityId { get; set; }
        public string District { get; set; }
        public int DistrictId { get; set; }
        public string Neighborhood { get; set; }
        public int NeighborhoodId { get; set; }
        public string Website { get; set; }
        public string Instagram { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Mo { get; set; }
        public string Tu { get; set; }
        public string We { get; set; }
        public string Th { get; set; }
        public string Fr { get; set; }
        public string Sa { get; set; }
        public string Su { get; set; }
        public int CommentCount { get; set; }
        public double FivePercent { get; set; }
        public double FourPercent { get; set; }
        public double ThreePercent { get; set; }
        public double TwoPercent { get; set; }
        public double OnePercent { get; set; }

        public double Rate { get; set; }
        public List<string> BusinessImages { get; set; }
        public List<BusinessCommentDto> BusinessComments { get; set; }

    }
}
