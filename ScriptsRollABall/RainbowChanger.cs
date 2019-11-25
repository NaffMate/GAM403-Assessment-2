using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainbowChanger : MonoBehaviour
{
    // public variables
    public float speed;
    public Color startColor;
    public Color endColor;
    public bool repeatable;

    // private variables
    private float startTime;

    void Start()
    {
        // sets the start time to the immediate time at scene load
        startTime = Time.time;
    }

    void Update()
    {
        // Calls the ColorChange function. Allows for repeatable colour changing
        ColorChange();
    }

    // changes the colour of a given Game Object between startColor and endColor
    void ColorChange()
    {
        // changes the material colour over time
        GetComponent<Renderer>().material.color = Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time, 1));
    }
}


