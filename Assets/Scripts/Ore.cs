using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OreType
{
    COAL, //level 0
    IRON, //Drill lvl 0
    EMERALD,//Drill lvl 1
    RUBY,//level 1
    SAPPHIRE,// level 2
    GOLD, //Drill lvl2
    DIAMOND// level 3
}

public static class OreSprites
{
    public static Sprite Coal = Resources.Load<Sprite>("Coal");
    public static Sprite Iron = Resources.Load<Sprite>("Iron");
    public static Sprite Emerald = Resources.Load<Sprite>("Emerald");
    public static Sprite Ruby = Resources.Load<Sprite>("Ruby");
    public static Sprite Sapphire = Resources.Load<Sprite>("Lapis");
    public static Sprite Gold = Resources.Load<Sprite>("Gold");
    public static Sprite Diamond = Resources.Load<Sprite>("Diamond");
}

public class Ore : MonoBehaviour
{
    private OreType _type;
    public int Count;

	void Start ()
	{ }

    // Update is called once per frame
    void Update ()
    { }

    public OreType Type
    {
        get { return _type; }
        set
        {
            _type = value;
            switch (value)
            {
                case OreType.COAL:
                    GetComponent<SpriteRenderer>().sprite = OreSprites.Coal;
                    break;
                case OreType.IRON:
                    GetComponent<SpriteRenderer>().sprite = OreSprites.Iron;
                    break;
                case OreType.EMERALD:
                    GetComponent<SpriteRenderer>().sprite = OreSprites.Emerald;
                    break;
                case OreType.RUBY:
                    GetComponent<SpriteRenderer>().sprite = OreSprites.Ruby;
                    break;
                case OreType.SAPPHIRE:
                    GetComponent<SpriteRenderer>().sprite = OreSprites.Sapphire;
                    break;
                case OreType.GOLD:
                    GetComponent<SpriteRenderer>().sprite = OreSprites.Gold;
                    break;
                case OreType.DIAMOND:
                    GetComponent<SpriteRenderer>().sprite = OreSprites.Diamond;
                    break;
                default:
                    GetComponent<SpriteRenderer>().sprite = null;
                    break;
            }
        }
    }

    public static int GetValue(OreType type)
    {
        switch (type)
        {
            case OreType.COAL:
                return 5;
            case OreType.IRON:
                return 15;
            case OreType.EMERALD:
                return 30;
            case OreType.RUBY:
                return 35;
            case OreType.SAPPHIRE:
                return 40;
            case OreType.GOLD:
                return 50;
            case OreType.DIAMOND:
                return 100;
            default:
                return 0;
        }
    }
}
