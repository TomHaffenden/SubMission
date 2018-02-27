using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSign : MonoBehaviour
{

    public GameObject ShopPanel;
    public GameObject GameUI;
    public GameObject MainMenu;
    public Sub Submarine;

	// Use this for initialization
	void Start ()
	{ }
	
	// Update is called once per frame
	void Update ()
	{ }

    void OnMouseDown()
    {
        ShopPanel.gameObject.SetActive(true);
        GameUI.gameObject.SetActive(false);
        Submarine.Fuel = Submarine.MaxFuel;
        Submarine.Health = Submarine.MaxHealth;
    }

    public void CloseShop()
    {
        ShopPanel.gameObject.SetActive(false);
        GameUI.gameObject.SetActive(true);
        Submarine.Fuel = Submarine.MaxFuel;
        Submarine.Health = Submarine.MaxHealth;
    }

    public void CloseMainMenu()
    {
        MainMenu.gameObject.SetActive(false);
        GameUI.gameObject.SetActive(true);
    }
}
