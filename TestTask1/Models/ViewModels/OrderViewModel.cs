using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TestTask1.Models.ViewModels
{
    public class OrderViewModel
    {

        [DisplayName("Имя")]
        [Required(ErrorMessage = "Ввведите корректные данные")]
        [StringLength(20)]
        public string FirstName { get; set; }

        [DisplayName("Фамилия")]
        [Required(ErrorMessage = "Ввведите корректные данные")]
        [StringLength(20)]
        public string SurName { get; set; }

        [DisplayName("Номер Телефона")]
        [Phone(ErrorMessage = "Ввведите корректные данные")]
        public string Phone { get; set; }

        [DisplayName("Откуда забрать")]
        [Required(ErrorMessage = "Ввведите корректные данные")]
        [StringLength(50)]
        public string FirstPlace { get; set; }

        [DisplayName("Куда доставить")]
        [Required(ErrorMessage = "Ввведите корректные данные")]
        [StringLength(50)]
        public string LastPlace { get; set; }

        [DisplayName("Вес(кг)")]
        [Required(ErrorMessage = "Ввведите корректные данные")]
        public double Weight { get; set; }

        [DisplayName("Объём(куб.см)")]
        [Required(ErrorMessage = "Ввведите корректные данные")]
        public double Size { get; set; }

        public int Price { get; set; }

        public double Distance { get; set; }
    }
}
