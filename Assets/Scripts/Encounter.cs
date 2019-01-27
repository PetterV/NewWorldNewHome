using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Encounter : MonoBehaviour
{
    public string title;
    public string text;
    public string optionAName;
    public string optionATooltip;
    public string optionBName;
    public string optionBTooltip;
    public string optionCName;
    public string optionCTooltip;
    public bool hasOptionB = false;
    public bool hasOptionC = false;

    public Text encounterTitle;
    public Text encounterText;

    public GameObject OptionA;
    public Text OptionAText;
    public Text OptionATooltip;

    public GameObject OptionB;
    public Text OptionBText;
    public Text OptionBTooltip;

    public GameObject OptionC;
    public Text OptionCText;
    public Text OptionCTooltip;

    public void ActivateEncounter()
    {
        encounterTitle.text = title;
        encounterText.text = text;

        OptionAText.text = optionAName;

        if (hasOptionB)
        {
            OptionB.SetActive(true);
            OptionBText.text = optionBName;
        }
        if (hasOptionC)
        {
            OptionC.SetActive(true);
            OptionCText.text = optionCName;
        }
    }

    public void CloseEncounter()
    {
        OptionC.SetActive(false);
        OptionB.SetActive(false);
        gameObject.SetActive(false);
    }
}
