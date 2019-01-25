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
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        turnCounter = GameObject.Find("TurnCounter").GetComponent<TurnCounter>();
        //Turn setup goes here
    }

    public void TakeTurn()
    {
        takingTurn = true;
        IncrementTurnCount();
        turnCounter.UpdateTurnCounter();
        playerMovement.ExecuteMove();
        takingTurn = false;
    }

    void IncrementTurnCount()
    {
        turn++;
    }
}
