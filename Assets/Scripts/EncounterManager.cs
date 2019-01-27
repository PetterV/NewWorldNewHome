using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EncounterManager : MonoBehaviour
{
    public GameController gameController;
    
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        EncounterSetup("ExampleEncounter");
    }

    public void OptionEffects(string optionID)
    {
        if (optionID == "ExampleOption")
        {
            gameController.playerInventory.GainFood(100);
        }
    }

    public void EncounterSetup(string encounterName)
    {
        GameObject.Find(encounterName).SetActive(true);
        GameObject.Find(encounterName).GetComponent<Encounter>().ActivateEncounter();
    }
}
