using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour {

    public GameObject Submarine;

	// Use this for initialization
	void Start ()
    { }

    // Update is called once per frame
    void Update ()
    {
        this.GetComponent<Slider>().maxValue = Submarine.GetComponent<Sub>().MaxHealth;
        this.GetComponent<Slider>().value = Submarine.GetComponent<Sub>().Health;
    }
}
