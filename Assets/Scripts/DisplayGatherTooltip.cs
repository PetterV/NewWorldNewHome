using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayGatherTooltip : MonoBehaviour
{
    public GameObject tooltip;
    public Text fromResources;
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

    public void ShowGatherTooltip()
    {
        int minFromPops = playerInventory.CalcWoodGainFromPops(encampmentManager.minGatherWood);
        int maxFromPops = playerInventory.CalcWoodGainFromPops(encampmentManager.maxGatherWood);
        int minFromTools = playerInventory.CalcWoodGainFromTools(encampmentManager.minGatherWood);
        int maxFromTools = playerInventory.CalcWoodGainFromTools(encampmentManager.maxGatherWood);
        int woodFromResources = playerInventory.CalcWoodGainFromResources(1);
        tooltip.SetActive(true);
        fromResources.text = woodFromResources.ToString();
        fromPops.text = minFromPops.ToString() + "-" + maxFromPops.ToString();
        fromTools.text = minFromTools.ToString() + "-" + maxFromTools.ToString();
    }
    public void UpdateGatherTooltip()
    {
        int minFromPops = playerInventory.CalcWoodGainFromPops(encampmentManager.minGatherWood);
        int maxFromPops = playerInventory.CalcWoodGainFromPops(encampmentManager.maxGatherWood);
        int minFromTools = playerInventory.CalcWoodGainFromTools(encampmentManager.minGatherWood);
        int maxFromTools = playerInventory.CalcWoodGainFromTools(encampmentManager.maxGatherWood);
        fromPops.text = minFromPops.ToString() + "-" + maxFromPops.ToString();
        fromTools.text = minFromTools.ToString() + "-" + maxFromTools.ToString();
    }
    public void HideGatherTooltip()
    {
        tooltip.SetActive(false);
    }
}
