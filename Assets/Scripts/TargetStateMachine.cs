using UnityEngine;

public enum TargetState
{
    START,
    IDLE,
    WAIT_SHORT,
    CHOOSE_DESTINATION,
    MOVEMENT
}

public class TargetStateMachine : MonoBehaviour
{
    public TargetState State;
}
