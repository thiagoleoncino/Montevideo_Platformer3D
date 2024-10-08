using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum SurfaceState
{
    Grounded,
    Airborn,
    Waterborne
}

public enum ActionState
{
    Passive,
    Cancelable,
    NonCancelable
}

public enum MoveState
{
    CanMove,
    SemiMove,
    CantMove
}

public class ScrPlayer02StateManager : MonoBehaviour
{
    public MoveState currentMoveState;

    public bool actualColition;

    public bool objectCanMove
    {
        get => currentMoveState == MoveState.CanMove;
        set { if (value) currentMoveState = MoveState.CanMove; }
    }
    public bool objectSemiMove
    {
        get => currentMoveState == MoveState.SemiMove;
        set { if (value) currentMoveState = MoveState.SemiMove; }
    }
    public bool objectCantMove
    {
        get => currentMoveState == MoveState.CantMove;
        set { if (value) currentMoveState = MoveState.CantMove; }
    }

    public SurfaceState currentSurfaceState;

    public bool groundedAction
    {
        get => currentSurfaceState == SurfaceState.Grounded;
        set { if (value) currentSurfaceState = SurfaceState.Grounded; }
    }
    public bool airbornAction
    {
        get => currentSurfaceState == SurfaceState.Airborn;
        set { if (value) currentSurfaceState = SurfaceState.Airborn; }
    }
    public bool waterborneAction
    {
        get => currentSurfaceState == SurfaceState.Waterborne;
        set { if (value) currentSurfaceState = SurfaceState.Waterborne; }
    }

    public ActionState currentActionState;

    public bool passiveAction
    {
        get => currentActionState == ActionState.Passive;
        set { if (value) currentActionState = ActionState.Passive; }
    }
    public bool cancelableAction
    {
        get => currentActionState == ActionState.Cancelable;
        set { if (value) currentActionState = ActionState.Cancelable; }
    }
    public bool noCancelableAction
    {
        get => currentActionState == ActionState.NonCancelable;
        set { if (value) currentActionState = ActionState.NonCancelable; }
    }

    public BoxCollider groundCheckCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            groundedAction = true;
            airbornAction = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            groundedAction = true;
            airbornAction = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            groundedAction = false;
            airbornAction = true;
        }
    }

    //NEW Detener Inercia al colisionar con algo
    private void OnCollisionStay(Collision collision)
    {
        actualColition = collision.collider;
    }

    private void OnCollisionExit(Collision collision)
    {
        actualColition = false;
    }

}