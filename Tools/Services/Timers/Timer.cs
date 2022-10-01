using System;

namespace Framework.Tools.Services.Timers
{
    public interface ITimer
    {
        float Limit { get; }

        float Value { get; }

        bool IsPaused { get; }

        bool HasTriggeredTimeOutCallback { get; }

        void Update(float deltaTime);

        void Pause(bool isPaused);

        void Stop();

        void SetTimeOutCallback(Action timeOutCallback);
    }

    public class Timer : ITimer
    {
        private Action timeOutCallback;

        public Timer(float limit, Action timeOutCallback)
        {
            Limit = limit;
            this.timeOutCallback = timeOutCallback;
        }

        public float Limit { get; }

        public float Value { get; private set; }

        public bool IsPaused { get; private set; }

        public bool HasTriggeredTimeOutCallback { get; private set; }

        public void Update(float deltaTime)
        {
            if (IsPaused) return;
            Value += deltaTime;
            if (!HasTriggeredTimeOutCallback && Value > Limit)
            {
                timeOutCallback?.Invoke();
                HasTriggeredTimeOutCallback = false;
            }
        }

        public void Pause(bool isPaused)
        {
            IsPaused = isPaused;
        }

        public void Stop()
        {
            Pause(true);
            Value = 0;
        }

        public void SetTimeOutCallback(Action timeOutCallback)
        {
            this.timeOutCallback = timeOutCallback;
        }
    }
}