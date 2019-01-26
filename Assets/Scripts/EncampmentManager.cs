using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EncampmentManager : MonoBehaviour
{
    System.Random r = new System.Random();
    public int minHuntFood = 3;
    public int maxHuntFood = 6;
    public int minGatherWood = 2;
    public int maxGatherWood = 5;
    public int minCraftingTools = 2;
    public int maxCraftingTools = 5;
    public int huntCost = 1;
    public int gatherCost = 1;
    public int craftingCost = 1;
    public float percentPopLossPerSettled = 0.5f;
    private float timer = 0.0f;
    public float settledValue = 0f;
    public float settlingPerTurn = 2f;
    public float currentSettledProgressDisplay = 0;
    float settledProgressBarSpeed = 0.001f;
    PlayerInventory playerInventory;
    TurnManager turnManager;
    Image progressBar;
    PlayerMovement playerMovement;
    

    // Start is called before the first frame update
    void Start()
    {
        playerInventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
        turnManager = GameObject.Find("TurnManager").GetComponent<TurnManager>();
        progressBar = GameObject.Find("SettledProgressBar").GetComponent<Image>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        PerTurnSettlement(0f);
    }

    public void Hunt()
    {
        if (turnManager.takingTurn)
        {
            int foodToGather = r.Next(minHuntFood, maxHuntFood + 1);
            playerInventory.GainFood(foodToGather);
            playerInventory.UseTools(huntCost);
            turnManager.TakeTurn();
            //TODO: Show outcome somewhere on-screen
        }
    }
    public void Gather()
    {
        if (turnManager.takingTurn)
        {
            int woodToGather = r.Next(minGatherWood, maxGatherWood + 1);
            playerInventory.GainWood(woodToGather);
            playerInventory.UseTools(gatherCost);
            turnManager.TakeTurn();
            //TODO: Show outcome somewhere on-screen
        }
    }
    public void Craft()
    {
        if (turnManager.takingTurn)
        {
            int toolsToCraft = r.Next(minCraftingTools, maxCraftingTools + 1);
            playerInventory.UseTools(craftingCost);
            playerInventory.GainTools(toolsToCraft);
            turnManager.TakeTurn();
            //TODO: Show outcome somewhere on-screen
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

    public void PerTurnSettlement(float value)
    {
        settledValue += value;
        GameObject.Find("SettledPercentage").GetComponent<Text>().text = settledValue.ToString() + "%";
    }

    public int CalculatedPopLossOnBreakCamp()
    {
        float percentageToLose = settledValue * percentPopLossPerSettled;
        int popsToLose = Mathf.RoundToInt(playerInventory.currentPops * percentageToLose / 100);
        return popsToLose;
    }

    public void PopLossOnBreakCamp()
    {
        int popsToLose = CalculatedPopLossOnBreakCamp();
        playerInventory.LosePops(popsToLose);
    }
}
