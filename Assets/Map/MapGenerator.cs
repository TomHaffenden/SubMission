using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public enum DIRECTION
{
    UP,
    DOWN,
    LEFT,
    RIGHT
};

public class MapGenerator : MonoBehaviour
{
    public int Size;

    public GameObject[] Presets;

    public GameObject Minimap;
    public GameObject MiniMapPreset;
    private Vector2 MinimapMax;
    private Vector2 MinimapMin;

    private int _roomCount;
    private int _treeSize;

    private MapNode _root;
    private Vector2 _search;
    private List<MapNode> _searched;
    private List<Vector2> _worldArray;
    // Use this for initialization
    void Start ()
	{
	    _roomCount = 1;
	    _treeSize = 0;

        _root = new MapNode(PickRandomPreset());
	    _search = new Vector2();
	    _searched = new List<MapNode>();
	    _worldArray = new List<Vector2>();

	    MinimapMax = new Vector2();
        MinimapMin = new Vector2();

        Random.InitState((int)DateTime.Now.Ticks);

        GenerateMap(_root);

        _searched.Clear();
        TreeToArray(_root);
	    Transform content = Minimap.transform.GetChild(0);
	    content.GetComponent<RectTransform>().offsetMax = MinimapMax;
	    content.GetComponent<RectTransform>().offsetMin = MinimapMin;
	    content.GetChild(0).localPosition = new Vector3((Math.Abs(MinimapMax.x) > Math.Abs(MinimapMin.x)) ? -MinimapMax.x/2.0f : -MinimapMin.x/2.0f, -MinimapMin.y/2.0f, 1.0f);
	}

    private void GenerateMap(MapNode inNode)
    {
        _treeSize++;
        float roomSeed = Random.value;
        float endChance = (Size / _roomCount) * (_treeSize / _roomCount);
        if (!(_roomCount > Size && roomSeed > endChance))
        {
            for (int i = 0; i < 3; i++)
            {
                GenerateRoom(inNode, Random.value);
            }
        }

        _treeSize--;
    }

    private void GenerateRoom(MapNode inNode, float direction)
    {
        if (direction < 0.4f && !inNode.HasDown())
        {
            _search = new Vector2(inNode.X, inNode.Y - 1);
            _searched.Clear();
            if (!CheckForOverlap(_root))
            {
                MapNode down = new MapNode(PickRandomPreset(), inNode, DIRECTION.DOWN);
                inNode.Down = down;
                _roomCount++;
                GenerateMap(down);
            }
            else if (Random.value > 0.5f)
            {
                inNode.Down = _searched[_searched.Count - 1];
                _searched[_searched.Count - 1].Up = inNode;
            }
        }
        else if (direction < 0.7f && !inNode.HasRight())
        {
            _search = new Vector2(inNode.X + 1, inNode.Y);
            _searched.Clear();
            if (!CheckForOverlap(_root))
            {
                MapNode right = new MapNode(PickRandomPreset(), inNode, DIRECTION.RIGHT);
                inNode.Right = right;
                _roomCount++;
                GenerateMap(right);
            }
            else if (Random.value > 0.5f)
            {
                inNode.Right = _searched[_searched.Count-1];
                _searched[_searched.Count-1].Left = inNode;
            }
        }
        else if (direction < 0.9f && !inNode.HasLeft())
        {
            _search = new Vector2(inNode.X - 1, inNode.Y);
            _searched.Clear();
            if (!CheckForOverlap(_root))
            {
                MapNode left = new MapNode(PickRandomPreset(), inNode, DIRECTION.LEFT);
                inNode.Left = left;
                _roomCount++;
                GenerateMap(left);
            }
            else if (Random.value > 0.5f)
            {
                inNode.Left = _searched[_searched.Count - 1];
                _searched[_searched.Count - 1].Right = inNode;
            }
        }
        else if (!inNode.HasUp() && (inNode.Y + 1) < 0)
        {
            _search = new Vector2(inNode.X, inNode.Y + 1);
            _searched.Clear();
            if (!CheckForOverlap(_root))
            {
                MapNode up = new MapNode(PickRandomPreset(), inNode, DIRECTION.UP);
                inNode.Up = up;
                _roomCount++;
                GenerateMap(up);
            }
            else if (Random.value > 0.5f)
            {
                inNode.Up = _searched[_searched.Count - 1];
                _searched[_searched.Count - 1].Down = inNode;
            }
        }
    }

    private bool CheckForOverlap(MapNode inNode)
    {
        foreach (MapNode node in _searched)
        {
            if (inNode == node)
                return false;
        }
        _searched.Add(inNode);

        if (inNode.X == _search.x && inNode.Y == _search.y)
        {
            return true;
        }

        if (inNode.HasUp())
        {
            if (CheckForOverlap(inNode.Up))
                return true;
        }

        if (inNode.HasLeft())
        {
            if(CheckForOverlap((inNode.Left)))
                return true;
        }

        if (inNode.HasRight())
        {
            if (CheckForOverlap(inNode.Right))
                return true;
        }

        if (inNode.HasDown())
        {
            if (CheckForOverlap(inNode.Down))
                return true;
        }

        return false;
    }

    private GameObject PickRandomPreset()
    {
        return Instantiate(Presets[Random.Range(0, Presets.Length)]);
    }

