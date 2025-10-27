using UnityEngine;

namespace StudentGameJam
{
    public class GroundChecker : MonoBehaviour
    {
        [SerializeField] float groundDistacne = 0.2f;
        [SerializeField] LayerMask groundLayer;
        [SerializeField] GameObject player;
        public Vector3 checkpoint = new Vector3(-10, 1, 0);
        public bool IsGrounded { get; private set; }

        void Update()
        {
            IsGrounded = Physics.CheckSphere(transform.position, groundDistacne, groundLayer);
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.CompareTag("Lava"))
            {
                player.transform.position = checkpoint; 
            }
            else if (collision.CompareTag("Checkpoint"))
            {
                checkpoint = collision.transform.position;
            }
        }
    }
}

