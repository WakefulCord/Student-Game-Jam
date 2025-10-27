using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class PickupUI : MonoBehaviour
{
    private TextMeshProUGUI pickupcointext;
    private TextMeshProUGUI pickuptrophytext;

    private void Start()
    {
        pickupcointext = GetComponent<TextMeshProUGUI>();
        pickuptrophytext = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateCoinPickupText(HUDController hudController)
    {
        pickupcointext.text = hudController.NumberOfCoins.ToString();
    }
    public void UpdateTrophyPickupText(HUDController hudController)
    {
        pickuptrophytext.text = hudController.NumberOfTrophies.ToString();
    }
}
