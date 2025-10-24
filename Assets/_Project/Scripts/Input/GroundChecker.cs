using UnityEngine;

namespace StudentGameJam
{
    public class GroundChecker : MonoBehaviour
    {
        [SerializeField] float groundDistacne = 0.2f;
        [SerializeField] LayerMask groundLayer;

        public bool IsGrounded { get; private set; }

        void Update()
        {
            IsGrounded = Physics.CheckSphere(transform.position, groundDistacne, groundLayer);
        }
    }
}

