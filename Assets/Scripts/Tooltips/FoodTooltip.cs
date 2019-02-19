using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodTooltip : MonoBehaviour
{
    public GameObject tooltip;
    public Text weightPer;
    public Text lossPerTurn;
    public Text totalWeight;
    PlayerInventory playerInventory;

    void Start()
    {
        playerInventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
    }

    public void OpenTooltip()
    {
        tooltip.SetActive(true);
        weightPer.text = playerInventory.foodSize.ToString();
        lossPerTurn.text = playerInventory.CalcFoodPerTurn(playerInventory.foodPerTurn).ToString();
        totalWeight.text = playerInventory.foodSpace.ToString();
    }
    
    public void CloseTooltip()
    {
        tooltip.SetActive(false);
    }
}
