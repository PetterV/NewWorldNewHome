using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    PlayerInventory playerInventory;
    TurnManager turnManager;

    // Start is called before the first frame update
    void Start()
    {
        playerInventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
        turnManager = GameObject.Find("TurnManager").GetComponent<TurnManager>();
    }

    public void Hunt()
    {
        int foodToGather = r.Next(minHuntFood, maxHuntFood + 1);
        playerInventory.GainFood(foodToGather);
        playerInventory.UseTools(huntCost);
        turnManager.TakeTurn();
        //TODO: Show outcome somewhere on-screen
    }
    public void Gather()
    {
        int woodToGather = r.Next(minGatherWood, maxGatherWood + 1);
        playerInventory.GainWood(woodToGather);
        playerInventory.UseTools(gatherCost);
        turnManager.TakeTurn();
        //TODO: Show outcome somewhere on-screen
    }
    public void Craft()
    {
        int toolsToCraft = r.Next(minCraftingTools, maxCraftingTools + 1);
        playerInventory.GainTools(toolsToCraft);
        turnManager.TakeTurn();
        //TODO: Show outcome somewhere on-screen
    }
}
