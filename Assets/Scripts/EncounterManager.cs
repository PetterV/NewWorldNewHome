using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EncounterManager : MonoBehaviour
{
    public GameController gameController;
    public PlayerInventory playerInventory;
    public GameObject[] encounters;

    public GameObject activeEncounter;
    public GameObject activeOptionB;
    public GameObject activeOptionC;
    
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        playerInventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
        EncounterSetup("TutorialEncounter");
    }

    public void EncounterSetup(string encounterName)
    {
        Debug.Log("Trying to set up encounter " + encounterName);
        gameController.Pause(true);
        Debug.Log("Successfully paused");
        foreach (GameObject encounter in encounters)
        {
            if (encounter.name == encounterName)
            {
                encounter.SetActive(true);
                encounter.GetComponent<Encounter>().ActivateEncounter();
            }
        }
    }

    public void CloseEncounter()
    {
        activeEncounter.SetActive(false);
        gameController.UnPause();
    }

    public void OptionEffect (GameObject effectValues)
    {
        if (effectValues.GetComponent<OptionEffectValues>().resource != "")
        {
            string gainOrLose = effectValues.GetComponent<OptionEffectValues>().gainOrLose;
            string resource = effectValues.GetComponent<OptionEffectValues>().resource;
            float value = effectValues.GetComponent<OptionEffectValues>().value;
            
            if (gainOrLose == "gain")
            {
                if (resource == "food")
                {
                    playerInventory.GainFood(Mathf.RoundToInt(value));
                }
                else if (resource == "wood")
                {
                    playerInventory.GainWood(Mathf.RoundToInt(value));
                }
                else if (resource == "tools")
                {
                    playerInventory.GainTools(Mathf.RoundToInt(value));
                }
                else if (resource == "pops")
                {
                    playerInventory.GainPops(Mathf.RoundToInt(value));
                }
                else if (resource == "settled")
                {
                    EncampmentManager encampmentManager = GameObject.Find("EncampmentManager").GetComponent<EncampmentManager>();
                    encampmentManager.settledValue = encampmentManager.settledValue + value;
                }
                else
                {
                    Debug.LogError("The resource types are 'food', 'wood', 'tools', 'pops' and 'settled'");
                }
            }
            else if (gainOrLose == "lose")
            {
                if (resource == "food")
                {
                    playerInventory.UseFood(Mathf.RoundToInt(value));
                }
                else if (resource == "wood")
                {
                    playerInventory.UseWood(Mathf.RoundToInt(value));
                }
                else if (resource == "tools")
                {
                    playerInventory.UseTools(Mathf.RoundToInt(value));
                }
                else if (resource == "pops")
                {
                    playerInventory.LosePops(Mathf.RoundToInt(value));
                }
                else if (resource == "settled")
                {
                    EncampmentManager encampmentManager = GameObject.Find("EncampmentManager").GetComponent<EncampmentManager>();
                    encampmentManager.settledValue = encampmentManager.settledValue - value;
                }
                else
                {
                    Debug.LogError("The resource types are 'food', 'wood', 'tools', 'pops' and 'settled'");
                }
            }
            else
            {
                Debug.LogError("gainOrLose needs to be either 'gain' or 'lose'");
            }
        }

        else
        {
            Debug.Log("Option Button had no effect assigned");
        }
    }
}
