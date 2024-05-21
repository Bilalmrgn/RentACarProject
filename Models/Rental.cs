using System.ComponentModel.DataAnnotations.Schema;

namespace RentACarProject.Models
{
    public class Rental
    {
        public int Id { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public float Price { get; set; }

        public string ApplicationUserId { get; set; } = "";
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser? ApplicationUser { get; set; }

        public int CarId { get; set; }
        [ForeignKey("CarId")]
        public Car? Car { get; set; }


    }
}
