using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : SingletonBase<InputManager>
{
    private Ray cameraToGroundRay;
    private RaycastHit groundHit;

    public void OnAttack(InputAction.CallbackContext ctx)
    {
        switch (ctx.phase)
        {
            case InputActionPhase.Performed:
                PlayerStateManager.Instance.SetState(PlayerState.Attacking);
                break;
            case InputActionPhase.Canceled:
            default:
                PlayerStateManager.Instance.SetState(PlayerState.Idle);
                break;
        }
    }
}
