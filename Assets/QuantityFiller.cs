using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuantityFiller : MonoBehaviour
{
    public double maxSize;
    public double minSize;
    public int percentage;
    public GameObject bar;

    private float y_fixed;
    private RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = bar.GetComponent<RectTransform>();

        

        Vector2 vector = rectTransform.sizeDelta;
        y_fixed = vector.y;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (percentage > 100) percentage = 100;
        if (percentage < 0) percentage = 0;
        float perc = (float)percentage / 100;
        rectTransform.sizeDelta = new Vector2((float)maxSize * perc, y_fixed);
    }
}
