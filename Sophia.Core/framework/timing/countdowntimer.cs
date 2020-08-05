using System;
using System.Diagnostics;

namespace Sophia.Core.Timing
{
    public class CountDownTimer : ITimer
    {
        //--------------------------------------------------------------------------------------
        // Delegates
        public Finished onFinished      { set; get; }
        public Started  onStarted       { set; get; }
        public Stopped  onStopped       { set; get; }
        public Updated  onUpdate        { set; get; }

        //--------------------------------------------------------------------------------------
        // Properties
        public Guid TimerID
        {
            get { return timer_id; }
        }

        public float StartTime
        {
            get { return start_time; }
        }
        public float CurrentTime
        {
            get { return curr_time; }
        }

        public bool IsRunning
        {
            get { return is_running; }
        }
        public bool IsPaused
        {
            get { return is_paused; }
        }

        //--------------------------------------------------------------------------------------
        // Fields
        private Guid timer_id;

        private float start_time;
        private float curr_time;

        private bool is_paused;
        private bool is_running;

        private bool is_initialized;

        //--------------------------------------------------------------------------------------
        public void initialize(float time)
        {
            timer_id = Guid.NewGuid();

            start_time = time;
            curr_time = time;

            is_paused = false;
            is_running = false;

            is_initialized = true;
        }

        //--------------------------------------------------------------------------------------
        public void start()
        {
            Debug.Assert(is_initialized, "Initialize timer before using it");

            is_running = true;
            is_paused = false;

            if (onStarted != null)
                onStarted(timer_id);
        }
        //--------------------------------------------------------------------------------------
        public void stop()
        {
            Debug.Assert(is_initialized, "Initialize timer before using it");

            is_running = false;
            is_paused = false;

            curr_time = start_time;

            if (onStopped != null)
                onStopped(timer_id);
        }

        //--------------------------------------------------------------------------------------
        public void togglePause()
        {
            Debug.Assert(is_initialized, "Initialize timer before using it");

            is_paused = !is_paused;
        }

        //--------------------------------------------------------------------------------------
        public void reset()
        {
            reset(start_time);
        }
        //--------------------------------------------------------------------------------------
        public void reset(float time)
        {
            reset(time, false);
        }
        //--------------------------------------------------------------------------------------
        public void reset(float time, bool shouldStart)
        {
            System.Diagnostics.Debug.Assert(is_initialized, "Initialize timer before using it");

            start_time = time;
            curr_time = time;

            if (shouldStart)
                start();
            else
                stop();
        }

        //--------------------------------------------------------------------------------------
        public void update(float dTime)
        {
            Debug.Assert(is_initialized, "Initialize timer before using it");

            if (is_paused || !is_running)
                return;

            curr_time -= dTime;
            if (curr_time <= 0.0f)
            {
                curr_time = 0.0f;

                stop();

                if (onFinished != null)
                {
                    onFinished(timer_id);
                    return;
                }
            }

            if (onUpdate != null)
                onUpdate(timer_id, curr_time);
        }

        //--------------------------------------------------------------------------------------
        public void addTime(float time)
        {
            curr_time += time;
        }
    }
}
