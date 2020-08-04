using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Sophia
{
    public enum TimerFinishedBehaviour
    {
        NONE,
        RESET_START_ON_FINSHED,
        RESET_STOP_ON_FINISHED,
        REMOVE_ON_FINISHED
    }

    public struct TimerEventInfo
    {
        public Finished finished_delegate;
        public Started  started_delegate;
        public Stopped  stopped_delegate;
        public Updated  updated_delegate;
    }

    public struct TimerCreationInfo
    {
        public float    start_time;
        public bool     start_on_creation;

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
        private readonly Dictionary<Guid, ITimer>    timers_active;
        private readonly List<Guid>                  timers_to_remove;

        //--------------------------------------------------------------------------------------
        public TimerManager()
        {
            timers_active = new Dictionary<Guid, ITimer>();
            timers_to_remove = new List<Guid>();
        }

        //--------------------------------------------------------------------------------------
        public static TimerCreationInfo createResetOnFinishedCreationInfo(float startTime, bool shouldStartOnReset)
        {
            return createResetOnFinishedCreationInfo(startTime, shouldStartOnReset, false);
        }
        //--------------------------------------------------------------------------------------
        public static TimerCreationInfo createResetOnFinishedCreationInfo(float startTime, bool shouldStartOnReset, bool startOnCreation)
        {
            return createResetOnFinishedCreationInfo(startTime, shouldStartOnReset, startOnCreation, null);
        }
        //--------------------------------------------------------------------------------------
        public static TimerCreationInfo createResetOnFinishedCreationInfo(float startTime, bool shouldStartOnReset, bool startOnCreation, Finished finishedDelegate)
        {
            TimerEventInfo event_info = new TimerEventInfo()
            {
                finished_delegate = finishedDelegate,
                started_delegate = null,
                stopped_delegate = null,
                updated_delegate = null
            };

            return createResetOnFinishedCreationInfo(startTime, shouldStartOnReset, startOnCreation, event_info);
        }
        //--------------------------------------------------------------------------------------
        public static TimerCreationInfo createResetOnFinishedCreationInfo(float startTime, bool shouldStartOnReset, bool startOnCreation, TimerEventInfo eventInfo)
        {
            TimerCreationInfo info = new TimerCreationInfo
            {
                start_time = startTime,
                start_on_creation = startOnCreation,
                finish_behaviour = shouldStartOnReset
                    ? TimerFinishedBehaviour.RESET_START_ON_FINSHED
                    : TimerFinishedBehaviour.RESET_STOP_ON_FINISHED,

                finished_delegate = eventInfo.finished_delegate,
                started_delegate = eventInfo.started_delegate,
                stopped_delegate = eventInfo.stopped_delegate,
                updated_delegate = eventInfo.updated_delegate
            };

            return info;
        }

        //--------------------------------------------------------------------------------------
        public static TimerCreationInfo createRemoveOnFinishedCreationInfo(float startTime)
        {
            return createRemoveOnFinishedCreationInfo(startTime, false);
        }
        //--------------------------------------------------------------------------------------
        public static TimerCreationInfo createRemoveOnFinishedCreationInfo(float startTime, bool startOnCreation)
        {
            return createRemoveOnFinishedCreationInfo(startTime, startOnCreation, null);
        }
        //--------------------------------------------------------------------------------------
        public static TimerCreationInfo createRemoveOnFinishedCreationInfo(float startTime, bool startOnCreation, Finished finishedDelegate)
        {
            TimerEventInfo event_info = new TimerEventInfo()
            {
                finished_delegate = finishedDelegate,
                started_delegate = null,
                stopped_delegate = null,
                updated_delegate = null
            };

            return createRemoveOnFinishedCreationInfo(startTime, startOnCreation, event_info);
        }
        //--------------------------------------------------------------------------------------
        public static TimerCreationInfo createRemoveOnFinishedCreationInfo(float startTime, bool startOnCreation, TimerEventInfo eventInfo)
        {
            TimerCreationInfo info = new TimerCreationInfo
            {
                start_time = startTime,
                start_on_creation = startOnCreation,
                finish_behaviour = TimerFinishedBehaviour.REMOVE_ON_FINISHED,

                finished_delegate = eventInfo.finished_delegate,
                started_delegate = eventInfo.started_delegate,
                stopped_delegate = eventInfo.stopped_delegate,
                updated_delegate = eventInfo.updated_delegate
            };

            return info;
        }

        //--------------------------------------------------------------------------------------
        public ITimer getTimer(Guid timerID)
        {
            if (timers_active.ContainsKey(timerID))
            {
                return timers_active[timerID];
            }

            return null;
        }

        //--------------------------------------------------------------------------------------
        public Guid createTimer<T>(TimerCreationInfo createionInfo)
            where T : ITimer, new()
        {
            ITimer timer = new T();
            timer.initialize(createionInfo.start_time);

            switch (createionInfo.finish_behaviour)
            {
                case TimerFinishedBehaviour.REMOVE_ON_FINISHED:
                    timer.onFinished += scheduleForRemove;
                    break;
                case TimerFinishedBehaviour.RESET_START_ON_FINSHED:
                    timer.onFinished += scheduleForResetStart;
                    break;
                case TimerFinishedBehaviour.RESET_STOP_ON_FINISHED:
                    timer.onFinished += scheduleForResetStop;
                    break;
                default:
                    timer.onFinished += scheduleForRemove;
                    break;
            }

            if (createionInfo.finished_delegate != null)
                timer.onFinished += createionInfo.finished_delegate;
            if (createionInfo.started_delegate != null)
                timer.onStarted += createionInfo.started_delegate;
            if (createionInfo.stopped_delegate != null)
                timer.onStopped += createionInfo.stopped_delegate;
            if (createionInfo.updated_delegate != null)
                timer.onUpdate += createionInfo.updated_delegate;

            timers_active.Add(timer.TimerID, timer);

            if (createionInfo.start_on_creation)
                timer.start();

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
        public void togglePauseTimer(Guid id)
        {
            if (timers_active.ContainsKey(id) == false)
                return;

            timers_active[id].togglePause();
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
        public void addTime(Guid id, float time)
        {
            if (timers_active.ContainsKey(id) == false)
                return;

            timers_active[id].addTime(time);
        }

        //--------------------------------------------------------------------------------------
        private void scheduleForRemove(Guid timerID)
        {
            timers_to_remove.Add(timerID);
        }
        //--------------------------------------------------------------------------------------
        private void scheduleForResetStart(Guid timerID)
        {
            Debug.Assert(timers_active.ContainsKey(timerID));

            timers_active[timerID].reset(timers_active[timerID].StartTime, true);
        }
        //--------------------------------------------------------------------------------------
        private void scheduleForResetStop(Guid timerID)
        {
            Debug.Assert(timers_active.ContainsKey(timerID));

            timers_active[timerID].reset(timers_active[timerID].StartTime, false);
        }
    }
}
