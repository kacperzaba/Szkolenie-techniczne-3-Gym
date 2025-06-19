using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Subscription.CrossCutting.Dtos
{
    public class UpdateSubscriptionDto
    {
        public DateTime EndDate { get; set; }
        public decimal Amount { get; set; }
        public bool IsPaid { get; set; }
    }
}
