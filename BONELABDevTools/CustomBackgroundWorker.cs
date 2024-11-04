namespace BonelabDevMode
{
    internal class CustomBackgroundWorker
    {
        public Action? Start;

        public event Action? Finish;

        public Thread? CurrentThread { get; private set; }

        public void Work()
        {
            CurrentThread = new Thread(() =>
            {
                Start?.Invoke();
                Finish?.Invoke();
            })
            {
                IsBackground = true
            };
            CurrentThread.Start();
        }
    }
}