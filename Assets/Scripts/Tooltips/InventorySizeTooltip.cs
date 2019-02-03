using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySizeTooltip : MonoBehaviour
{
    public GameObject tooltip;
    public Text sizePerPop;
    PlayerInventory playerInventory;

    void Start()
    {
        playerInventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
    }

    public void OpenTooltip()
    {
        tooltip.SetActive(true);
        sizePerPop.text = playerInventory.maxInventoryFactor.ToString();
    }
    
    public void CloseTooltip()
    {
        tooltip.SetActive(false);
    }
}
