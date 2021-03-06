﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public int turn = 0;
    public bool takingTurn = false;
    public float turnExecutionTimer = 0.15f;
    public float timer = 0.0f;
    bool wrappingUpTurn = false;
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
        if (playerMovement.isHunting)
        {
            playerMovement.isHunting = false;
        }
        if (playerMovement.isEncamped)
        {
            encampmentManager.SetPossibleGainValues();
            GameObject.Find("HuntTooltipTrigger").GetComponent<DisplayTooltipHunt>().UpdateHuntTooltip();
            GameObject.Find("GatherTooltipTrigger").GetComponent<DisplayGatherTooltip>().UpdateGatherTooltip();
            GameObject.Find("CraftTooltipTrigger").GetComponent<DisplayCraftTooltip>().UpdateCraftTooltip();
        }
        timer = 0.0f;
        wrappingUpTurn = true;
    }

    void Update()
    {
        if (wrappingUpTurn)
        {
            if (turnExecutionTimer > timer)
            {
                timer += Time.deltaTime;
            }
            else
            {
                takingTurn = false;
                wrappingUpTurn = false;
            }
        }
    }

    void IncrementTurnCount()
    {
        turn++;
    }
}