    private void TreeToArray(MapNode inNode)
    {
        foreach (MapNode node in _searched)
        {
            if (inNode == node)
                return;
        }
        _searched.Add(inNode);

        GameObject blip = Instantiate(MiniMapPreset, Minimap.transform.GetChild(0).GetChild(0));
        blip.transform.localPosition = new Vector3(inNode.X*15, inNode.Y*10, 2);
        if (blip.transform.localPosition.x > MinimapMax.x)
        {
            MinimapMax.x = blip.transform.localPosition.x;
        }
        if (blip.transform.localPosition.y > MinimapMax.y)
        {
            MinimapMax.y = blip.transform.localPosition.y;
        }
        if (blip.transform.localPosition.x < MinimapMin.x)
        {
            MinimapMin.x = blip.transform.localPosition.x;
        }
        if (blip.transform.localPosition.y < MinimapMin.y)
        {
            MinimapMin.y = blip.transform.localPosition.y;
        }

        _worldArray.Add(new Vector2(inNode.X, inNode.Y));
        if (inNode.HasUp())
        {
            TreeToArray(inNode.Up);
        }

        if (inNode.HasDown())
        {
            TreeToArray(inNode.Down);
        }

        if (inNode.HasLeft())
        {
            TreeToArray(inNode.Left);
        }

        if (inNode.HasRight())
        {
            TreeToArray(inNode.Right);
        }

        return;
    }

    // Update is called once per frame
    void Update () {
		
	}
}


public static class GlobalOre
{
    public static List<GameObject> AllOres = new List<GameObject>();
}


public class MapNode
{
    private readonly GameObject _preset;

    private readonly List<GameObject> _ores;

    private MapNode _up = null;
    private MapNode _down = null;
    private MapNode _left = null;
    private MapNode _right = null;

    private readonly int _x;
    private readonly int _y;

    public MapNode(GameObject preset)
    {
        this._preset = preset;
        Up = null;
        _x = 0;
        _y = 0;
        this._preset.GetComponent<Transform>().position = new Vector3(48.75f, -36.25f, 0);
        _preset.transform.Find("Foreground").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sand");
        _preset.transform.Find("Background").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sand");
    }
    public MapNode(GameObject preset, MapNode parent, DIRECTION parentDirection)
    {
        this._preset = preset;
        switch (parentDirection)
        {
            case DIRECTION.UP:
                Down = parent;
                _x = parent.X;
                _y = parent.Y + 1;
                break;
            case DIRECTION.LEFT:
                Right = parent;
                _x = parent.X - 1;
                _y = parent.Y;
                break;
            case DIRECTION.RIGHT:
                Left = parent;
                _x = parent.X + 1;
                _y = parent.Y;
                break;
            case DIRECTION.DOWN:
                Up = parent;
                _x = parent.X;
                _y = parent.Y - 1;
                break;
            default:
                break;
        }

        _ores = new List<GameObject>
        {
            _preset.transform.Find("OreSpot1").gameObject,
            _preset.transform.Find("OreSpot2").gameObject,
            _preset.transform.Find("OreSpot3").gameObject,
            _preset.transform.Find("OreSpot4").gameObject,
            _preset.transform.Find("OreSpot5").gameObject,
            _preset.transform.Find("OreSpot6").gameObject
        };

        foreach (GameObject ore in _ores)
        {
            GenerateOre(ore.GetComponent<Ore>());
        }
        this._preset.GetComponent<Transform>().position = new Vector3(_x*20 + 48.75f, _y*12 - 36.25f, 0);
        if (Y >= -3)
        {
            _preset.transform.Find("Foreground").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sand");
            _preset.transform.Find("Background").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sand");

        }
        else if (Y >= -6)
        {
            _preset.transform.Find("Foreground").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Dirt");
            _preset.transform.Find("Background").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Dirt");
        }
    }

    private void GenerateOre(Ore ore)
    {
        int oreCount = Random.Range(0, 10);
        
        if (oreCount % 6 == 0)
            ore.Count = 3;
        else if (oreCount % 3 == 0)
            ore.Count = 2;
        else if (oreCount % 2 == 0)
            ore.Count = 1;
        else if (oreCount % 2 != 0)
            return;

        GlobalOre.AllOres.Add(ore.gameObject);

        if (Y < -6)
        {
            float oreSeed = Random.value;
            if (oreSeed >= 0.95f)
            {
                ore.Type = OreType.DIAMOND;
                return;
            }

            if (oreSeed >= 0.75f)
            {
                ore.Type = OreType.GOLD;
                return;
            }
        }

        if (Y < -2)
        {
            float oreSeed = Random.value;
            if (oreSeed >= 0.85f)
            {
                ore.Type = OreType.RUBY;
                return;
            }

            if (oreSeed >= 0.6f)
            {
                ore.Type = OreType.EMERALD;
                return;
            }

            if (oreSeed >= 0.45f)
            {
                ore.Type = OreType.SAPPHIRE;
                return;
            }
        }

        if (Random.value > 0.5f)
        {
            ore.Type = OreType.IRON;
            return;
        }


        ore.Type = OreType.COAL;
    }

    public int X
    {
        get { return _x; }
    }

    public int Y
    {
        get { return _y; }
    }

    public MapNode Up
    {
        get { return _up; }
        set
        {
            _up = value;
            _preset.transform.Find("TopExit").gameObject.SetActive(false);
        }
    }
    public MapNode Down {
        get { return _down; }
        set
        {
            _down = value;
            _preset.transform.Find("BottomExit").gameObject.SetActive(false);
        }
    }
    public MapNode Left {
        get { return _left; }
        set
        {
            _left = value;
            _preset.transform.Find("LeftExit").gameObject.SetActive(false);
        }
    }
    public MapNode Right {
        get { return _right; }
        set
        {
            _right = value;
            _preset.transform.Find("RightExit").gameObject.SetActive(false);
        }
    }

    public bool HasUp()
    {
        return (Up != null);
    }
    public bool HasDown()
    {
        return (Down != null);
    }
    public bool HasLeft()
    {
        return (Left != null);
    }
    public bool HasRight()
    {
        return (Right != null);
    }
}
