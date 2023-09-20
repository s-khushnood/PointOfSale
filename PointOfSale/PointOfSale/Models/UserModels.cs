using System.ComponentModel.DataAnnotations;

namespace PointOfSale.Models
{
    public class User
    {
        public int UserId { get; set; }
        [StringLength(12,MinimumLength =3,ErrorMessage ="First Name should have 3 to 12 characters")][Required]
        public string firstName { get; set; }
        [StringLength(12, MinimumLength = 3, ErrorMessage = "Last Name should have 3 to 12 characters")][Required]
        public string lastName { get; set; }
        [Required][RegularExpression(@"^(?:(?=.*[a-z])(?:(?=.*[A-Z])(?=.*[\d\W])|(?=.*\W)(?=.*\d))|(?=.*\W)(?=.*[A-Z])(?=.*\d)).{8,}$",ErrorMessage ="Password does not meet complexity criteria")]
        public string Password { get; set; }
        [Required][RegularExpression(@"^\d\d\d\d\d\d\d\d\d\d\d\d\d$",ErrorMessage ="CNIC must contain 13 digits")]
        public string CNIC { get; set; }
        [Required]
        [RegularExpression(@"^\d\d\d\d\d\d\d\d\d\d\d$",ErrorMessage ="PhoneNo must contain 11 digits")]
        public string PhoneNo { get; set; }
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$", ErrorMessage = "Email is not valid.")]
        [Required]
        public string UserMail { get; set; }
        [Required]
        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime UserCreated { get; set; }
        [Required]
        public int Createdby { get; set; }
    }
    public class UserLog
    {
        [Required]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$", ErrorMessage = "Email is not valid.")]
        public string UserMail { get; set; }
        [StringLength(12, MinimumLength = 8, ErrorMessage = "Password should have 8 to 12 characters")]
        [Required]
        public string Password { get; set; }
    }
    public class Userupdate
    {
        [Required]
        public int UserId { get; set; }

        [StringLength(12, MinimumLength = 3, ErrorMessage = "First Name should have 3 to 12 characters")]
        public string? firstName { get; set; }

        [StringLength(12, MinimumLength = 3, ErrorMessage = "Last Name should have 3 to 12 characters")]
        public string? lastName { get; set; }

        [RegularExpression(@"^(?:(?=.*[a-z])(?:(?=.*[A-Z])(?=.*[\d\W])|(?=.*\W)(?=.*\d))|(?=.*\W)(?=.*[A-Z])(?=.*\d)).{8,}$", ErrorMessage = "Password does not meet complexity criteria")]
        public string? Password { get; set; }

        [RegularExpression(@"^\d\d\d\d\d\d\d\d\d\d\d$", ErrorMessage = "PhoneNo must contain 11 digits")]
        public string? PhoneNo { get; set; }

        [Required]
        public int Updatedby { get; set; }
    }

}
