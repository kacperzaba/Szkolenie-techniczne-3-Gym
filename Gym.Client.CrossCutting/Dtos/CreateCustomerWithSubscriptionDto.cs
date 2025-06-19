using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Client.CrossCutting.Dtos
{
    public class CustomerWithSubscriptionsDto : CustomerDto
    {
        public List<SubscriptionDto> Subscriptions { get; set; } = new();
    }
}
