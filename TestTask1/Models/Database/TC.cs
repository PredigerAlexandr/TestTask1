using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestTask1.Models.Database
{
    public class TC
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double CoefficientOfKilometer { get; set; }
        [Required]
        public double CoefficientOfKilogram { get; set; }
        [Required]
        public double CoefficientOfSize { get; set; }

        public List<Order> orders { get; set; } = new List<Order>();

        public double GetPrice(double distance, double weight, double size)
        {
            return (distance*CoefficientOfKilometer+weight*CoefficientOfKilogram+size*CoefficientOfSize);
        }
    }
}
