using System;

namespace Gym.Subscription.CrossCutting.Dtos
{
    /// <summary>
    /// Reprezentuje dane wymagane do utworzenia nowej subskrypcji.
    /// </summary>
    public class CreateSubscriptionDto
    {
        /// <summary>
        /// Typ subskrypcji (np. miesięczna, roczna).
        /// </summary>
        public string SubscriptionType { get; set; }

        /// <summary>
        /// Data rozpoczęcia subskrypcji.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Data zakończenia subskrypcji.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Kwota do zapłaty za subskrypcję.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Informacja, czy subskrypcja została opłacona.
        /// </summary>
        public bool IsPaid { get; set; }
    }
}
