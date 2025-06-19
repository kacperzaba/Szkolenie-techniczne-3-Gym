using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Subscription.Storage.Entities
{
    public class Subscription
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string SubscriptionType { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Amount { get; set; }

        public bool IsPaid { get; set; }
    }
}
