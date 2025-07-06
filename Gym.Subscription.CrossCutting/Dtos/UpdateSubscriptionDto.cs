using System;

namespace Gym.Subscription.CrossCutting.Dtos
{
    /// <summary>
    /// Reprezentuje dane służące do aktualizacji istniejącej subskrypcji.
    /// </summary>
    public class UpdateSubscriptionDto
    {
        /// <summary>
        /// Nowa data zakończenia subskrypcji.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Zaktualizowana kwota subskrypcji.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Informacja, czy subskrypcja została opłacona.
        /// </summary>
        public bool IsPaid { get; set; }
    }
}
