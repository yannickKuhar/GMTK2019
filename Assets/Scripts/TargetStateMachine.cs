using UnityEngine;

public enum TargetState
{
    START,
    IDLE,
    WAIT_SHORT,
    CHOOSE_DESTINATION,
    MOVEMENT_GROUND,
    MOVEMENT_AIR
}

public class TargetStateMachine : MonoBehaviour
{
    public TargetState State;
}
