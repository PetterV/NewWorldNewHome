using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    EncampmentManager encampmentManager;
    public GameObject[] impassableAreas;
    public GameObject NorthCollider;
    public GameObject EastCollider;
    public GameObject SouthCollider;
    public GameObject WestCollider;
    bool canMoveNorth;
    bool canMoveEast;
    bool canMoveSouth;
    bool canMoveWest;
    public bool isHunting = false;

    void Start()
    {
        inventory = GetComponent<PlayerInventory>();
        encampmentMenu = GameObject.Find("EncampmentMenu");
        encampmentManager = GameObject.Find("EncampmentManager").GetComponent<EncampmentManager>();
        impassableAreas = GameObject.FindGameObjectsWithTag("MoveBlocker");
    }
    // Update is called once per frame
    void Update()
    {
        if (!gameController.turnManager.takingTurn && !gameController.isPaused)
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
                    isNorthClear();
                    if (canMoveNorth)
                    {
                        moveDirection = "up";
                        readyToMove = true;
                    }
                    else
                    {
                        Debug.Log("Something is blocking the way north!");
                    }
                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    isEastClear();
                    if (canMoveEast)
                    {
                        moveDirection = "right";
                        readyToMove = true;
                    }
                    else
                    {
                        Debug.Log("Something is blocking the way east!");
                    }
                }
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    isWestClear();
                    if (canMoveWest)
                    {
                        moveDirection = "left";
                        readyToMove = true;
                    }
                    else
                    {
                        Debug.Log("Something is blocking the way east!");
                    }
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    isSouthClear();
                    if (canMoveSouth)
                    {
                        moveDirection = "down";
                        readyToMove = true;
                    }
                    else
                    {
                        Debug.Log("Something is blocking the way east!");
                    }
                }
            }

            //If a direction has been set, and a turn is not already in progress, take the turn
            if (readyToMove)
            {
                gameController.turnManager.TakeTurn();
            }
        }
    }

    void isNorthClear()
    {
        Collider2D northCollider = NorthCollider.GetComponent<Collider2D>();
        canMoveNorth = true;
        foreach (GameObject moveBlocker in impassableAreas)
        {
            if (moveBlocker.GetComponent<Collider2D>().bounds.Intersects(northCollider.bounds))
            {
                canMoveNorth = false;
            }
        }
    }
    void isEastClear()
    {
        Collider2D eastCollider = EastCollider.GetComponent<Collider2D>();
        canMoveEast = true;
        foreach (GameObject moveBlocker in impassableAreas)
        {
            if (moveBlocker.GetComponent<Collider2D>().bounds.Intersects(eastCollider.bounds))
            {
                canMoveEast = false;
            }
        }
    }
    void isSouthClear()
    {
        Collider2D southCollider = SouthCollider.GetComponent<Collider2D>();
        canMoveSouth = true;
        foreach (GameObject moveBlocker in impassableAreas)
        {
            if (moveBlocker.GetComponent<Collider2D>().bounds.Intersects(southCollider.bounds))
            {
                canMoveSouth = false;
            }
        }
    }
    void isWestClear()
    {
        Collider2D westCollider = WestCollider.GetComponent<Collider2D>();
        canMoveWest = true;
        foreach (GameObject moveBlocker in impassableAreas)
        {
            if (moveBlocker.GetComponent<Collider2D>().bounds.Intersects(westCollider.bounds))
            {
                canMoveWest = false;
            }
        }
    }

    public void ExecuteMove()
    {
        if (isEncamped && !(moveDirection == "encamp"))
        {
            encampmentManager.PerTurnSettlement(encampmentManager.settlingPerTurn);
        }

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
        if (!isHunting)
        {
            inventory.UseFoodPerRound();
        }
        if (isEncamped)
        {
            inventory.UseWoodPerRound(inventory.woodPerTurn);
        }

        moveDirection = "none";
        readyToMove = false;
    }

    void ToggleEncampment()
    {
        if (isEncamped)
        {
            isEncamped = false;
            encampmentManager.PopLossOnBreakCamp();
            encampmentMenu.SetActive(false);
            gameController.mode = "move";
            SFXManager.instance.PlayCampSFX(false);
        }
        else
        {
            isEncamped = true;
            encampmentMenu.SetActive(true);
            encampmentManager.StartNewEncampment();
            gameController.mode = "camp";
            SFXManager.instance.PlayCampSFX(true);
        }
    }

    IEnumerator AnimationDelay()
    {
        yield return new WaitForSeconds(1);
    }
}
