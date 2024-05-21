using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentACarProject.Models
{
    public class Car
    {
        public int Id { get; set; }
        
        //attributes
        [StringLength(50,MinimumLength =2)] //Arabanın markası en az 2 en fazla 50 karakterli olmalı
        [Column(TypeName ="nvarchar(50)")]//Markanın veri tabanında nasıl saklanacağını göstermek için kullanılır(burda nvarchar olarak gösterilecek ve max 50 karakterli olacak)
        public string Brand { get; set; }

        [StringLength(50, MinimumLength = 1)] //Arabanın model en az 1 en fazla 50 karakterli olmalı
        [Column(TypeName = "nvarchar(50)")]
        public string Model { get; set; }

        [StringLength(4, MinimumLength = 4)]
        [Column(TypeName = "varchar(4)")]
        public string ModelYear { get; set; }

        [StringLength(500)]
        [Column(TypeName ="nvarchar(500)")]
        public string? Description { get; set; }// Description yazılmasa da olur(? bu anlama gelir.) 

        [StringLength(30, MinimumLength =1)]
        [Column(TypeName ="nvarchar(30)")]
        public string GearBox { get; set; }

        [StringLength(50, MinimumLength = 1)]
        [Column(TypeName = "nvarchar(50)")]
        public string Color { get; set; }
            
        public bool IsAvailiable { get; set; }

        [Column(TypeName ="real")]
        [Range(0,float.MaxValue)]//0 dan float ın max değerine kadar aralık verilir
        public float Price { get; set; }
        public string? ImageUrl { get; set; }
        

    }
}

