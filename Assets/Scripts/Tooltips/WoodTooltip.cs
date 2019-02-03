using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WoodTooltip : MonoBehaviour
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
        weightPer.text = playerInventory.woodSize.ToString();
        lossPerTurn.text = playerInventory.CalcWoodPerTurn(playerInventory.woodPerTurn).ToString();
        totalWeight.text = playerInventory.woodSpace.ToString();
    }
    
    public void CloseTooltip()
    {
        tooltip.SetActive(false);
    }
}
