using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayTooltip : MonoBehaviour
{
    public GameObject tooltip;
    // Start is called before the first frame update

    public void TooltipOn()
    {
        tooltip.SetActive(true);
    }
    
    public void TooltipOff()
    {
        tooltip.SetActive(false);
    }
}
