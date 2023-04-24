namespace TrainСounter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Генерируем рандомный поезд с рандомным состоянием лампочек у вагонов
            var random = new Random();
            var wagonList = new List<Wagon>();
            for (int i = 0; i < random.Next(3, 1000); i++)
            {
                var wagon = new Wagon(random.Next(6) < 3);
                wagonList.Add(wagon);
            }

            var train = new Train(wagonList);
            var observer = new Observer(train);

            Observer.CountingWagons(observer); 
        }
    }
}