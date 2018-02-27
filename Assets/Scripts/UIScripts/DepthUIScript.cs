using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DepthUIScript : MonoBehaviour {

    public Text DepthText;
    public GameObject Submarine;
    private float depth;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        int depth = (int)Submarine.transform.position.y;
	    depth = Math.Abs(depth);
        DepthText.text = depth.ToString() + "m";
    }
}
