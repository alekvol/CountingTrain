namespace TrainСounter
{
    internal class Train
    {
        public List<Wagon> Wagons { get; }
        public Train(List<Wagon> wagons)
        {
            Wagons = wagons;
        }
    }
}
