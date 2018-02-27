using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background_Still : MonoBehaviour
{
    public Camera _Camera;
    public GameObject _Submarine;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    var Pos = _Submarine.transform.position;
	    Pos.z = _Camera.transform.position.z;
	    _Camera.transform.position = Pos;
	    Pos.z = this.transform.position.z;
        this.transform.position = Pos;
	}
}
