namespace Gym.Subscription.CrossCutting.Dtos
{
    public class SubscriptionDto
    {
        public int Id { get; set; }
        public string SubscriptionType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Amount { get; set; }
        public bool IsPaid { get; set; }
    }
}
