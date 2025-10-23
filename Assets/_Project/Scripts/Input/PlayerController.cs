using UnityEngine;
using KBCore.Refs;
using Unity.Cinemachine;

namespace StudentGameJam
{
    public class PlayerController : ValidatedMonoBehaviour 
    {
        [Header("References")]
        [SerializeField, Self] CharacterController controller;
        [SerializeField, Self] Animator animator;
        [SerializeField, Anywhere] CinemachineCamera freeLookVCam;
        [SerializeField, Anywhere] InputReader input;

        [Header("Settings")]
        [SerializeField] float moveSpeed = 5f;
        [SerializeField] float rotationSpeed = 15f;
        [SerializeField] float smoothTime = 0.2f;

        Transform mainCam;

        float currentSpeed = 1f;
        float velocity;

        static readonly int Speed = Animator.StringToHash("Speed");

        void Awake()
        {
            mainCam = Camera.main.transform;
            freeLookVCam.Follow = transform;
            freeLookVCam.LookAt = transform;
            //when object is teleported, adjust the vcam accordingly
            freeLookVCam.OnTargetObjectWarped(transform, transform.position - freeLookVCam.transform.position - Vector3.forward);
        }

        private void Start() => input.EnablePlayerActions();

        void Update()
        {
            HandleMovement();
            UpdateAnimator();
        }

        void UpdateAnimator()
        {
            animator.SetFloat(Speed, currentSpeed);
        }

        void HandleMovement()
        {
            var movementDirection = new Vector3(input.Direction.x, 0f, input.Direction.y).normalized;
            var adjustedDirection = Quaternion.AngleAxis(mainCam.eulerAngles.y, Vector3.up) * movementDirection;
            if(adjustedDirection.magnitude > 0f)
            {
                var targetRotation = Quaternion.LookRotation(adjustedDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                transform.LookAt(transform.position + adjustedDirection);

                var adjustedMovement = adjustedDirection * (moveSpeed * Time.deltaTime);
                controller.Move(adjustedMovement);

                currentSpeed = Mathf.SmoothDamp(currentSpeed, 1f, ref velocity, smoothTime);
            }
            else
            {
                currentSpeed = Mathf.SmoothDamp(currentSpeed, 0f, ref velocity, smoothTime);
            }
        }
    }

}
