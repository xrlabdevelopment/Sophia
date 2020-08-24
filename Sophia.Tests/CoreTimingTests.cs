using System;
using NUnit.Framework;
using Sophia.Core.Timing;

namespace Sophia.Tests.Core
{
    //timermanager.cs
    [TestFixture]
    public class testsuit_TimerManager
    {
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_timermanager_createTimer()
        {
            TimerManager timer_manager = new TimerManager();

            TimerCreationInfo info = new TimerCreationInfo();

            Guid timer_id = timer_manager.createTimer<CountDownTimer>(info);

            Assert.That(timer_manager.getTimer(timer_id), Is.Not.Null);
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_timermanager_startTimer()
        {
            TimerManager timer_manager = new TimerManager();

            TimerCreationInfo info = new TimerCreationInfo();

            Guid timer_id = timer_manager.createTimer<CountDownTimer>(info);

            timer_manager.startTimer(timer_id, true);

            Assert.IsTrue(timer_manager.getTimer(timer_id).IsRunning);
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_timermanager_stopTimer()
        {
            TimerManager timer_manager = new TimerManager();

            TimerCreationInfo info = new TimerCreationInfo();

            Guid timer_id = timer_manager.createTimer<CountDownTimer>(info);

            timer_manager.startTimer(timer_id, true);

            Assert.IsTrue(timer_manager.getTimer(timer_id).IsRunning);

            timer_manager.stopTimer(timer_id);

            Assert.IsFalse(timer_manager.getTimer(timer_id).IsRunning);
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_timermanager_togglePauseTimer()
        {
            TimerManager timer_manager = new TimerManager();

            TimerCreationInfo info = new TimerCreationInfo();

            Guid timer_id = timer_manager.createTimer<CountDownTimer>(info);

            timer_manager.startTimer(timer_id, true);

            Assert.IsTrue(timer_manager.getTimer(timer_id).IsRunning);

            timer_manager.togglePauseTimer(timer_id);

            Assert.IsTrue(timer_manager.getTimer(timer_id).IsPaused);

            timer_manager.togglePauseTimer(timer_id);

            Assert.IsFalse(timer_manager.getTimer(timer_id).IsPaused);
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_timermanager_isTimerRunning()
        {
            TimerManager timer_manager = new TimerManager();

            TimerCreationInfo info = new TimerCreationInfo();

            Guid timer_id = timer_manager.createTimer<CountDownTimer>(info);

            timer_manager.startTimer(timer_id, true);

            Assert.IsTrue(timer_manager.isTimerRunning(timer_id));
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_timermanager_isTimerPaused()
        {
            TimerManager timer_manager = new TimerManager();

            TimerCreationInfo info = new TimerCreationInfo();

            Guid timer_id = timer_manager.createTimer<CountDownTimer>(info);

            timer_manager.startTimer(timer_id, true);

            Assert.IsTrue(timer_manager.getTimer(timer_id).IsRunning);

            timer_manager.togglePauseTimer(timer_id);

            Assert.IsTrue(timer_manager.isTimerPaused(timer_id));

            timer_manager.togglePauseTimer(timer_id);

            Assert.IsFalse(timer_manager.isTimerPaused(timer_id));
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_timermanager_update()
        {
            TimerManager timer_manager = new TimerManager();

            Guid timer_id = timer_manager.createTimer<CountDownTimer>(TimerManager.createRemoveOnFinishedCreationInfo(1.0f));

            timer_manager.startTimer(timer_id, true);

            // Iterate for 3 frames
            for(int i = 0; i < 3; ++i)
                timer_manager.update(1.0f);

            ITimer timer = timer_manager.getTimer(timer_id);

            Assert.That(timer, Is.Null);

            timer_id = timer_manager.createTimer<CountDownTimer>(TimerManager.createResetOnFinishedCreationInfo(1.0f, true));

            timer_manager.startTimer(timer_id, true);

            // Iterate for 3 frames
            for (int i = 0; i < 3; ++i)
                timer_manager.update(1.0f);

            timer = timer_manager.getTimer(timer_id);

            Assert.That(timer, Is.Not.Null);
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_timermanager_addTime()
        {
            TimerManager timer_manager = new TimerManager();

            Guid timer_id = timer_manager.createTimer<CountDownTimer>(TimerManager.createRemoveOnFinishedCreationInfo(1.0f));

            timer_manager.startTimer(timer_id, true);
            timer_manager.update(0.5f);
            timer_manager.addTime(timer_id, 1.0f);
            timer_manager.update(0.5f);

            ITimer timer = timer_manager.getTimer(timer_id);

            Assert.That(timer, Is.Not.Null);
        }
    }
}
