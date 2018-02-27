using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay : MonoBehaviour
{
    public GameObject ScrollRect;
    public GameObject moneyOutput;

    private float _topTarget;

	// Use this for initialization
	void Start ()
	{
	    _topTarget = 711.1f;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    ScrollRect.GetComponent<RectTransform>().offsetMin = new Vector2(ScrollRect.GetComponent<RectTransform>().offsetMin.x, Mathf.Lerp(ScrollRect.GetComponent<RectTransform>().offsetMin.y, _topTarget, Time.deltaTime));
	}

    public void ToggleDisplay()
    {
        if (_topTarget == 711.1f)
        {
            ScrollRect.gameObject.SetActive(true);
            _topTarget = 55.05f;
        }
        else
        {
            ScrollRect.gameObject.SetActive(false);
            _topTarget = 711.1f;
            ScrollRect.GetComponent<RectTransform>().offsetMin = new Vector2(ScrollRect.GetComponent<RectTransform>().offsetMin.x, _topTarget);
        }
    }

    public void UpdateOreList(List<OreType> inventory)
    {
        int[] resourceCount = new int[6]
            {0, 0, 0, 0, 0, 0};
        int moneyTotal = 0;
    foreach (OreType ore in inventory)
        {
            switch (ore)
            {
                case OreType.IRON:
                    resourceCount[0]++;
                    moneyTotal += Ore.GetValue(OreType.IRON);
                    break;
                case OreType.EMERALD:
                    resourceCount[1]++;
                    moneyTotal += Ore.GetValue(OreType.EMERALD);
                    break;
                case OreType.RUBY:
                    resourceCount[2]++;
                    moneyTotal += Ore.GetValue(OreType.RUBY);
                    break;
                case OreType.SAPPHIRE:
                    resourceCount[3]++;
                    moneyTotal += Ore.GetValue(OreType.SAPPHIRE);
                    break;
                case OreType.GOLD:
                    resourceCount[4]++;
                    moneyTotal += Ore.GetValue(OreType.GOLD);
                    break;
                case OreType.DIAMOND:
                    resourceCount[5]++;
                    moneyTotal += Ore.GetValue(OreType.DIAMOND);
                    break;
            }
        }

        for (int i = 0; i < 6; i++)
        {
            ScrollRect.transform.GetChild(0).GetChild(i).GetChild(0).GetComponent<Text>().text = resourceCount[i].ToString();
        }

        moneyOutput.GetComponent<Text>().text = moneyTotal.ToString();

        transform.GetChild(0).GetComponent<Text>().text = inventory.Count.ToString();
    }
}
