using KBCore.Refs;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

namespace StudentGameJam
{
    public class CameraManager : ValidatedMonoBehaviour
    {
        [Header("References")]
        [SerializeField, Anywhere] InputReader input;
        [SerializeField, Anywhere] CinemachineCamera freeLookVCam;

        [Header("Settings")]
        [SerializeField, Range(0.5f, 3f)] float speedMultiplier = 1f;

        bool isRMBPressed;
        bool isDeviceMouse;
        bool cameraMovementEnabled;

        void OnLook(Vector2 cameraMovement, bool isDeviceMouse)
        {
            if (cameraMovementEnabled) return;

            if (isDeviceMouse && isRMBPressed) return;

            //If device is mouse, use deltaTime, else use fixedDeltaTime
            float deviceMultiplier = isDeviceMouse ? Time.fixedDeltaTime : Time.deltaTime;
            
            freeLookVCam.transform.Rotate(Vector3.up, cameraMovement.x * speedMultiplier, Space.World);
            freeLookVCam.transform.Rotate(Vector3.right, -cameraMovement.y * speedMultiplier, Space.Self);

        }

        void OnEnable()
        {
            input.Look += OnLook;
            input.EnableMouseControlCamera += OnEnableMouseControlCamera;
            input.DisableMouseControlCamera += OnDisableMouseControlCamera;
        }

        void OnDisable()
        {
            input.Look -= OnLook;
            input.EnableMouseControlCamera -= OnEnableMouseControlCamera;
            input.DisableMouseControlCamera -= OnDisableMouseControlCamera;
        }

        void OnEnableMouseControlCamera()
        {
            isRMBPressed = true;
            //lock cursor to center and hide it
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            StartCoroutine(DisableMouseForFrame());
        }
        IEnumerator DisableMouseForFrame()
        {
            cameraMovementEnabled = true;
            yield return new WaitForEndOfFrame();
            cameraMovementEnabled = false;
        }

        
        void OnDisableMouseControlCamera()
        {
            isRMBPressed = false;
            //unlock cursor and show it
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            //reset camera to prevent jumping
            freeLookVCam.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }

}
