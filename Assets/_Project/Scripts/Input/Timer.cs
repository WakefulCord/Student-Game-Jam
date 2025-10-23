using System;
using UnityEngine;

namespace Utilities
{
    public abstract class Timer
    {
        protected float initialTime;
        protected float Time;
        public bool isRunning { get; protected set; }

        public Action OnTimerStart = delegate { };
        public Action OnTimerStop = delegate { };

        protected Timer(float value)
        {
            initialTime = value;
            isRunning = false;
        }

        public void Start()
        {
            Time = initialTime;
            if (!isRunning)
            {
                isRunning = true;
                OnTimerStart.Invoke();
            }
        }

        public void stop()
        {
            if (isRunning)
            {
                isRunning = false;
                OnTimerStop.Invoke();
            }
        }

        public void resume() => isRunning = true;

        public void pause() => isRunning = false;

        public abstract void Tick(float deltaTime);
    }
    //countdonw/cooldown timer
    public class CountdownTimer : Timer
    {
        public CountdownTimer(float value) : base(value) { }
        public override void Tick(float deltaTime)
        {
            if (isRunning && Time > 0)
            {
                Time -= deltaTime;
            }
            if (isRunning && Time <= 0)
            {
                stop();
            }
        }
        public float Progress => Mathf.Clamp01(1 - (Time / initialTime));

        public bool IsFinished() => Time <= 0;

        public void Reset() => Time = initialTime;

        public void Reset(float newTime)
        {
            initialTime = newTime;
            Time = initialTime;
        }
    }
    //stopwatch timer
    public class StopwatchTimer : Timer
    {
        public StopwatchTimer() : base(0f) { }
        public override void Tick(float deltaTime)
        {
            if (isRunning)
            {
                Time += deltaTime;
            }
        }
        public float GetTime() => Time;
        public void Reset() => Time = 0f;
    }
}


