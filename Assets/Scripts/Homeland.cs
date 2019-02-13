using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homeland : MonoBehaviour
{
    public Color closeColor;
    public Color defaultColor;

    Collider2D playerCollider;

    public bool isWithinRange = false;

    GameController gameController;
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        playerCollider = GameObject.Find("ResourceCollider").GetComponent<Collider2D>();
        GetComponent<SpriteRenderer>().color = defaultColor;
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
