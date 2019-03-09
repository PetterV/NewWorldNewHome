using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueChangeTooltipMovement : MonoBehaviour
{
    public Text valueText;
    public bool isFloatingText;
    float floatSpeed = 0.005f;
    public Image i;
    public Text t;
    
    void Awake()
    {
        i = GetComponent<Image>();
        t = GetComponentInChildren<Text>();
        isFloatingText = true;
        StartCoroutine(FadeEffect());
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

    IEnumerator FadeEffect()
    {
        // loop over 1.5 second backwards
        for (float f = 1.5f; f >= -0.1; f -= Time.deltaTime)
        {
            // set color with i as alpha
            i.color = new Color(i.color.r, i.color.g, i.color.b, f);
            t.color = new Color(t.color.r, t.color.g, t.color.b, f);
            yield return null;
        }
    }
}
