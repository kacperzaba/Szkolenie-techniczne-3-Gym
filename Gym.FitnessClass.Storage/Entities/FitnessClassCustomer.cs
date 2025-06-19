using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.FitnessClass.Storage.Entities
{
    public class FitnessClassCustomer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ExternalId { get; set; }

        [Required]
        public int FitnessClassId { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(11)]
        public string PhoneNumber { get; set; }
    }
}
