using UnityEngine;

public class TransitionPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneController.instance.NextScene();
        }
    }
}

