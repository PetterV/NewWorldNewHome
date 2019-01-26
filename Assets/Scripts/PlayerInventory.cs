using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int foodPerTurn = 1;
    public int woodPerTurn = 1;
    public int maxInventory = 800;
    public int currentInventory;
    public int currentWood = 80;
    public int currentFood = 100;
    public int currentTools = 50;
    public int woodSpace;
    public int foodSpace;
    public int toolSpace;
    public int woodSize = 3;
    public int foodSize = 1;
    public int toolSize = 5;

    public int startingPops = 500;
    public int currentPops = 500;

    float woodUsageThreshold = 25f;

    InventoryPanel inventoryPanel;
    EncampmentManager encampmentManager;
    PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        inventoryPanel = GameObject.Find("InventoryPanel").GetComponent<InventoryPanel>();
        encampmentManager = GameObject.Find("EncampmentManager").GetComponent<EncampmentManager>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void CalculateInventorySpace()
    {
        woodSpace = currentWood * woodSize;
        foodSpace = currentFood * foodSize;
        toolSpace = currentTools * toolSize;
        currentInventory = woodSpace + foodSpace + toolSpace;
        inventoryPanel.UpdateInventoryView();
    }

    public void UseFood(int value)
    {
        currentFood = currentFood - value;
        CalculateInventorySpace();
    }
    public void UseFoodPerRound(int value)
    {
        int foodToConsume = CalcFoodPerTurn(value);
        currentFood = currentFood - foodToConsume;
        CalculateInventorySpace();
    }
    public void UseWood(int value)
    {
        currentWood = currentWood - value;
        CalculateInventorySpace();
    }

    public void UseWoodPerRound(int value)
    {
        int woodToConsume = CalcWoodPerTurn(value);
        currentWood = currentWood - woodToConsume;
        CalculateInventorySpace();
    }
    public void UseTools(int value)
    {
        currentTools = currentTools - value;
        CalculateInventorySpace();
    }

    public void LosePops(int value)
    {
        currentPops = currentPops - value;
        inventoryPanel.UpdateInventoryView();
    }

    public void GainFood(int value)
    {
        currentFood = currentFood + value;
        CalculateInventorySpace();
        while (currentInventory > maxInventory)
        {
            currentFood = currentFood - 1;
            CalculateInventorySpace();
        }
    }
    public void GainWood(int value)
    {
        currentWood = currentWood + value;
        CalculateInventorySpace();
        while (currentInventory > maxInventory)
        {
            currentWood = currentWood - 1;
            CalculateInventorySpace();
        }
    }
    public void GainTools(int value)
    {
        currentTools = currentTools + value;
        CalculateInventorySpace();
        while (currentInventory > maxInventory)
        {
            currentTools = currentTools - 1;
            CalculateInventorySpace();
        }
    }

    public void GainPops(int value)
    {
        currentPops += value;
    }

    public int CalcFoodPerTurn(int baseValue)
    {
        float popModifier = currentPops / 50;

        int foodToConsume = Mathf.RoundToInt(baseValue * popModifier);

        return foodToConsume;
    }
    
    public int CalcWoodPerTurn(int baseValue)
    {
        int woodToConsume = 0;

        if (encampmentManager)
        {
            if (encampmentManager.settledValue >= woodUsageThreshold)
            {
                float popModifier = currentPops / 100;
                float settlementModifier = encampmentManager.settledValue / 200;

                woodToConsume = Mathf.RoundToInt(baseValue * popModifier * settlementModifier);
            }
        }

        return woodToConsume;
    }
}
