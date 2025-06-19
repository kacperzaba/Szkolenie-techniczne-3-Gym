using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Client.CrossCutting.Dtos
{
    public class SubscriptionDto
    {
        public int Id { get; set; }
        public string SubscriptionType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
