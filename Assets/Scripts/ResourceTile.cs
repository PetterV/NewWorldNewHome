using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceTile : MonoBehaviour
{
    public string terrainType;
    public int minFood;
    public int maxFood;
    public int minWood;
    public int maxWood;
    public int minTools;
    public int maxTools;
    public int food = 0;
    public int wood = 0;
    public int tools = 0;
    public int tileWeight = 1;
    public Color closeColor;
    public Color defaultColor;

    Collider2D playerCollider;

    public bool isWithinRange = false;

    GameController gameController;
    void Awake()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        playerCollider = GameObject.Find("ResourceCollider").GetComponent<Collider2D>();
        RandomiseResources();
        GetComponent<SpriteRenderer>().color = defaultColor;
    }
    public void RandomiseResources()
    {
        wood = gameController.random.Next(minWood, maxWood + 1);
        food = gameController.random.Next(minFood, maxFood + 1);
        tools = gameController.random.Next(minTools, maxTools + 1);
    }

    void Update()
    {
        if (GetComponent<Collider2D>().bounds.Intersects(playerCollider.bounds) && GetComponent<SpriteRenderer>().color != closeColor)
        {
            GetComponent<SpriteRenderer>().color = closeColor;
        }
        else if (GetComponent<SpriteRenderer>().color == closeColor &! GetComponent<Collider2D>().bounds.Intersects(playerCollider.bounds))
        {
            GetComponent<SpriteRenderer>().color = defaultColor;
        }
    }
}
