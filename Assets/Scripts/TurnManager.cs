using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public int turn = 0;
    public bool takingTurn = false;
    GameController gameController;
    PlayerMovement playerMovement;
    TurnCounter turnCounter;
    InventoryPanel inventoryPanel;
    EncampmentManager encampmentManager;
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        turnCounter = GameObject.Find("TurnCounter").GetComponent<TurnCounter>();
        inventoryPanel = GameObject.Find("InventoryPanel").GetComponent<InventoryPanel>();
        encampmentManager = GameObject.Find("EncampmentManager").GetComponent<EncampmentManager>(); 
        //Turn setup goes here
    }

    public void TakeTurn()
    {
        takingTurn = true;
        IncrementTurnCount();
        turnCounter.UpdateTurnCounter();
        playerMovement.ExecuteMove();
        if (playerMovement.isEncamped)
        {
            encampmentManager.PerTurnSettlement(encampmentManager.settlingPerTurn);
        }
        takingTurn = false;
    }

    void IncrementTurnCount()
    {
        turn++;
    }
}
