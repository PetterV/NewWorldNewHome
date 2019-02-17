using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ResourceTileManager : MonoBehaviour
{
    public GameObject[] allResourceTiles;
    public GameObject[] allMonoliths;
    public GameObject[] resourceTileTypes;
    public GameObject monolithPrefab;

    EncampmentManager encampmentManager;
    GameController gameController;
    EncounterManager encounterManager;
    // Start is called before the first frame update
    void Start()
    {
        encampmentManager = GameObject.Find("EncampmentManager").GetComponent<EncampmentManager>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        encounterManager = GameObject.Find("EncounterManager").GetComponent<EncounterManager>();
        allResourceTiles = GameObject.FindGameObjectsWithTag("ResourceTile");
        allMonoliths = GameObject.FindGameObjectsWithTag("Monolith");
        //Setup for world generation
        EstablishWeights();
    }

    public void FindResourceTilesWithinRange()
    {
        encampmentManager.resourceTilesWithinRange.Clear();
        Collider2D collider = GameObject.Find("ResourceCollider").GetComponent<Collider2D>();
        foreach (GameObject tile in allResourceTiles)
        {
            tile.GetComponent<ResourceTile>().isWithinRange = false;
            //Does it intersect with the player collider
            if (tile.GetComponent<Collider2D>().bounds.Intersects(collider.bounds))
            {
                encampmentManager.resourceTilesWithinRange.Add(tile);
                tile.GetComponent<ResourceTile>().isWithinRange = true;
            }
        }
    }

    public void CheckForMonoliths()
    {
        Collider2D collider = GameObject.Find("ResourceCollider").GetComponent<Collider2D>();
        foreach (GameObject tile in allMonoliths)
        {
            tile.GetComponent<MonolithTile>().isWithinRange = false;
            //Does it intersect with the player collider
            if (tile.GetComponent<Collider2D>().bounds.Intersects(collider.bounds))
            {
                MonolithTile monolith = tile.GetComponent<MonolithTile>();
                monolith.isWithinRange = true;
                if (monolith.distanceToHomeland <= gameController.homelandCloseRange)
                {
                    //Trigger close event
                    encounterManager.EncounterSetup("MonolithNear");
                }
                else if (monolith.distanceToHomeland <= gameController.homelandMediumNearRange)
                {
                    //Trigger MediumNear event
                    encounterManager.EncounterSetup("MonolithMediumNear");
                }
                else if (monolith.distanceToHomeland <= gameController.homelandMediumFarRange)
                {
                    //Trigger MediumFar event
                    encounterManager.EncounterSetup("MonolithMediumFar");
                }
                else
                {
                    //Trigger Far event
                    encounterManager.EncounterSetup("MonolithFar");
                }
            }
        }
    }

    void EstablishWeights()
    {
        int totalWeights = 0;
        foreach (GameObject t in resourceTileTypes)
        {
            totalWeights = totalWeights + t.GetComponent<ResourceTile>().tileWeight;
        }
        float perWeightValue = 100 / totalWeights;
        Debug.Log("The initial totalWeightValue is " + totalWeights + ", which results in a perWeightValue of " + perWeightValue);
        int weightFrame = 0;
        foreach (GameObject t in resourceTileTypes)
        {
            float tempTileWeight = t.GetComponent<ResourceTile>().tileWeight * perWeightValue;
            t.GetComponent<ResourceTile>().tileWeight = Mathf.RoundToInt(weightFrame + tempTileWeight);
            weightFrame = weightFrame + Mathf.RoundToInt(tempTileWeight);
            Debug.Log(t.name + " has a weight of " + t.GetComponent<ResourceTile>().tileWeight);
        }
    }

    public GameObject randomiseTileType()
    {
        int roll = gameController.random.Next(100);
        GameObject tileToSend = null;

        foreach(GameObject t in resourceTileTypes)
        {
            if (tileToSend == null && roll < t.GetComponent<ResourceTile>().tileWeight)
            {
                tileToSend = t;
            }
        }
        
        return tileToSend;
    }
}
