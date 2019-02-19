using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulationTooltip : MonoBehaviour
{
    public GameObject tooltip;

    public void OpenTooltip()
    {
        tooltip.SetActive(true);
    }
    
    public void CloseTooltip()
    {
        tooltip.SetActive(false);
    }
}
