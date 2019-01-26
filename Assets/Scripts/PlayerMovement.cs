﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool canMove = false;
    public bool readyToMove = false;
    public string moveDirection = "none";
    public float tileSize;
    public float animationSpeed = 0.01f;
    public bool isEncamped = true;
    public GameController gameController; //Set by the GameController itself
    GameObject encampmentMenu;
    PlayerInventory inventory;

    void Start()
    {
        inventory = GetComponent<PlayerInventory>();
        encampmentMenu = GameObject.Find("EncampmentMenu");
    }
    // Update is called once per frame
    void Update()
    {
        if (!readyToMove && Input.GetKeyDown(KeyCode.Space))
        {
            moveDirection = "encamp";
            readyToMove = true;
        }


        if (!readyToMove && isEncamped == false)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                moveDirection = "up";
                readyToMove = true;
            }
            if(Input.GetKeyDown(KeyCode.RightArrow)){
                moveDirection = "right";
                readyToMove = true;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                moveDirection = "left";
                readyToMove = true;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                moveDirection = "down";
                readyToMove = true;
            }
        }

        //If a direction has been set, and a turn is not already in progress, take the turn
        if (readyToMove && !gameController.turnManager.takingTurn)
        {
            gameController.turnManager.TakeTurn();
        }
    }

    public void ExecuteMove()
    {
        if (moveDirection == "encamp")
        {
            ToggleEncampment();
        }
        else
        {
            //Find target position
            float targetPositionX;
            float targetPositionY;
            if (moveDirection == "up")
            {
                targetPositionY = transform.position.y + tileSize;
                targetPositionX = transform.position.x;

                while (targetPositionY > transform.position.y)
                {
                    transform.position = new Vector2(transform.position.x, transform.position.y + animationSpeed);
                    AnimationDelay();
                }
            }
            else if (moveDirection == "down")
            {
                targetPositionY = transform.position.y - tileSize;
                targetPositionX = transform.position.x;

                while (targetPositionY < transform.position.y)
                {
                    transform.position = new Vector2(transform.position.x, transform.position.y - animationSpeed);
                    AnimationDelay();
                }
            }
            else if (moveDirection == "left")
            {
                targetPositionY = transform.position.y;
                targetPositionX = transform.position.x - tileSize;

                while (targetPositionX < transform.position.x)
                {
                    transform.position = new Vector2(transform.position.x - animationSpeed, transform.position.y);
                    AnimationDelay();
                }
            }
            else if (moveDirection == "right")
            {
                targetPositionY = transform.position.y;
                targetPositionX = transform.position.x + tileSize;

                while (targetPositionX > transform.position.x)
                {
                    transform.position = new Vector2(transform.position.x + animationSpeed, transform.position.y);
                    AnimationDelay();
                }
            }
        }

        //Drain food per turn
        inventory.UseFood(inventory.foodPerTurn);

        moveDirection = "none";
        readyToMove = false;
    }

    void ToggleEncampment()
    {
        if (isEncamped)
        {
            isEncamped = false;
            encampmentMenu.SetActive(false);
            gameController.mode = "move";
            SFXManager.instance.PlayCampSFX(false);
        }
        else
        {
            isEncamped = true;
            encampmentMenu.SetActive(true);
            gameController.mode = "camp";
            SFXManager.instance.PlayCampSFX(true);
        }
    }

    IEnumerator AnimationDelay()
    {
        yield return new WaitForSeconds(1);
    }
}
