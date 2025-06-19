using System.ComponentModel.DataAnnotations;

namespace Gym.Client.Storage.Entities
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(11)]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(255)]
        public string Address { get; set; }

        public List<CustomerSubscription> CustomerSubscriptions { get; set; } = new List<CustomerSubscription>();
    }
}
