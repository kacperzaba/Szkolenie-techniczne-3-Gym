﻿namespace Gym.FitnessClass.CrossCutting.Dtos
{
    public class CreateFitnessClassDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
