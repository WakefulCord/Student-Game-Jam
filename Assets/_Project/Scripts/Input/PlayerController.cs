using UnityEngine;
using KBCore.Refs;
using Unity.Cinemachine;
using NUnit.Framework;
using Utilities;
using System.Collections.Generic;


namespace StudentGameJam
{
    public class PlayerController : ValidatedMonoBehaviour 
    {
        [Header("References")]
        /*[SerializeField, Self] CharacterController controller;*/
        [SerializeField, Self] Rigidbody rb;
        [SerializeField, Self] GroundChecker groundChecker;
        [SerializeField, Self] Animator animator;
        [SerializeField, Anywhere] CinemachineCamera freeLookVCam;
        [SerializeField, Anywhere] InputReader input;

        [Header("Settings")]
        [SerializeField] float moveSpeed = 5f;
        [SerializeField] float rotationSpeed = 15f;
        [SerializeField] float smoothTime = 0.2f;

        [Header("Jump Settings")]
        [SerializeField] float jumpForce = 10f;
        [SerializeField] float jumpDuration = 0.5f;
        [SerializeField] float jumpCooldown = 0f;
        [SerializeField] float jumpMaxHeight = 2f;
        [SerializeField] float gravityMultiplier = 3f;

        Transform mainCam;

        float currentSpeed = 1f;
        float Velocity;
        float jumpVelocity;

        Vector3 movement;

        List<Timer> timers;
        CountdownTimer jumpTimer;
        CountdownTimer jumpCooldownTimer;

        static readonly int Speed = Animator.StringToHash("Speed");

        void Awake()
        {
            mainCam = Camera.main.transform;
            freeLookVCam.Follow = transform;
            freeLookVCam.LookAt = transform;
            //when object is teleported, adjust the vcam accordingly
            freeLookVCam.OnTargetObjectWarped(transform, transform.position - freeLookVCam.transform.position - Vector3.forward);
            
            rb.freezeRotation = true;

            jumpTimer = new CountdownTimer(jumpDuration);
            jumpCooldownTimer = new CountdownTimer(jumpCooldown);
            timers = new List<Timer>(2) { jumpTimer, jumpCooldownTimer };

            jumpTimer.OnTimerStop += () => jumpCooldownTimer.Start();
            
        }

        private void Start() => input.EnablePlayerActions();

        private void OnEnable()
        {
            input.Jump += OnJump;
        }
        private void OnDisable()
        {
            input.Jump -= OnJump;
        }

        void OnJump(bool performed)
        {
            if (performed && !jumpTimer.isRunning && !jumpCooldownTimer.isRunning && groundChecker.IsGrounded)
            {
                jumpTimer.Start();
            }
            else if (!performed && jumpTimer.isRunning)
            {
                jumpTimer.stop();
            }
        }


        void Update()
        {
            HandleTimers();

            movement = new Vector3(input.Direction.x, 0f, input.Direction.y);
            HandleMovement();
            UpdateAnimator();
        }

        private void FixedUpdate()
        {
            HandleJump();
            HandleMovement();
        }

        void UpdateAnimator()
        {
            animator.SetFloat(Speed, currentSpeed);
        }

        void HandleTimers()
        {
            foreach (var timer in timers)
            {
                timer.Tick(Time.deltaTime);
            }
        }

        void HandleJump()
        {
            if (!jumpTimer.isRunning && groundChecker.IsGrounded)
            {
                jumpVelocity = 0f;
                jumpTimer.stop();
                return;
            }
            if (jumpTimer.isRunning)
            {
                float launchPoint = 0.9f;
                if (jumpTimer.Progress > launchPoint)
                {
                    jumpVelocity = Mathf.Sqrt(2 * jumpMaxHeight * Mathf.Abs(Physics.gravity.y));
                }
                else
                {
                    //Apply less velocity as jump progresses  
                    jumpVelocity += (1 - jumpTimer.Progress) * jumpForce * Time.fixedDeltaTime;
                }
            }
            else
            {
                // Apply gravity when not jumping  
                jumpVelocity += Physics.gravity.y * gravityMultiplier * Time.fixedDeltaTime;
            }

            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpVelocity, rb.linearVelocity.z);
        }
        void HandleMovement()
        {
            var adjustedDirection = Quaternion.AngleAxis(mainCam.eulerAngles.y, Vector3.up) * movement;
            if(adjustedDirection.magnitude > 0f)
            {
                var targetRotation = Quaternion.LookRotation(adjustedDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                transform.LookAt(transform.position + adjustedDirection);

                Vector3 velocity = adjustedDirection * moveSpeed * Time.deltaTime;
                rb.linearVelocity = new Vector3(velocity.x, rb.linearVelocity.y, velocity.z);

                currentSpeed = Mathf.SmoothDamp(currentSpeed, 1f, ref Velocity, smoothTime);
            }
            else
            {
                currentSpeed = Mathf.SmoothDamp(currentSpeed, 0f, ref Velocity, smoothTime);

                rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
            }
        }
    }

}
