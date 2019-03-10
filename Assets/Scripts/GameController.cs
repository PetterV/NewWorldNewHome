using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public float tileSize = 0.01f;
    public float worldWidth = 50f;
    public float worldHeight = 25f;
    public GameObject player;
    public TurnManager turnManager;
    public PlayerInventory playerInventory;
    public GameObject loadScreen;
    public string mode;
    public string modePausedFrom;
    public System.Random random = new System.Random();
    GameObject pauseBoard;
    public bool isPaused = false;
    public bool pausedByEvent = false;
    public float homelandCloseRange = 100f;
    public float homelandMediumNearRange = 300f;
    public float homelandMediumFarRange = 800f;


    // Start is called before the first frame update
    void Start()
    {
        //TODO: Make other manager's be activated from here, rather than using void Start
        pauseBoard = GameObject.Find("PauseBoard");
        pauseBoard.SetActive(false);
        mode = "camp";
        player = GameObject.Find("Player");
        player.GetComponent<PlayerMovement>().tileSize = tileSize;
        player.GetComponent<PlayerMovement>().gameController = this;
        playerInventory = player.GetComponent<PlayerInventory>();
        turnManager = GameObject.Find("TurnManager").GetComponent<TurnManager>();
        GameObject.Find("InventoryPanel").GetComponent<InventoryPanel>().UpdateInventoryView();

        //Generate the world
        GameObject.Find("WorldCreationManager").GetComponent<WorldCreationManager>().SetUpWorld();
        loadScreen.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            if (isPaused && !pausedByEvent)
            {
                UnPause();
            }
            else if (!isPaused)
            {
                Pause(false);
            }
        }
    }

    public void Pause(bool fromEvent)
    {
        pauseBoard.SetActive(true);
        isPaused = true;
        pausedByEvent = fromEvent;
    }

    public void UnPause()
    {
        pauseBoard.SetActive(false);
        isPaused = false;
        pausedByEvent = false;
    }

    public void GameOver(string cause)
    {
        if(cause == "popLoss")
        {
            GameObject.Find("EncounterManager").GetComponent<EncounterManager>().EncounterSetup("GameOverScreen");
        }
        else if(cause == "settled")
        {
            GameObject.Find("EncounterManager").GetComponent<EncounterManager>().EncounterSetup("SettledScreen");
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }
}
