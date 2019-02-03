using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public float tileSize = 0.01f;
    public GameObject player;
    public TurnManager turnManager;
    public PlayerInventory playerInventory;
    public string mode;
    public string modePausedFrom;
    public System.Random random = new System.Random();
    public GameObject pauseBoard;
    public bool isPaused = false;
    public bool pausedByEvent = false;
    // Start is called before the first frame update
    void Start()
    {
        pauseBoard = GameObject.Find("PauseBoard");
        pauseBoard.SetActive(false);
        mode = "camp";
        player = GameObject.Find("Player");
        player.GetComponent<PlayerMovement>().tileSize = tileSize;
        player.GetComponent<PlayerMovement>().gameController = this;
        playerInventory = player.GetComponent<PlayerInventory>();
        turnManager = GameObject.Find("TurnManager").GetComponent<TurnManager>();
        GameObject.Find("InventoryPanel").GetComponent<InventoryPanel>().UpdateInventoryView();
    }

    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            if (isPaused && !pausedByEvent)
            {
                UnPause();
            }
            else
            {
                Pause(false);
            }
        }
    }

    public void Pause(bool fromEvent)
    {
        pauseBoard.SetActive(true);
        isPaused = true;
    }

    public void UnPause()
    {
        pauseBoard.SetActive(false);
        isPaused = false;
        pausedByEvent = false;
    }
}
