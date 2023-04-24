namespace TrainСounter
{
    internal class Observer
    {
        private int _currentPosition;
        private Train Train { get; }
        private int Count { get; set; }
        public static int StartPosition { get; set; } = 0;

        public int CurrentPosition
        {
            get => _currentPosition;
            set
            {
                //Здесть, чтобы не ловить ArgumentOutOfRangeException
                //Я проверяю, если выходит за массив, значит мы попали в [0] вагон,
                //и устанавливаю значение начальной позиции откуда пошел счет вагонов
                if (Train.Wagons.Count == value)
                    value = StartPosition;
                _currentPosition = value;
            }
        }

        public Observer(Train train)
        {
            Train = train;
        }

        /// <summary>
        /// Включает/Выключает лампочку в вагоне
        /// </summary>
        /// <param name="light">Вкл - true / Выкл - false</param>
        /// <param name="currentWagon">Позиция вагона в котором вы находитесь</param>
        public void TurnLightInWagon(bool light, int currentWagon)
        {
            Train.Wagons[currentWagon].LightInWagon = light;
        }

        /// <summary>
        /// Проверяет включена ли лампочка в вагоне
        /// </summary>
        /// <param name="currentWagon">Позиция вагона в котором вы находитесь</param>
        /// <returns>true - Вкл / false - Выкл</returns>
        public bool CheckLightInWagon(int currentWagon)
        {
            return Train.Wagons[currentWagon].LightInWagon;
        }

        /// <summary>
        /// Переход к след. вагону
        /// </summary>
        /// <param name="currentWagon">Позиция вагона в котором вы находитесь</param>
        /// <returns>Переход к след. позиции, точнее к след. вагону</returns>
        public static int GoToTheNextWagon(int currentWagon)
        {

            return ++currentWagon;
        }

        /// <summary>
        /// Возврат к начальной позиции и проверка света в вагоне
        /// </summary>
        /// <returns>Состояние лампочки в начальной позиции</returns>
        public bool GoToBackOnStartPosition()
        {
            return CheckLightInWagon(StartPosition);
        }

        /// <summary>
        /// Проверяем свет лампочки в начальной позиции, если выключена, то включаем
        /// </summary>
        public void CheckStartPositionAndTurnOnLight()
        {
            if (CheckLightInWagon(StartPosition) == false)
                TurnLightInWagon(true, StartPosition);
        }

        /// <summary>
        /// Подсчет вагонов.
        /// </summary>
        /// <param name="observer">Наблюдатель поезда</param>
        public static void CountingWagons(Observer observer)
        {
            observer.CheckStartPositionAndTurnOnLight(); 

            do
            {
                observer.Count = observer.CurrentPosition;
                observer.CurrentPosition += GoToTheNextWagon(StartPosition);

                //Если не горит свет в вагоне, идем в след. вагон
                if (!observer.CheckLightInWagon(observer.CurrentPosition)) continue;

                //Если свет есть в вагоне, выключаем и идем в первый вагон
                observer.TurnLightInWagon(false, observer.CurrentPosition);

                if (observer.GoToBackOnStartPosition())
                {
                    //В первом вагоне горит свет, значит начинаем проход заново
                    observer.CurrentPosition = 0;
                }
                else
                {
                    // Добавляем первый вагон, т.к. в нем не горит уже свет,
                    // значит мы нашли кол-во вагонов
                    observer.Count++; 
                    Console.WriteLine($"Кол-во вагонов в поезде: {observer.Count}");
                }

            } while (observer.CheckLightInWagon(observer.CurrentPosition) == false || observer.CurrentPosition == 0);
        }

    }
}
