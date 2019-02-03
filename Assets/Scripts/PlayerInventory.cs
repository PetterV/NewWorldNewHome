using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int foodPerTurn = 1; //Base value of food lost per turn when relevant (For Food it always is?)
    public int woodPerTurn = 1; //Base value of wood lost per turn when relevant
    public int maxInventory; //Max inventory size
    public int currentInventory; //Current inventory size. When this is goes over maxInventory, resources will be lost
    public int currentWood = 80; //Current amount of wood
    public int currentFood = 100; //Current amount of Food
    public int currentTools = 50; //Current amount of Tools
    public int woodSpace; //Amount of inventory taken up by Wood - Calculated from woodSize and currentWood
    public int foodSpace; //Amount of inventory taken up by Food - Calculated from foodSize and currentFood
    public int toolSpace; //Amount of inventory taken up by Tools - Calculated from toolSize and currentTools
    public int woodSize = 3; //Amount of inventory taken up by Wood per unit
    public int foodSize = 1; //Amount of inventory taken up by Food per unit
    public int toolSize = 5; //Amount of inventory taken up by Tools per unit

    public int startingPops = 500; //Number of starting pops
    public int currentPops; //Number of current pops

    public GameObject foodValueBox; //The box appearing when the food value changes
    public GameObject woodValueBox; //The box appearing when the wood value changes
    public GameObject toolsValueBox; //The box appearing when the tools value changes
    public GameObject popsValueBox; //The box appearing when the pops value changes

    float woodUsageThreshold = 25f; //The threshold of the "Settled" value that will trigger wood-per-turn usage

    InventoryPanel inventoryPanel;
    EncampmentManager encampmentManager;
    PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        inventoryPanel = GameObject.Find("InventoryPanel").GetComponent<InventoryPanel>();
        encampmentManager = GameObject.Find("EncampmentManager").GetComponent<EncampmentManager>();
        playerMovement = GetComponent<PlayerMovement>();
        currentPops = startingPops;
        maxInventory = currentPops * 2;
        CalculateInventorySpace();
    }

    public void CalculateInventorySpace()
    {
        woodSpace = currentWood * woodSize;
        foodSpace = currentFood * foodSize;
        toolSpace = currentTools * toolSize;
        maxInventory = currentPops * 2;
        currentInventory = woodSpace + foodSpace + toolSpace;
        inventoryPanel.UpdateInventoryView();
    }

    public void UseFood(int value)
    {
        currentFood = currentFood - value;
        CalculateInventorySpace();
        foodValueBox.GetComponent<ValueChangeTooltip>().Activate(value, "loss");
    }
    public void UseFoodPerRound(int value)
    {
        int foodToConsume = CalcFoodPerTurn(value);
        currentFood = currentFood - foodToConsume;
        if(currentFood < 0)
        {
            int foodBalance = currentFood;
            currentFood = 0;
            PopLossFromFood(foodBalance);
            foodToConsume = foodToConsume - foodBalance;
        }
        CalculateInventorySpace();
        foodValueBox.GetComponent<ValueChangeTooltip>().Activate(foodToConsume, "loss");
    }

    public void PopLossFromFood(int foodMissing)
    {
        int popsToLose = foodMissing * -1;
        LosePops(popsToLose);
    }
    public void UseWood(int value)
    {
        currentWood = currentWood - value;
        if (currentWood < 0)
        {
            currentWood = 0;
        }
        CalculateInventorySpace();
        if (value > 0)
        {
            woodValueBox.GetComponent<ValueChangeTooltip>().Activate(value, "loss");
        }
    }

    public void UseWoodPerRound(int value)
    {
        int woodToConsume = CalcWoodPerTurn(value);
        currentWood = currentWood - woodToConsume;
        if (currentWood < 0)
        {
            int woodMissing = currentWood;
            currentWood = 0;
            PopLossFromWood(woodMissing);
        }
        CalculateInventorySpace();
        if (woodToConsume > 0)
        {
            woodValueBox.GetComponent<ValueChangeTooltip>().Activate(woodToConsume, "loss");
        }
    }
    public void PopLossFromWood(int woodMissing)
    {
        int popsToLose = woodMissing * -2;
        LosePops(popsToLose);
    }
    public void UseTools(int value)
    {
        currentTools = currentTools - value;
        if (currentTools < 0)
        {
            currentTools = 0;
        }
        CalculateInventorySpace();
        if (value > 0)
        {
            toolsValueBox.GetComponent<ValueChangeTooltip>().Activate(value, "loss");
        }
    }

    public void LosePops(int value)
    {
        if (currentPops - value <= 0)
        {
            value = currentPops;
        }
        currentPops = currentPops - value;
        if (currentPops <= 0)
        {
            currentPops = 0;
            //TODO: Game Over goes here
        }
        inventoryPanel.UpdateInventoryView();
        popsValueBox.GetComponent<ValueChangeTooltip>().Activate(value, "loss");

    }

    public void GainFood(int value)
    {
        currentFood = currentFood + value;
        CalculateInventorySpace();
        while (currentInventory > maxInventory)
        {
            currentFood = currentFood - 1;
            value = value - 1;
            CalculateInventorySpace();
        }
        if (value >= 0)
        {
            Debug.Log("Gained" + value.ToString() + "food");
            foodValueBox.GetComponent<ValueChangeTooltip>().Activate(value, "gain");
        }
    }
    //Food gain calculations
    public int CalcHuntGain(int baseValue)
    {
        //Currently basevalue times popmodifier + basevalue times toolmodifier
        float popModifier = CalcFoodGainFromPops(baseValue);
        float toolsModifier = CalcFoodGainFromTools(baseValue);
        int foodFromResources = CalcFoodGainFromResources(1);

        int foodToGain = Mathf.RoundToInt(popModifier) + Mathf.RoundToInt(toolsModifier) + foodFromResources;
        
        return foodToGain;
    }
    public int CalcFoodGainFromPops(int baseValue)
    {
        float popModifier = currentPops / 100;
        int foodFromPops = Mathf.RoundToInt(baseValue * popModifier);

        return foodFromPops;
    }
    public int CalcFoodGainFromTools(int baseValue)
    {
        float toolsModifier = currentTools / 10;
        int foodFromTools = Mathf.RoundToInt(baseValue * toolsModifier);

        return foodFromTools;
    }
    public int CalcFoodGainFromResources(int baseValue)
    {
        int foodResources = encampmentManager.huntFood;
        return foodResources;
    }
    
    //Wood gathering
    public void GainWood(int value)
    {
        currentWood = currentWood + value;
        CalculateInventorySpace();
        while (currentInventory > maxInventory)
        {
            currentWood = currentWood - 1;
            value = value - 1;
            CalculateInventorySpace();
        }
        if (value >= 0)
            woodValueBox.GetComponent<ValueChangeTooltip>().Activate(value, "gain");
    }
    public int CalcGatherGain(int baseValue)
    {
        //Currently basevalue times popmodifier + basevalue times toolmodifier
        float popModifier = CalcWoodGainFromPops(baseValue);
        float toolsModifier = CalcWoodGainFromTools(baseValue);
        int resourceGain = CalcWoodGainFromResources(1);

        int foodToGain = Mathf.RoundToInt(popModifier) + Mathf.RoundToInt(toolsModifier) + resourceGain;

        return foodToGain;
    }
    public int CalcWoodGainFromPops(int baseValue)
    {
        float popModifier = currentPops / 100;
        int woodFromPops = Mathf.RoundToInt(baseValue * popModifier);

        return woodFromPops;
    }
    public int CalcWoodGainFromTools(int baseValue)
    {
        float toolsModifier = currentTools / 10;
        int woodFromTools = Mathf.RoundToInt(baseValue * toolsModifier);

        return woodFromTools;
    }
    public int CalcWoodGainFromResources(int baseValue)
    {
        int woodResources = encampmentManager.gatherWood;
        return woodResources;
    }
    public void GainTools(int value)
    {
        currentTools = currentTools + value;
        CalculateInventorySpace();
        while (currentInventory > maxInventory)
        {
            currentTools = currentTools - 1;
            value = value - 1;
            CalculateInventorySpace();
        }
        if (value > 0)
            toolsValueBox.GetComponent<ValueChangeTooltip>().Activate(value, "gain");
    }
    public int CalcCraftingGain(int baseValue)
    {
        //Currently basevalue times popmodifier
        float popModifier = CalcToolsGainFromPops(baseValue);
        float toolsModifier = CalcWoodGainFromTools(baseValue);
        int resourceGain = CalcToolsGainFromResources(1);

        int foodToGain = Mathf.RoundToInt(popModifier) + Mathf.RoundToInt(toolsModifier) + resourceGain;

        return foodToGain;
    }
    public int CalcToolsGainFromPops(int baseValue)
    {
        float popModifier = currentPops / 100;
        int toolsFromPops = Mathf.RoundToInt(baseValue * popModifier);

        return toolsFromPops;
    }
    public int CalcToolsGainFromResources(int baseValue)
    {
        int toolsResources = encampmentManager.craftingTools;
        return toolsResources;
    }

    public void GainPops(int value)
    {
        currentPops += value;
        popsValueBox.GetComponent<ValueChangeTooltip>().Activate(value, "gain");
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
