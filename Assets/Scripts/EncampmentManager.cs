﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EncampmentManager : MonoBehaviour
{
    public int minHuntFood = 1; //The minimum basevalue used for acquiring Food
    public int maxHuntFood = 3; //The maximum basevalue used for acquiring Food
    int minHuntGain;
    int maxHuntGain;
    public int minGatherWood = 1; //The minimum basevalue used for acquiring Wood
    public int maxGatherWood = 3; //The maximum basevalue used for acquiring Food
    int maxGatherGain;
    int minGatherGain;
    public int minCraftingTools = 1; //The minimum basevalue used for acquiring Tools
    public int maxCraftingTools = 3; //The maximum basevalue used for acquiring Food
    int minCraftingGain;
    int maxCraftingGain;

    public int huntFood = 0;
    public int gatherWood = 0;
    public int craftingTools = 0;

    public int huntCost = 1; //The basevalue used for calculating the cost of hunting in Tools
    public int gatherCost = 1; //The basevalue used for calculating the cost of gathering in Tools
    
    public float percentPopLossPerSettled = 0.5f; //0-1 value determining the percentage of the "Settled" value that is converted into population remaining at a given settlement 
    public float settledValue = 0f;
    public float settlingPerTurn = 2f;

    private float timer = 0.0f; //Timer used for the display of the Settled progress bar changing
    public float currentSettledProgressDisplay = 0; //Value used to display the correct fillrate for the Settled progress bar
    float settledProgressBarSpeed = 0.001f; //Used to determine the speed of the Settled progress bar filling

    public GameObject FoodInventoryWarning;
    public GameObject WoodInventoryWarning;
    public GameObject ToolsInventoryWarning;
    public GameObject WoodUsageWarning;

    //Various script targets
    PlayerInventory playerInventory;
    TurnManager turnManager;
    Image progressBar;
    PlayerMovement playerMovement;
    GameController gameController;
    ResourceTileManager resourceTileManager;
    public List<GameObject> resourceTilesWithinRange;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        playerInventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
        turnManager = GameObject.Find("TurnManager").GetComponent<TurnManager>();
        progressBar = GameObject.Find("SettledProgressBar").GetComponent<Image>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        resourceTileManager = GameObject.Find("ResourceTileManager").GetComponent<ResourceTileManager>();
        PerTurnSettlement(0f);
        SetPossibleGainValues();
    }

    public void Hunt()
    {
        if (!turnManager.takingTurn && !gameController.isPaused)
        {
            playerMovement.isHunting = true;
            int foodToGain = gameController.random.Next(minHuntGain, maxHuntGain + 1);
            playerInventory.UseTools(huntCost);
            playerInventory.GainFood(foodToGain);
            turnManager.TakeTurn();
        }
    }
    public void Gather()
    {
        if (!turnManager.takingTurn && !gameController.isPaused)
        {
            int woodToGather = gameController.random.Next(minGatherGain, maxGatherGain + 1);
            playerInventory.UseTools(gatherCost);
            playerInventory.GainWood(woodToGather);
            turnManager.TakeTurn();
        }
    }
    public void Craft()
    {
        if (!turnManager.takingTurn && !gameController.isPaused)
        {
            int toolsToCraft = gameController.random.Next(minCraftingGain, maxCraftingGain + 1);
            playerInventory.GainTools(toolsToCraft);
            turnManager.TakeTurn();
        }
    }

    void Update()
    {
        if (playerMovement.isEncamped)
        {
            if (currentSettledProgressDisplay < settledValue)
            {
                timer += Time.deltaTime;

                if (timer > settledProgressBarSpeed)
                {
                    currentSettledProgressDisplay += 0.25f;
                    timer = timer - settledProgressBarSpeed;
                }
            }

            if (progressBar.isActiveAndEnabled)
            {
                progressBar.fillAmount = currentSettledProgressDisplay / 100f;
            }
        }
    }

    public void StartNewEncampment()
    {
        settledValue = 0f;
        //Determine all resources within range
        resourceTileManager.FindResourceTilesWithinRange();
        if (resourceTilesWithinRange.Count == 0)
        {
            Debug.Log("No resource tiles found");
        }
        CalculateResourcesWithinRange();
        currentSettledProgressDisplay = 0f;
        GameObject.Find("SettledPercentage").GetComponent<Text>().text = settledValue.ToString() + "%";
        int popLoss = CalculatedPopLossOnBreakCamp(settledValue);
        GameObject.Find("PredictedLossText").GetComponent<Text>().text = popLoss.ToString();
        int popLossNextTurn = CalculatedPopLossOnBreakCamp(settledValue + settlingPerTurn);
        GameObject.Find("PredictedLossNexTurnText").GetComponent<Text>().text = popLossNextTurn.ToString();
        SetPossibleHuntGain();
        //Fire Monolith events if relevant
        GameObject.Find("ResourceTileManager").GetComponent<ResourceTileManager>().CheckForMonoliths();
    }

    void CalculateResourcesWithinRange()
    {
        foreach (GameObject t in resourceTilesWithinRange)
        {
            ResourceTile tileInfo = t.GetComponent<ResourceTile>();
            huntFood = huntFood + tileInfo.food;
            gatherWood = gatherWood + tileInfo.wood;
            craftingTools = craftingTools + tileInfo.tools;
        }
        //TODO: Make a list of all resources currently within range
    }

    public void PerTurnSettlement(float value)
    {
        settledValue += value;
        if (settledValue >= 100)
        {
            settledValue = 100;
            gameController.GameOver("settled");
        }
        GameObject.Find("SettledPercentage").GetComponent<Text>().text = settledValue.ToString() + "%";
        int popLoss = CalculatedPopLossOnBreakCamp(settledValue);
        int popLossNextTurn = CalculatedPopLossOnBreakCamp(settledValue + settlingPerTurn);
        GameObject.Find("PredictedLossText").GetComponent<Text>().text = popLoss.ToString();
        GameObject.Find("PredictedLossNexTurnText").GetComponent<Text>().text = popLossNextTurn.ToString();
        if (settledValue >= playerInventory.woodUsageThreshold)
        {
            WoodUsageWarning.SetActive(true);
        }
        else
        {
            WoodUsageWarning.SetActive(false);
        }
    }

    public int CalculatedPopLossOnBreakCamp(float settledRating)
    {
        float percentageToLose = settledRating * percentPopLossPerSettled;
        int popsToLose = Mathf.RoundToInt(playerInventory.currentPops * percentageToLose / 100);
        return popsToLose;
    }

    public void PopLossOnBreakCamp()
    {
        int popsToLose = CalculatedPopLossOnBreakCamp(settledValue);
        playerInventory.LosePops(popsToLose);
    }


    //The "SetPossibleBlahGain" methods are to display correct info on the camp action buttons
    public void SetPossibleHuntGain()
    {
        minHuntGain = playerInventory.CalcHuntGain(minHuntFood);
        maxHuntGain = playerInventory.CalcHuntGain(maxHuntFood);

        GameObject.Find("HuntPossibleGain").GetComponent<Text>().text = "+" + minHuntGain.ToString() + "-" + maxHuntGain.ToString();
        //Calculate what the new inventorry can possibly be, and display warning if it can exceed inventory capacity
        int maxPossibleInventory = playerInventory.currentInventory + maxHuntGain;
        if(playerInventory.currentTools >= huntCost)
        {
            maxPossibleInventory = maxPossibleInventory - huntCost;
        }
        else
        {
            maxPossibleInventory = maxPossibleInventory - playerInventory.currentTools;
        }
        if (maxPossibleInventory > playerInventory.maxInventory)
        {
            FoodInventoryWarning.SetActive(true);
        }
        else
        {
            FoodInventoryWarning.SetActive(false);
        }
    }
    public void SetPossibleGatherGain()
    {
        minGatherGain = playerInventory.CalcGatherGain(minGatherWood);
        maxGatherGain = playerInventory.CalcGatherGain(maxGatherWood);

        GameObject.Find("GatherPossibleGain").GetComponent<Text>().text = "+" + minGatherGain.ToString() + "-" + maxGatherGain.ToString();

        //Calculate what the new inventorry can possibly be, and display warning if it can exceed inventory capacity
        int maxPossibleInventory = playerInventory.currentInventory + maxGatherGain - playerInventory.CalcFoodPerTurn();
        if (playerInventory.currentTools >= gatherCost)
        {
            maxPossibleInventory = maxPossibleInventory - gatherCost;
        }
        else
        {
            maxPossibleInventory = maxPossibleInventory - playerInventory.currentTools;
        }
        if (maxPossibleInventory > playerInventory.maxInventory)
        {
            WoodInventoryWarning.SetActive(true);
        }
        else
        {
            WoodInventoryWarning.SetActive(false);
        }
    }
    public void SetPossibleToolsGain()
    {
        minCraftingGain = playerInventory.CalcCraftingGain(minCraftingTools);
        maxCraftingGain = playerInventory.CalcCraftingGain(maxCraftingTools);

        GameObject.Find("CraftPossibleGain").GetComponent<Text>().text = "+" + minCraftingGain.ToString() + "-" + maxCraftingGain.ToString();
        if (playerInventory.currentInventory + maxCraftingGain - playerInventory.CalcFoodPerTurn() > playerInventory.maxInventory)
        {
            ToolsInventoryWarning.SetActive(true);
        }
        else
        {
            ToolsInventoryWarning.SetActive(false);
        }
    }
    public void SetPossibleGainValues()
    {
        SetPossibleHuntGain();
        SetPossibleToolsGain();
        SetPossibleGatherGain();
    }
}
