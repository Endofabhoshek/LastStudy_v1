using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastStudy.Core.DTO
{
    public class UserCreateDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
        //Need to check this
        public bool IsInstituteAdmin { get; set; }
        public string UserName { get; set; }
        public bool IsTeacher { get; set; }
        public bool IsStudent { get; set; }
    }

    public class UserEditDTO : BaseDTO
    {
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string PinCode { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsInstituteAdmin { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class UserInviteDTO : BaseDTO
    {
        public int Id { get; set; }
        [Required]
        public List<string> Emails { get; set; }
        public string Message { get; set; }
        public int RoleId { get; set; }
        public int CourseId { get; set; }
        public string Course { get; set; }
        public string Role { get; set; }
        public int StudentId { get; set; }

    }
}
