using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int foodPerTurn = 1;
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

    InventoryPanel inventoryPanel;
    // Start is called before the first frame update
    void Start()
    {
        inventoryPanel = GameObject.Find("InventoryPanel").GetComponent<InventoryPanel>();
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
    public void UseWood(int value)
    {
        currentWood = currentWood - value;
        CalculateInventorySpace();
    }
    public void UseTools(int value)
    {
        currentTools = currentTools - value;
        CalculateInventorySpace();
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
}
