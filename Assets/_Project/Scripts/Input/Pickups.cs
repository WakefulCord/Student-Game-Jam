
using UnityEngine;

public class Pickups : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        HUDController HUDController = other.GetComponent<HUDController>();
        if(HUDController != null)
        {
            if (gameObject.CompareTag("Coin"))
            {
                HUDController.UpdateCoinCount();
                Destroy(gameObject);
            }
            if (gameObject.CompareTag("Trophy"))
            {
                HUDController.UpdateTrophyCount();
                Destroy(gameObject);
            }

        }
    }
}
