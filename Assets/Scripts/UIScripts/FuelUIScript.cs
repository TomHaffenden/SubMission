using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelUIScript : MonoBehaviour {

    public Text currentFuel;
    public Text maxFuel;
    public GameObject Submarine;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        currentFuel.text = Submarine.GetComponent<Sub>().Fuel.ToString();
        maxFuel.text = "/" + Submarine.GetComponent<Sub>().MaxFuel.ToString();
    }
}
