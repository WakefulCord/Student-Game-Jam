using UnityEngine;

public class EndTransition : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneController.instance.LastScene();
        }
    }
}
