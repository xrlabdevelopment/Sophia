namespace Sophia.Core
{
    public interface IStateMachine
    {
        void start();
        void update();
        void reset();
        void stop();
    }
}
