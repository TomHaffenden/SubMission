using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {

    public GameObject Submarine;
    public InventoryDisplay inventoryDisplay;

    private int money;

    public GameObject UpgradeSub1;
    public GameObject UpgradeSub2;
    public GameObject UpgradeSub3;
    public GameObject UpgradeFuel1;
    public GameObject UpgradeFuel2;
    public GameObject UpgradeFuel3;
    public GameObject UpgradeLight1;
    public GameObject UpgradeLight2;
    public GameObject UpgradeLight3;
    public GameObject UpgradePipe1;
    public GameObject UpgradePipe2;
    public GameObject UpgradePipe3;
    public GameObject UpgradeSonar1;
    public GameObject UpgradeSonar2;
    public GameObject UpgradeSonar3;
    public GameObject UpgradeDrill1;
    public GameObject UpgradeDrill2;
    public GameObject UpgradeDrill3;

    // Use this for initialization
    void Start () {
        money = 0;
        
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown("m"))
        {
            money += 1000;
        }
        transform.Find("GoldAmountText").GetComponent<Text>().text = money.ToString();
    }

    public void SellAll()
    {
        foreach(OreType type in Submarine.GetComponent<Sub>().Inventory)
            money += Ore.GetValue(type);
        Submarine.GetComponent<Sub>().Inventory.Clear();
        inventoryDisplay.UpdateOreList(Submarine.GetComponent<Sub>().Inventory);
    }

    public void UpgradeSubI()
    {
        if (UpgradeSub1.GetComponent<Button>().interactable && money >= 250)
        {
            UpgradeSub1.GetComponent<Button>().interactable = false;
            //Upgrade code
            Submarine.GetComponent<Sub>().UpgradeSub(Upgrade.Level1);
            money -= 250;
        }
    }
    public void UpgradeSubII()
    {
        if (!UpgradeSub1.GetComponent<Button>().interactable && UpgradeSub2.GetComponent<Button>().interactable && money >= 750)
        {
            UpgradeSub2.GetComponent<Button>().interactable = false;
            //Upgrade code
            Submarine.GetComponent<Sub>().UpgradeSub(Upgrade.Level2);
            money -= 750;
        }
    }
    public void UpgradeSubIII()
    {
        if (!UpgradeSub1.GetComponent<Button>().interactable && !UpgradeSub2.GetComponent<Button>().interactable && UpgradeSub3.GetComponent<Button>().interactable && money >= 1500)
        {
            UpgradeSub3.GetComponent<Button>().interactable = false;
            //Upgrade code
            Submarine.GetComponent<Sub>().UpgradeSub(Upgrade.Level3);
            money -= 1500;
        }
    }
    public void UpgradeFuelI()
    {
        if (UpgradeFuel1.GetComponent<Button>().interactable && money >= 150)
        {
            UpgradeFuel1.GetComponent<Button>().interactable = false;
            //Upgrade code
            Submarine.GetComponent<Sub>().UpgradeFuel(Upgrade.Level1);
            money -= 150;
        }
    }
    public void UpgradeFuelII()
    {
        if (!UpgradeFuel1.GetComponent<Button>().interactable && UpgradeFuel2.GetComponent<Button>().interactable && money >= 550)
        {
            UpgradeFuel2.GetComponent<Button>().interactable = false;
            //Upgrade code
            Submarine.GetComponent<Sub>().UpgradeFuel(Upgrade.Level2);
            money -= 550;
        }
    }
    public void UpgradeFuelIII()
    {
        if (!UpgradeFuel1.GetComponent<Button>().interactable && !UpgradeFuel2.GetComponent<Button>().interactable && UpgradeFuel3.GetComponent<Button>().interactable && money >= 1250)
        {
            UpgradeFuel3.GetComponent<Button>().interactable = false;
            //Upgrade code
            Submarine.GetComponent<Sub>().UpgradeFuel(Upgrade.Level3);
        }
    }
    public void UpgradeLightI()
    {
        if (UpgradeLight1.GetComponent<Button>().interactable && money >= 100)
        {
            UpgradeLight1.GetComponent<Button>().interactable = false;
            //Upgrade code
            Submarine.GetComponent<Sub>().UpgradeLight(Upgrade.Level1);
            money -= 100;
        }
    }
    public void UpgradeLightII()
    {
        if (!UpgradeLight1.GetComponent<Button>().interactable && UpgradeLight2.GetComponent<Button>().interactable && money >= 300)
        {
            UpgradeLight2.GetComponent<Button>().interactable = false;
            //Upgrade code
            Submarine.GetComponent<Sub>().UpgradeLight(Upgrade.Level2);
            money -= 300;
        }
    }
    public void UpgradeLightIII()
    {
        if (!UpgradeLight1.GetComponent<Button>().interactable && !UpgradeLight2.GetComponent<Button>().interactable && UpgradeLight3.GetComponent<Button>().interactable && money >= 800)
        {
            UpgradeLight3.GetComponent<Button>().interactable = false;
            //Upgrade code
            Submarine.GetComponent<Sub>().UpgradeLight(Upgrade.Level3);
            money -= 800;
        }
    }
    public void UpgradePipeI()
    {
        if (UpgradePipe1.GetComponent<Button>().interactable && money >= 285)
        {
            UpgradePipe1.GetComponent<Button>().interactable = false;
            //Upgrade code
            Submarine.GetComponent<Sub>().UpgradePipe(Upgrade.Level1);
            money -= 285;
        }
    }
    public void UpgradePipeII()
    {
        if (!UpgradePipe1.GetComponent<Button>().interactable && UpgradePipe2.GetComponent<Button>().interactable && money >= 650)
        {
            UpgradePipe2.GetComponent<Button>().interactable = false;
            //Upgrade code
            Submarine.GetComponent<Sub>().UpgradePipe(Upgrade.Level2);
            money -= 650;
        }
    }
    public void UpgradePipeIII()
    {
        if (!UpgradePipe1.GetComponent<Button>().interactable && !UpgradePipe2.GetComponent<Button>().interactable && UpgradePipe3.GetComponent<Button>().interactable && money >= 1350)
        {
            UpgradePipe3.GetComponent<Button>().interactable = false;
            //Upgrade code
            Submarine.GetComponent<Sub>().UpgradePipe(Upgrade.Level3);
            money -= 1350;
        }
    }
    public void UpgradeSonarI()
    {
        if (UpgradeSonar1.GetComponent<Button>().interactable && money >= 225)
        {
            UpgradeSonar1.GetComponent<Button>().interactable = false;
            //Upgrade code
            Submarine.GetComponent<Sub>().UpgradeSonar(Upgrade.Level1);
            money -= 225;
        }
    }
    public void UpgradeSonarII()
    {
        if (!UpgradeSonar1.GetComponent<Button>().interactable && !UpgradeSonar2.GetComponent<Button>().interactable && UpgradeSonar3.GetComponent<Button>().interactable && money >= 950)
        {
            UpgradeSonar3.GetComponent<Button>().interactable = false;
            //Upgrade code
            Submarine.GetComponent<Sub>().UpgradeSonar(Upgrade.Level2);
            money -= 950;
        }
    }
    public void UpgradeSonarIII()
    {
        if (!UpgradeSonar1.GetComponent<Button>().interactable && UpgradeSonar2.GetComponent<Button>().interactable && money >= 575)
        {
            UpgradeSonar2.GetComponent<Button>().interactable = false;
            //Upgrade code
            Submarine.GetComponent<Sub>().UpgradeSonar(Upgrade.Level3);
            money -= 575;
        }
    }
    public void UpgradeDrillI()
    {
        if (UpgradeDrill1.GetComponent<Button>().interactable && money >= 300)
        {
            UpgradeFuel1.GetComponent<Button>().interactable = false;
            //Upgrade code
            Submarine.GetComponent<Sub>().UpgradeDrill(Upgrade.Level1);
            money -= 300;
        }
    }
    public void UpgradeDrillII()
    {
        if (!UpgradeDrill1.GetComponent<Button>().interactable && UpgradeDrill2.GetComponent<Button>().interactable && money >= 675)
        {
            UpgradeDrill2.GetComponent<Button>().interactable = false;
            //Upgrade code
            Submarine.GetComponent<Sub>().UpgradeDrill(Upgrade.Level2);
            money -= 675;
        }
    }
    public void UpgradeDrillIII()
    {
        if (!UpgradeDrill1.GetComponent<Button>().interactable && !UpgradeDrill2.GetComponent<Button>().interactable && UpgradeDrill3.GetComponent<Button>().interactable && money >= 1350)
        {
            UpgradeDrill3.GetComponent<Button>().interactable = false;
            //Upgrade code
            Submarine.GetComponent<Sub>().UpgradeDrill(Upgrade.Level3);
            money -= 1350;
        }
    }
}
