using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanel : MonoBehaviour
{
    public Text maxInventory;
    public Text currentInventory;
    public Text foodHeader;
    public Text currentFood;
    public Text foodSpace;
    public Text woodHeader;
    public Text currentWood;
    public Text woodSpace;
    public Text toolsHeader;
    public Text currentTools;
    public Text toolSpace;
    public Text currentPops;
    public Text foodPerTurn;
    public Text woodPerTurn;
    PlayerInventory playerInventory;

    public void Start()
    {
        playerInventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
        UpdateInventoryView();
    }

    public void UpdateInventoryView()
    {
        foodHeader.text = "[" + playerInventory.foodSize.ToString() + "]";
        woodHeader.text = "[" + playerInventory.woodSize.ToString() + "]";
        toolsHeader.text = "[" + playerInventory.toolSize.ToString() + "]";
        maxInventory.text = playerInventory.maxInventory.ToString();
        currentInventory.text = playerInventory.currentInventory.ToString();
        currentFood.text = playerInventory.currentFood.ToString();
        foodSpace.text = "(" + playerInventory.foodSpace.ToString() + ")";
        foodPerTurn.text = "Per turn: -" + playerInventory.CalcFoodPerTurn(playerInventory.foodPerTurn).ToString();
        currentWood.text = playerInventory.currentWood.ToString();
        woodSpace.text = "(" + playerInventory.woodSpace.ToString() + ")";
        woodPerTurn.text = "Per turn: -" + playerInventory.CalcWoodPerTurn(playerInventory.woodPerTurn).ToString();
        currentTools.text = playerInventory.currentTools.ToString();
        toolSpace.text = "(" + playerInventory.toolSpace.ToString() + ")";
        currentPops.text = playerInventory.currentPops.ToString();
    }
}
