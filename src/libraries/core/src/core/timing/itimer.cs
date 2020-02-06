using System;

namespace Sophia.Core
{
    public delegate void Finished(Guid timerID);                    // When timer reached 0
    public delegate void Started(Guid timerID);                     // When timer is started
    public delegate void Stopped(Guid timerID);                     // When timer is stopped
    public delegate void Updated(Guid timerID, float time);         // Fired every update

    public interface ITimer
    {
        //--------------------------------------------------------------------------------------
        // Delegates
        Finished onFinished      { set; get; }
        Started  onStarted       { set; get; }
        Stopped  onStopped       { set; get; }
        Updated  onUpdate        { set; get; }

        //--------------------------------------------------------------------------------------
        // Properties
        Guid     TimerID         { get; }

        float    StartTime       { get; }
        float    CurrentTime     { get; }

        bool     IsRunning       { get; }
        bool     IsPaused        { get; }

        //--------------------------------------------------------------------------------------
        void initialize(float time);

        //--------------------------------------------------------------------------------------
        void start();
        //--------------------------------------------------------------------------------------
        void stop();

        //--------------------------------------------------------------------------------------
        void togglePause();

        //--------------------------------------------------------------------------------------
        void reset();
        //--------------------------------------------------------------------------------------
        void reset(float time);
        //--------------------------------------------------------------------------------------
        void reset(float time, bool shouldStart);

        //--------------------------------------------------------------------------------------
        void update(float dTime);
    }
}
