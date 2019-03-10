using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueChangeTooltip : MonoBehaviour
{
    public GameObject goodGainLabel; //The "labels" are the tooltips appearing, but I'm sick and my brain is no good for naming things right now
    public GameObject badGainLabel;

    public void Activate(int value, string change)
    {
        if (change == "gain")
        {
            GameObject newText = Instantiate(goodGainLabel, gameObject.transform);
            ValueChangeTooltipMovement newTextScript = newText.GetComponent<ValueChangeTooltipMovement>();
            newText.SetActive(true);
            newTextScript.t.text = "+" + value.ToString();
        }
        else if (change == "loss")
        {
            GameObject newText = Instantiate(badGainLabel, gameObject.transform);
            ValueChangeTooltipMovement newTextScript = newText.GetComponent<ValueChangeTooltipMovement>();
            newText.SetActive(true);
            newTextScript.t.text = "-" + value.ToString();
        }
        else
        {
            GameObject newText = Instantiate(goodGainLabel, gameObject.transform);
            ValueChangeTooltipMovement newTextScript = newText.GetComponent<ValueChangeTooltipMovement>();
            newText.SetActive(true);
            newTextScript.t.text = value.ToString();
        }
    }
}
