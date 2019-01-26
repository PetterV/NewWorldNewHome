using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayCraftTooltip : MonoBehaviour
{
    public GameObject tooltip;
    public Text fromPops;
    EncampmentManager encampmentManager;
    PlayerInventory playerInventory;
    // Start is called before the first frame update
    void Start()
    {
        playerInventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
        encampmentManager = GameObject.Find("EncampmentManager").GetComponent<EncampmentManager>();
    }

    public void ShowCraftTooltip()
    {
        int minFromPops = playerInventory.CalcToolsGainFromPops(encampmentManager.minGatherWood);
        int maxFromPops = playerInventory.CalcToolsGainFromPops(encampmentManager.maxGatherWood);
        tooltip.SetActive(true);
        fromPops.text = minFromPops.ToString() + "-" + maxFromPops.ToString();
    }
    public void UpdateCraftTooltip()
    {
        int minFromPops = playerInventory.CalcToolsGainFromPops(encampmentManager.minGatherWood);
        int maxFromPops = playerInventory.CalcToolsGainFromPops(encampmentManager.maxGatherWood);
        fromPops.text = minFromPops.ToString() + "-" + maxFromPops.ToString();
    }
    public void HideCraftTooltip()
    {
        tooltip.SetActive(false);
    }
}
