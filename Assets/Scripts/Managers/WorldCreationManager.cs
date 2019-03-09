using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCreationManager : MonoBehaviour
{
    GameController gameController;
    ResourceTileManager tileManager;
    System.Random r;
    float tileSize;
    float worldWidth;
    float worldHeight;
    Transform refTransform;
    Vector3 startPosition;
    Vector3 currentPosition;
    float tilesInWidthFloat;
    float tilesInHeightFloat;
    int tilesInWidth;
    int tilesInHeight;

    public GameObject homeland;
    public float tileChance;
    public float homelandTileChance;

    Collider2D positionRefCollider;
    Collider2D homelandCollider;
    Collider2D startAreaCollider;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        tileManager = GameObject.Find("ResourceTileManager").GetComponent<ResourceTileManager>();
        refTransform = GameObject.Find("TileCreationTransform").GetComponent<Transform>();
        positionRefCollider = GameObject.Find("TileCreationTransform").GetComponent<Collider2D>();
        homelandCollider = GameObject.Find("Homeland").GetComponent<Collider2D>();
        startAreaCollider = GameObject.Find("StartArea").GetComponent<Collider2D>();
        tileSize = gameController.tileSize;
        worldWidth = gameController.worldWidth;
        worldHeight = gameController.worldHeight;
        r = gameController.random;
        startPosition = transform.position;
        currentPosition = startPosition;
        refTransform.position = startPosition;
        tilesInWidthFloat = worldWidth / tileSize;
        tilesInHeightFloat = worldHeight / tileSize;
    }

    public void SetUpWorld()
    {
        tilesInWidth = Mathf.RoundToInt(tilesInWidthFloat);
        Debug.Log(tilesInWidth + " tiles in width");
        tilesInHeight = Mathf.RoundToInt(tilesInHeightFloat);
        Debug.Log(tilesInHeight + " tiles in height");
        SetUpHomeland();
        SetUpResourceTiles();
    }
    void SetUpHomeland()
    {
        bool homelandSet = false;
        while (!homelandSet)
        {
            FindRandomHomelandLocation();
            homelandSet = ValidateHomelandLocation();
        }
    }

    void FindRandomHomelandLocation()
    {
        //TODO: This isn't working quite as it should. I'm probably doing the calculations in the wrong order
        float tempRandomX = r.Next(Mathf.RoundToInt(worldWidth));
        float randomX = tempRandomX * tileSize;
        float tempRandomY = r.Next(Mathf.RoundToInt(worldHeight));
        float randomY = tempRandomY * tileSize;
        GameObject.Find("Homeland").GetComponent<Transform>().position = new Vector3(randomX, randomY);
    }
    bool ValidateHomelandLocation()
    {
        //TODO: Evaluate the homeland location properly here

        if (homelandCollider.bounds.Intersects(startAreaCollider.bounds))
        {
            Debug.Log("Homeland collides with StartArea, roll again.");
            return false;
        }
        else
        {
            Debug.Log("No issues found with Homeland location, setting it here.");
            return true;
        }
    }
    void SetUpResourceTiles() //The setup of resource tiles across the map
    {
        int widthDone = 1; //Starts at 1, since we're counting tile numbers
        int heightDone = 1;
        bool resourceTilesComplete = false;
        
        Debug.Log("Generating tiles...");
        while (!resourceTilesComplete)
        {
            while(widthDone < tilesInWidth)
            {
                //Generate the first tile in a row
                refTransform.position = currentPosition + new Vector3(gameController.tileSize * widthDone, 0);

                RollForGeneration();

                //Generate the entire row
                while (heightDone < tilesInHeight)
                {
                    refTransform.position = currentPosition + new Vector3(0, gameController.tileSize * heightDone);
                    RollForGeneration();
                    heightDone++;
                }
                heightDone = 0;
                currentPosition = currentPosition + new Vector3(gameController.tileSize, 0); //Move one row over
                widthDone++;
            }
            widthDone = 0;
            resourceTilesComplete = true;
        }

        foreach (GameObject t in tileManager.resourceTileTypes)
        {
            t.SetActive(false);
        }

        tileManager.RegisterResourceTiles();
    }

    void RollForGeneration() //Does the actual rolling for generation of tiles
    {
        int roll = r.Next(100);
        bool withinHomelandRange = isWithinHomelandRange();

        /*if (withinHomelandRange)
        {
            GenerateTile(roll, homelandTileChance);
        }
        else
        {*/
            GenerateTile(roll, tileChance);
        //}
    }

    bool isWithinHomelandRange() //Checks whether the ref collider is currently within range of the homeland area
    {
        if (positionRefCollider.bounds.Intersects(homelandCollider.bounds))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void GenerateTile(int chance, float spawnChance) //Check the chance of spawning a tile based on a %-roll, and generate a tile if relevant
    {
        if (chance <= spawnChance)
        {
            Debug.Log("Chance was " + chance + ", spawnChance was " + spawnChance + "   Generating tile");
            //Debug.Log("Rolled within random range, attempting to create tile");
            GameObject tileToCreate = tileManager.randomiseTileType(); //Picks a tile type to place based on weights
            if (tileToCreate == null)
            {
                tileToCreate = tileManager.resourceTileTypes[0];
            }
            Instantiate(tileToCreate, refTransform, true); //Creates the tile

            //Debug.Log("Tried to create " + tileToCreate.name);
        }
    }

    void PlaceHomeland()
    {

    }
}
