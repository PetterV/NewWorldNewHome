using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ResourceTileManager : MonoBehaviour
{
    public GameObject[] allResourceTiles;
    EncampmentManager encampmentManager;
    // Start is called before the first frame update
    void Start()
    {
        encampmentManager = GameObject.Find("EncampmentManager").GetComponent<EncampmentManager>();
        allResourceTiles = GameObject.FindGameObjectsWithTag("ResourceTile");
    }

    public void FindResourceTilesWithinRange()
    {
        encampmentManager.resourceTilesWithinRange.Clear();
        Collider2D collider = GameObject.Find("ResourceCollider").GetComponent<Collider2D>();
        foreach (GameObject tile in allResourceTiles)
        {
            //Does it intersect with the player collider
            if (tile.GetComponent<Collider2D>().bounds.Intersects(collider.bounds))
            {
                encampmentManager.resourceTilesWithinRange.Add(tile);
                tile.GetComponent<ResourceTile>().isWithinRange = true;
            }
        }
    }
}
