namespace Gym.FitnessClass.CrossCutting.Dtos
{
    public class FitnessClassWithCustomersDto 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public ICollection<CustomerDto> Participants { get; set; } = new List<CustomerDto>();
    }
}
