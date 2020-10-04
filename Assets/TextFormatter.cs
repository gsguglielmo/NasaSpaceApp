using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFormatter : MonoBehaviour
{
    public int number;
    public string before;
    public string after;
    public bool hideWhenZero;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Text text = gameObject.GetComponent<Text>();
        if (number == 0 && hideWhenZero) {
            text.text = $"";
        } else {
            text.text = $"{before}{number}{after}";
        }
        
        
    }
}     
