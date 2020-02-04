using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sophia;

//-------------------------------------------------------------------------
public class ObserverTestEvent1 : IObserverEvent
{ }
//-------------------------------------------------------------------------
public class ObserverTestEvent2 : IObserverEvent
{ }

//-------------------------------------------------------------------------
public class MockObserver : IObserver
{
    public bool has_received_from_desired_subject = false;

    public bool received_test_event_1 = false;
    public bool received_test_event_2 = false;

    private Subject should_receive_from = null;

    //-------------------------------------------------------------------------
    public MockObserver(Subject shouldReceiveFrom)
    {
        should_receive_from = shouldReceiveFrom;
    }

    //-------------------------------------------------------------------------
    public void notify(Subject subject, IObserverEvent evt)
    {
        has_received_from_desired_subject = subject == should_receive_from;

        if (evt is ObserverTestEvent1)
            received_test_event_1 = true;
        if (evt is ObserverTestEvent2)
            received_test_event_2 = true;
    }
}

//-------------------------------------------------------------------------
[TestClass]
public class ObserverTests
{
    //-------------------------------------------------------------------------
    [TestMethod]
    public void Observer_Notify()
    {
        Subject subject = new Subject();

        MockObserver mock_observer_1 = new MockObserver(subject);
        MockObserver mock_observer_2 = new MockObserver(subject);

        subject.attach(mock_observer_1);
        subject.attach(mock_observer_2);

        subject.notify(new ObserverTestEvent1());

        Assert.IsTrue(mock_observer_1.received_test_event_1);
        Assert.IsTrue(mock_observer_2.received_test_event_1);

        Assert.IsFalse(mock_observer_1.received_test_event_2);
        Assert.IsFalse(mock_observer_2.received_test_event_2);

        Assert.IsTrue(mock_observer_1.has_received_from_desired_subject);
        Assert.IsTrue(mock_observer_2.has_received_from_desired_subject);
    }
    //-------------------------------------------------------------------------
    [TestMethod]
    public void Observer_CannotAddSomeObserver()
    {
        Subject subject = new Subject();

        MockObserver mock_observer_1 = new MockObserver(subject);
        MockObserver mock_observer_2 = new MockObserver(subject);

        subject.attach(mock_observer_1);
        subject.attach(mock_observer_1);    // Add a duplicate
        subject.attach(mock_observer_2);
        subject.attach(mock_observer_2);    // /Add a duplicate

        Assert.AreEqual(2, subject.ObserverCount);
    }
}
