using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolsTooltip : MonoBehaviour
{
    public GameObject tooltip;
    public Text weightPer;
    public Text totalWeight;
    PlayerInventory playerInventory;

    void Start()
    {
        playerInventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
    }

    public void OpenTooltip()
    {
        tooltip.SetActive(true);
        weightPer.text = playerInventory.toolSize.ToString();
        totalWeight.text = playerInventory.toolSpace.ToString();
    }
    
    public void CloseTooltip()
    {
        tooltip.SetActive(false);
    }
}
