using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnCounter : MonoBehaviour
{
    public Text counterText;
    TurnManager turnManager;
    // Start is called before the first frame update
    void Start()
    {
        turnManager = GameObject.Find("TurnManager").GetComponent<TurnManager>();
    }

    public void UpdateTurnCounter()
    {
        counterText.text = turnManager.turn.ToString(); 
    }
}
