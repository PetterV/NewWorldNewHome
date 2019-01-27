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
    public System.Random random = new System.Random();
    // Start is called before the first frame update
    void Start()
    {
        mode = "camp";
        player = GameObject.Find("Player");
        player.GetComponent<PlayerMovement>().tileSize = tileSize;
        player.GetComponent<PlayerMovement>().gameController = this;
        playerInventory = player.GetComponent<PlayerInventory>();
        turnManager = GameObject.Find("TurnManager").GetComponent<TurnManager>();
        GameObject.Find("InventoryPanel").GetComponent<InventoryPanel>().UpdateInventoryView();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
