using UnityEngine;
using UnityEngine.InputSystem;

namespace Student-Game-Jam
{

    public class InputReader : ScriptableObject, PlayerInputActions.IPlayerActions
    {
    public event UnityAction<Vector2> Move = delegate { };
    public event UnityAction<Vector2, bool> Look = delegate { };
    public event UnityAction EnableMouseControlCamera = delegate { };
    public event UnityAction DisableMouseControlCamera = delegate { };

    PlayerInputActions inputActions;

    public Vector3 Direction => inputActions.Player.Move.ReadValue<Vector2>();

    void OnEnable()
    {
        if(inputActions == null)
        {
            inputActions = new PlayerInputActions();
            inputActions.Player.SetCallbacks(this);
        }
        inputActions.Enable();
    }

    public void OnMove(InputAction.CallbackContext context)
        {
        Move.Invoke(context.ReadValue<Vector2>());
        }

    public void OnLook(InputAction.CallbackContext context)
        {
        Look.Invoke(context.ReadValue<Vector2>(), IsDeviceMouse(context));
    }

    bool IsDeviceMouse(InputAction.CallbackContext context) => context.control.device.name == "Mouse";
    
    public void OnFire(InputAction.CallbackContext context)
        {
            //noop
        }

    public void OnMouseControlCamera(InputAction.CallbackContext context)
        {
            //noop
        }

    public void OnRun(InputAction.CallbackContext context)
        {
            //noop
        }

    public void OnJump(InputAction.CallbackContext context)
        {
            //noop
        }

    }  

}
