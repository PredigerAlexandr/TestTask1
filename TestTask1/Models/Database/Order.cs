using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TestTask1.Models.Database
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        
        public string FirstName { get; set; }

        public string SurName { get; set; }

        public string Phone { get; set; }

        public string FirstPlace { get; set; }

        public string LastPlace { get; set; }

        public double Weight { get; set; }

        public double Size { get; set; }

        public int Price { get; set; }

        public int TcId { get; set; }

        public TC Tc { get; set; }

    }
}
