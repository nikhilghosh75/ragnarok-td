/*
 * Utility timer. Has the option to be recurring.
 * Update needs to be called with the deltaTime.
 * TimerCompleteEvent is emitted on completion.
 * 
 * Adapted from https://github.com/xjjon/UnityCooldownTutorial/
 * Written by Christine Chen
 */

using UnityEngine;

namespace WSoft.UI
{
    public class CooldownTimer
    {
        public float timeRemaining { get; private set; }
        public float totalTime { get; private set; }
        public bool isRecurring { get; }
        public bool isActive { get; private set; }

        // Helper fields to get the state of the timer
        public float timeElapsed => totalTime - timeRemaining;
        public float percentRemaining => timeRemaining / totalTime;

        public delegate void TimerCompleteHandler();

        /// <summary>
        /// Emits event when timer is completed
        /// </summary>
        public event TimerCompleteHandler TimerCompleteEvent;

        /// <summary>
        /// Create a new CooldownTimer
        /// Must call Start() to begin timer
        /// </summary>
        /// <param name="time">Timer length (seconds)</param>
        /// <param name="recurring">Is this timer recurring</param>
        public CooldownTimer(float time, bool recurring = false)
        {
            totalTime = time;
            isRecurring = recurring;
            timeRemaining = totalTime;
        }

        /// <summary>
        /// Start timer with existing time
        /// </summary>
        public void Start()
        {
            timeRemaining = totalTime;
            isActive = true;
            if (timeRemaining <= 0)
            {
                TimerCompleteEvent?.Invoke();
            }
        }

        /// <summary>
        /// Start timer with new time
        /// </summary>
        public void Start(float time)
        {
            totalTime = time;
            Start();
        }

        public void UpdateTimer(float timeDelta)
        {
            if (timeRemaining > 0 && isActive)
            {
                timeRemaining -= timeDelta;
                if (timeRemaining <= 0)
                {
                    if (isRecurring)
                    {
                        timeRemaining = totalTime;
                    }
                    else
                    {
                        isActive = false;
                        timeRemaining = 0;
                    }

                    TimerCompleteEvent?.Invoke();
                }
            }
        }
    }
}