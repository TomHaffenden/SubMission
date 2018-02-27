using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pluse : MonoBehaviour
{
    private float value;
    private bool direction;

	// Use this for initialization
	void Start ()
	{
	    value = 1.0f;
	    direction = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    value += 0.5f * Time.deltaTime * (direction ? 1 : -1);
        GetComponent<Image>().color = new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g, GetComponent<Image>().color.b, value);
	    if (value < 0.6f) direction = true;
	    if (value >= 0.9f) direction = false;
	}
}
