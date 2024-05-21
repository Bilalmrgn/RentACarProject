using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RentACarProject.Models
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(100, MinimumLength = 2)]
        [Column(TypeName = "nvarchar(100)")]
        public override string UserName { get; set; } = "";

        [StringLength(150, MinimumLength = 2)]
        [Column(TypeName = "nvarchar(150)")]
        public string Name { get; set; } = "";

        [StringLength(200, MinimumLength = 2)]
        [Column(TypeName = "nvarchar(200)")]
        public string Address { get; set; } = "";

        [StringLength(5, MinimumLength = 2)]
        [Column(TypeName = "nvarchar(5)")]
        public string Gender { get; set; } = "";

        [EmailAddress]
        [StringLength(100, MinimumLength = 5)]
        [Column(TypeName = "varchar(100)")]
        public override string Email { get; set; } = "";

        [Phone]
        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public override string PhoneNumber { get; set; } = "";
        public DateTime BirthDate { get; set; }
        public DateTime RegisterDate { get; set; }


    }
}