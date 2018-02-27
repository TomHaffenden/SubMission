using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barba : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (transform.position.y > 0.3f && GetComponent<Rigidbody2D>().velocity.y < 0.2f)
	        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, -transform.position.y);
	    if (transform.position.y < -0.06f)
	        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0.1f);

	    if (this.GetComponent<Rigidbody2D>().rotation > 2.0f - 5)
	    {
	        this.GetComponent<Rigidbody2D>().AddTorque(-0.4f);
	    }

	    if (this.GetComponent<Rigidbody2D>().rotation < -2.0f - 5)
	    {
	        this.GetComponent<Rigidbody2D>().AddTorque(0.4f);
	    }

	    if (this.GetComponent<Rigidbody2D>().rotation > 95.0f)
	    {
	        this.GetComponent<Rigidbody2D>().rotation = 75.0f;
	        this.GetComponent<Rigidbody2D>().angularVelocity = 0;
	    }

	    if (this.GetComponent<Rigidbody2D>().rotation < -95.0f)
	    {
	        this.GetComponent<Rigidbody2D>().rotation = -75.0f;
	        this.GetComponent<Rigidbody2D>().angularVelocity = 0;
	    }
    }
}
