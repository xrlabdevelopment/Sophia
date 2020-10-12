using UnityEngine;

namespace Sophia.Platform.Patterns
{
    public interface ITransition
    {
        int InstanceID { get; }

        string InstanceName { get; }

        string EventName { get; set; }

        IState TargetState { get; set; }
    }
}
