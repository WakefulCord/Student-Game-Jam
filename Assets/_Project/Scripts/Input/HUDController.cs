using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

 public class HUDController : MonoBehaviour
 {

    public int NumberOfCoins { get; private set; }
    public int NumberOfTrophies { get; private set; }

    public UnityEvent<HUDController> OnPickupUpdated;

    public void UpdateCoinCount()
    {
       NumberOfCoins++;
       OnPickupUpdated.Invoke(this);
    }

    public void UpdateTrophyCount()
    {
        NumberOfTrophies++;
        OnPickupUpdated.Invoke(this);
    }
 }
