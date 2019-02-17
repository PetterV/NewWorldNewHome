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

    public float tileChance = 10;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        tileManager = GameObject.Find("ResourceTileManager").GetComponent<ResourceTileManager>();
        refTransform = GameObject.Find("TileCreationTransform").GetComponent<Transform>();
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

        SetUpResourceTiles();
    }
    void SetUpResourceTiles()
    {
        int widthDone = 1;
        int heightDone = 1;
        bool resourceTilesComplete = false;

        int roll;
        Debug.Log("Generating tiles...");
        while (!resourceTilesComplete)
        {
            while(widthDone < tilesInWidth)
            {
                //Generate the first tile in a row
                refTransform.position = currentPosition + new Vector3(gameController.tileSize * widthDone, 0);
                roll = r.Next(100);
                GenerateTile(roll);

                //Generate the entire row
                while (heightDone < tilesInHeight)
                {
                    refTransform.position = currentPosition + new Vector3(0, gameController.tileSize * heightDone);
                    roll = r.Next(100);
                    GenerateTile(roll);
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
    }

    void GenerateTile(int chance)
    {
        Debug.Log("Evaluating tile chance: " + chance);
        if (chance <= tileChance)
        {
            Debug.Log("Rolled within random range, attempting to create tile");
            GameObject tileToCreate = tileManager.randomiseTileType(); //Picks a tile type to place based on weights
            if (tileToCreate == null)
            {
                tileToCreate = tileManager.resourceTileTypes[0];
            }
            Instantiate(tileToCreate, refTransform, true); //Creates the tile

            Debug.Log("Tried to create " + tileToCreate.name);
        }
    }
}
