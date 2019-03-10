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

    public bool hasFired = false;

    public EncounterManager encounterManager;

    public void ActivateEncounter()
    {
        hasFired = true;
        encounterManager = GameObject.Find("EncounterManager").GetComponent<EncounterManager>();
        encounterManager.activeEncounter = gameObject;

        encounterTitle.text = title;
        encounterText.text = text;

        OptionAText.text = optionAName;
        OptionATooltip.text = optionATooltip;

        if (hasOptionB)
        {
            OptionB.SetActive(true);
            OptionBText.text = optionBName;
            OptionBTooltip.text = optionBTooltip;
        }
        else
        {
            OptionB.SetActive(false);
        }
        if (hasOptionC)
        {
            OptionC.SetActive(true);
            OptionCText.text = optionCName;
            OptionCTooltip.text = optionCTooltip;
        }
        else
        {
            OptionC.SetActive(false);
        }
    }
}
