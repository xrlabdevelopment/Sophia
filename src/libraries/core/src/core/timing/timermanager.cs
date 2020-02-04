using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace Sophia.Core
{
    public enum TimerFinishedBehaviour
    {
        RESET_ON_FINSHED,
        REMOVE_ON_FINISHED
    }

    public struct TimerCreationInfo
    {
        public float start_time;

        public TimerFinishedBehaviour finish_behaviour;

        public Finished finished_delegate;
        public Started started_delegate;
        public Stopped stopped_delegate;
        public Updated updated_delegate;
    }

    public class TimerManager
    {
        //--------------------------------------------------------------------------------------
        // Fields
        private Dictionary<Guid, ITimer>    timers_active;
        private List<Guid>                  timers_to_remove;

        public TimerManager()
        {
            timers_active = new Dictionary<Guid, ITimer>();
            timers_to_remove = new List<Guid>();
        }

        //--------------------------------------------------------------------------------------
        public Guid createTimer<T>(TimerCreationInfo createionInfo)
            where T : ITimer, new()
        {
            ITimer timer = new T();
            timer.initialize(createionInfo.start_time);

            switch (createionInfo.finish_behaviour)
            {
                case TimerFinishedBehaviour.REMOVE_ON_FINISHED: timer.onFinished += scheduleForRemove;  break;
                case TimerFinishedBehaviour.RESET_ON_FINSHED:   timer.onFinished += scheduleForReset;   break;
            }

            if (createionInfo.finished_delegate != null)    timer.onFinished += createionInfo.finished_delegate;
            if (createionInfo.started_delegate != null)     timer.onStarted += createionInfo.started_delegate;
            if (createionInfo.stopped_delegate != null)     timer.onStopped += createionInfo.stopped_delegate;
            if (createionInfo.updated_delegate != null)     timer.onUpdate += createionInfo.updated_delegate;

            timers_active.Add(timer.TimerID, timer);

            return timer.TimerID;
        }

        //--------------------------------------------------------------------------------------
        public void startTimer(Guid id, bool reset)
        {
            if (timers_active.ContainsKey(id) == false)
                return;

            if (reset)
                timers_active[id].reset();
            timers_active[id].start();
        }
        //--------------------------------------------------------------------------------------
        public void stopTimer(Guid id)
        {
            if (timers_active.ContainsKey(id) == false)
                return;

            timers_active[id].stop();
        }

        //--------------------------------------------------------------------------------------
        public bool isTimerRunning(Guid id)
        {
            if (timers_active.ContainsKey(id) == false)
                return false;

            return timers_active[id].IsRunning;
        }
        //--------------------------------------------------------------------------------------
        public bool isTimerPaused(Guid id)
        {
            if (timers_active.ContainsKey(id) == false)
                return false;

            return timers_active[id].IsPaused;
        }

        //--------------------------------------------------------------------------------------
        public void update(float dTime)
        {
            if (timers_to_remove.Count > 0)
            {
                foreach (Guid timer_id in timers_to_remove)
                    timers_active.Remove(timer_id);
            }

            foreach (KeyValuePair<Guid, ITimer> pair in timers_active)
                pair.Value.update(dTime);
        }

        //--------------------------------------------------------------------------------------
        private void scheduleForRemove(Guid timerID)
        {
            timers_to_remove.Add(timerID);
        }
        //--------------------------------------------------------------------------------------
        private void scheduleForReset(Guid timerID)
        {
            Debug.Assert(timers_active.ContainsKey(timerID));

            timers_active[timerID].reset();
            timers_active[timerID].start();
        }

        //--------------------------------------------------------------------------------------
        private ITimer createNewTimer<T>(TimerCreationInfo createionInfo)
            where T : new()
        {
            return typeof(T).GetConstructor(new Type[] { createionInfo.start_time.GetType() }).Invoke(new object[] { createionInfo.start_time }) as ITimer;
        }
    }
}
