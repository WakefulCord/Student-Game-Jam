using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static PlayerInputActions;

namespace StudentGameJam
{
    [CreateAssetMenu(fileName = "Input Reader", menuName = "Input/Input Reader")]
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

    public void EnablePlayerActions()
        {
            inputActions.Player.Enable();
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
            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    EnableMouseControlCamera.Invoke();
                    break;
                case InputActionPhase.Canceled:
                    DisableMouseControlCamera.Invoke();
                    break;
            }
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
