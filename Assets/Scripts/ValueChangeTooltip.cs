using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueChangeTooltip : MonoBehaviour
{
    public Text valueText;
    public Color gainColor;
    public Color lossColor;
    public Color changeColor;
    public bool isFloatingText;
    public GameObject toInstantiate;
    float floatSpeed = 0.01f;
    bool justActivated = false;
    Image I;

    void Awake()
    {
        I = GetComponent<Image>();
    }

    void Update()
    {
        if (isFloatingText)
        {
            if (valueText.color.a > 0)
            {
                float newY = transform.position.y + floatSpeed;
                transform.position = new Vector2(transform.position.x, newY);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    public void Activate(int value, string change)
    {
        StartCoroutine(SendTooltipNumber(value, change));
    }
    IEnumerator SendTooltipNumber(int value, string change)
    {
        valueText.color = new Color(valueText.color.r, valueText.color.g, valueText.color.b, 0);
        I.color = new Color(I.color.r, I.color.g, I.color.b, 0);
        GameObject newText = Instantiate(toInstantiate, GameObject.Find("Canvas").transform);
        ValueChangeTooltip newTextScript = newText.GetComponent<ValueChangeTooltip>();
        newTextScript.StopAllCoroutines();
        if (change == "gain")
        {
            newTextScript.valueText.color = gainColor;
            newTextScript.valueText.text = "+" + value.ToString();
        }
        else if (change == "loss")
        {
            newTextScript.valueText.color = lossColor;
            newTextScript.valueText.text = "-" + value.ToString();
        }
        else
        {
            newTextScript.valueText.color = changeColor;
            newTextScript.valueText.text = value.ToString();
        }
        newTextScript.valueText.color = new Color(newTextScript.valueText.color.r, newTextScript.valueText.color.g, newTextScript.valueText.color.b, 0.9f);
        newTextScript.isFloatingText = true;
        newTextScript.I.color = new Color(I.color.r, I.color.g, I.color.b, 0.8f);
        newTextScript.StartCoroutine(FadeEffect(newTextScript.gameObject.GetComponent<Image>(), newTextScript.valueText));
        yield return null;
    }

    IEnumerator FadeEffect(Image I, Text t)
    {
        // loop over 1.5 second backwards
        for (float i = 1.5f; i >= -0.1; i -= Time.deltaTime)
        {
            // set color with i as alpha
            I.color = new Color(I.color.r, I.color.g, I.color.b, i);
            t.color = new Color(t.color.r, t.color.g, t.color.b, i);
            yield return null;
        }
    }
}
