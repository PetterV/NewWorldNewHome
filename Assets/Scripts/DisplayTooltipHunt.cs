using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayTooltipHunt : MonoBehaviour
{
    public GameObject tooltip;
    public Text fromPops;
    public Text fromTools;
    EncampmentManager encampmentManager;
    PlayerInventory playerInventory;
    // Start is called before the first frame update
    void Start()
    {
        playerInventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
        encampmentManager = GameObject.Find("EncampmentManager").GetComponent<EncampmentManager>();
    }

    public void DisplayHuntTooltip()
    {
        int minFromPops = playerInventory.CalcFoodGainFromPops(encampmentManager.minHuntFood);
        int maxFromPops = playerInventory.CalcFoodGainFromPops(encampmentManager.maxHuntFood);
        int minFromTools = playerInventory.CalcFoodGainFromTools(encampmentManager.minHuntFood);
        int maxFromTools = playerInventory.CalcFoodGainFromTools(encampmentManager.maxHuntFood);
        tooltip.SetActive(true);
        fromPops.text = minFromPops.ToString() + "-" + maxFromPops.ToString();
        fromTools.text = minFromTools.ToString() + "-" + maxFromTools.ToString();
    }
    public void UpdateHuntTooltip()
    {
        int minFromPops = playerInventory.CalcFoodGainFromPops(encampmentManager.minHuntFood);
        int maxFromPops = playerInventory.CalcFoodGainFromPops(encampmentManager.maxHuntFood);
        int minFromTools = playerInventory.CalcFoodGainFromTools(encampmentManager.minHuntFood);
        int maxFromTools = playerInventory.CalcFoodGainFromTools(encampmentManager.maxHuntFood);
        fromPops.text = minFromPops.ToString() + "-" + maxFromPops.ToString();
        fromTools.text = minFromTools.ToString() + "-" + maxFromTools.ToString();
    }
    public void HideHuntTooltip()
    {
        tooltip.SetActive(false);
    }
}
