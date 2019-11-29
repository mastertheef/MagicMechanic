using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : SingletonBase<InputManager>
{
    public void OnAttack(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Performed)
        {
            PlayerAnimationManager.Instance.Attack(true);
        }
        else if (ctx.phase == InputActionPhase.Canceled)
        {
            PlayerAnimationManager.Instance.Attack(false);
        }
    }
}
