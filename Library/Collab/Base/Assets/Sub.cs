using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Upgrade
{
    Level0,
    Level1,
    Level2,
    Level3
}

public class Sub : MonoBehaviour
{
    public float WaterLevel;
    public Camera camera;
    private float speed;
    private Upgrade DrillLevel;
    public float Fuel;
    public int Health;
    private Vector2 move;
    public float MaxFuel;
    public int MaxHealth;
    public float SonarDistance;
    private GameObject child;
    private MoveMoment movement = MoveMoment.Submarine;
    public Text EnterText;
    public Text Drill;
    public LineRenderer line;
    public ParticleSystem particle;
    public GameObject spotLight;
    private float drill;
    private bool _sonarPulse;
    private List<GameObject> oresInRange; //= GetOreList

    public List<OreType> Inventory;
    public int InventorySize;
    public InventoryDisplay inventoryDisplay;

    public AudioSource sonarSound;
    public AudioSource collisonSound;
    public AudioSource DrillSound;

    enum MoveMoment
    {
        Submarine,
        Player,
    };

    // Use this for initialization
    void Start()
    {
        Fuel = 500;
        Health = 50;
        speed = 2.0f;
        MaxFuel = 500;
        MaxHealth = 50;
        child = this.gameObject.transform.GetChild(0).gameObject;
        _sonarPulse = false;
        InventorySize = 30;
        drill = 2.5f;
        oresInRange = new List<GameObject>();
        DrillLevel = Upgrade.Level0;
        SonarDistance = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {

        float rotationw = transform.rotation.z;
        switch (movement)
        {
            case MoveMoment.Submarine:
                
                child.GetComponent<Rigidbody2D>().gravityScale = 0;
                Vector3 pos = transform.position;
                pos.z = child.transform.position.z;
                child.transform.position = pos;
                child.GetComponent<Rigidbody2D>().rotation = 0;
                //child.transform.position = transform.position;
                EnterText.text = " ";
                if (Fuel > 0)
                {
                    if (transform.position.y > WaterLevel)
                    {
                        float value = (WaterLevel + 0.1f) - transform.position.y;
                        if (value > 3.0f)
                            this.GetComponent<Rigidbody2D>().velocity -= new Vector2(0, (Math.Abs(value) / 150.0f));
                        else
                            this.GetComponent<Rigidbody2D>().velocity -= new Vector2(0, 0.1f);
                    }
                    else if (Fuel > 0)
                    {
                        if (Input.GetKey(KeyCode.LeftShift))
                        {
                            speed = 10.0f;
                        }
                        else
                        {
                            speed = 2.0f;
                        }

                        move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
                        this.GetComponent<Rigidbody2D>().velocity += move * speed * Time.deltaTime;
                        if (move.x < 0)
                        {
                            if (Math.Abs(GetComponent<Rigidbody2D>().rotation) < 1.0f &&
                                GetComponent<Rigidbody2D>().angularVelocity < 3.0f)
                            {
                                transform.localScale = new Vector3(-0.9f, 0.9f, 1.0f);
                                GetComponent<Rigidbody2D>().rotation = 0.0f;
                            }

                        }
                        else if (move.x > 0)
                        {
                            if (Math.Abs(GetComponent<Rigidbody2D>().rotation) < 1.0f &&
                                GetComponent<Rigidbody2D>().angularVelocity < 3.0f)
                            {
                                transform.localScale = new Vector3(0.9f, 0.9f, 1.0f);
                                GetComponent<Rigidbody2D>().rotation = 0.0f;
                            }

                        }

                        if (move != new Vector2())
                        {
                            Fuel -= Time.deltaTime * speed * 2;
                            particle.startLifetime = 2;
                        }
                        else
                        {
                            particle.startLifetime = 0;
                        }
                    }
                    else
                    {
                        this.GetComponent<Rigidbody2D>().velocity -= new Vector2(0, 0.1f);
                    }

                    if (Input.GetKeyDown("q"))
                    {
                        movement = MoveMoment.Player;
                        foreach (var ore in GlobalOre.AllOres)
                        {
                            if (ore.activeSelf && Vector3.Distance(ore.gameObject.transform.position, child.transform.position) < gameObject.GetComponent<DistanceJoint2D>().distance + 3)
                            {
                                oresInRange.Add(ore);
                            }
                        }
                    }
                }

                break;
            case MoveMoment.Player:

                if (transform.position.y > WaterLevel)
                {
                    float value = (WaterLevel + 0.1f) - transform.position.y;
                    if (value > 3.0f)
                        this.GetComponent<Rigidbody2D>().velocity -= new Vector2(0, (Math.Abs(value) / 150.0f));
                    else
                        this.GetComponent<Rigidbody2D>().velocity -= new Vector2(0, 0.1f);
                }

                particle.startLifetime = 0;
                child.GetComponent<Rigidbody2D>().gravityScale = 0.1f;
                child.GetComponent<BoxCollider2D>().isTrigger = false;
                this.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
                this.GetComponent<Rigidbody2D>().angularVelocity = 0.0f;
                if (child.transform.position.y > WaterLevel + 0.2f)
                {
                    float value = (WaterLevel + 0.3f) - transform.position.y;
                    if (value > 3.0f)
                        child.GetComponent<Rigidbody2D>().velocity -= new Vector2(0, (Math.Abs(value) / 150.0f));
                    else
                        child.GetComponent<Rigidbody2D>().velocity -= new Vector2(0, 0.1f);
                }
                else
                {
                    move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

                    child.GetComponent<Rigidbody2D>().velocity += move * speed * Time.deltaTime;
                }

                Vector3 pz = Input.mousePosition;
                Drill.rectTransform.position = pz + new Vector3(-10, -10, 0);


                foreach (var ore in oresInRange)
                {
                    if (Inventory.Count >= 30)
                    {
                        Drill.text = " ";
                        break;
                    }

                    if (Vector3.Distance(ore.gameObject.transform.position, child.transform.position) < drill)
                    {
                        Vector2 mousePos = Input.mousePosition;
                        Camera c = Camera.main;

                        Vector3 p = c.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, c.nearClipPlane));
                        p.z = -1;

                        if (Vector3.Distance(ore.gameObject.transform.position, p) < 1)
                        {

                            Drill.text = "DRILL!";

                            if (Input.GetMouseButtonDown(0))
                            {

                                if (ore.gameObject.GetComponent<Ore>().Type == OreType.COAL ||
                                    ore.gameObject.GetComponent<Ore>().Type == OreType.IRON &&
                                    DrillLevel == Upgrade.Level0 || DrillLevel == Upgrade.Level1 ||
                                    DrillLevel == Upgrade.Level2 || DrillLevel == Upgrade.Level3)
                                {
                                    if (ore.gameObject.GetComponent<Ore>().Type == OreType.COAL)
                                    {
                                        for (int i = 0; i < ore.GetComponent<Ore>().Count; i++)

                                            if(Fuel < MaxFuel)
                                            {
                                                Fuel += 50;
                                            }

                                        DrillSound.Play();
                                    }
                                    else
                                    {
                                        for (int i = 0; i < ore.GetComponent<Ore>().Count; i++)
                                            Inventory.Add(OreType.IRON);
                                        DrillSound.Play();
                                    }
                                    ore.SetActive(false);
                                    oresInRange.Remove(ore);
                                    GlobalOre.AllOres.Remove(ore);

                                }

                                if (ore.gameObject.GetComponent<Ore>().Type == OreType.EMERALD ||
                                    ore.gameObject.GetComponent<Ore>().Type == OreType.RUBY &&
                                    DrillLevel == Upgrade.Level1 || DrillLevel == Upgrade.Level2 ||
                                    DrillLevel == Upgrade.Level3)
                                {
                                    if (ore.gameObject.GetComponent<Ore>().Type == OreType.RUBY)
                                    {
                                        for (int i = 0; i < ore.GetComponent<Ore>().Count; i++)
                                            Inventory.Add(OreType.RUBY);
                                    }
                                    else
                                    {
                                        for (int i = 0; i < ore.GetComponent<Ore>().Count; i++)
                                            Inventory.Add(OreType.EMERALD);
                                    }
                                    ore.SetActive(false);
                                    oresInRange.Remove(ore);
                                    GlobalOre.AllOres.Remove(ore);

                            }

                                if (ore.gameObject.GetComponent<Ore>().Type == OreType.SAPPHIRE ||
                                    ore.gameObject.GetComponent<Ore>().Type == OreType.GOLD &&
                                    DrillLevel == Upgrade.Level2 || DrillLevel == Upgrade.Level3)
                                {
                                    if (ore.gameObject.GetComponent<Ore>().Type == OreType.GOLD)
                                    {
                                        for (int i = 0; i < ore.GetComponent<Ore>().Count; i++)
                                            Inventory.Add(OreType.GOLD);
                                    }
                                    else
                                    {
                                        for (int i = 0; i < ore.GetComponent<Ore>().Count; i++)
                                            Inventory.Add(OreType.SAPPHIRE);
                                    }
                                    ore.SetActive(false);
                                    oresInRange.Remove(ore);
                                    GlobalOre.AllOres.Remove(ore);
                                }

                                if (ore.gameObject.GetComponent<Ore>().Type == OreType.DIAMOND &&
                                    DrillLevel == Upgrade.Level3)
                                {

                                    for (int i = 0; i < ore.GetComponent<Ore>().Count; i++)
                                        Inventory.Add(OreType.DIAMOND);
                                    ore.SetActive(false);
                                    oresInRange.Remove(ore);
                                    GlobalOre.AllOres.Remove(ore);
                                }

                                inventoryDisplay.UpdateOreList(Inventory);
                            }

                            break;
                        }
                    }
                    else { Drill.text = " "; }

                }
                if (move.x < 0)
                {
                    child.transform.localScale = new Vector3(transform.localScale.x, 1.0f, 1.0f);
                }
                else if (move.x > 0)
                {
                    child.transform.localScale = new Vector3(-transform.localScale.x, 1.0f, 1.0f);
                }

                if (Vector3.Distance(child.transform.position, this.transform.position) < 1.5)
                {
                    EnterText.text = "Q to enter";
                    if (Input.GetKeyDown("q"))
                    {
                       oresInRange.Clear();
                        movement = MoveMoment.Submarine;
                    }
                }
                else
                {
                    EnterText.text = " ";
                }

                break;
        }
        var poss = child.transform.position;
        poss.z = line.GetPosition(1).z;
        line.SetPosition(1, poss);
        line.SetPosition(0, transform.position);
        if (transform.position.y < -29.0f)
        {
            camera.GetComponent<Camera>().orthographicSize = 5.5f;
            spotLight.SetActive(true);
        }
        else
        {
            spotLight.SetActive(false);
            camera.GetComponent<Camera>().orthographicSize = 3.7f;
        }

        if (this.GetComponent<Rigidbody2D>().rotation > 2.0f)
        {
            this.GetComponent<Rigidbody2D>().AddTorque(-0.4f);
        }

        if (this.GetComponent<Rigidbody2D>().rotation < -2.0f)
        {
            this.GetComponent<Rigidbody2D>().AddTorque(0.4f);
        }

        if (this.GetComponent<Rigidbody2D>().rotation > 45.0f)
        {
            this.GetComponent<Rigidbody2D>().rotation = 45.0f;
            this.GetComponent<Rigidbody2D>().angularVelocity = 0;
        }

        if (this.GetComponent<Rigidbody2D>().rotation < -45.0f)
        {
            this.GetComponent<Rigidbody2D>().rotation = -45.0f;
            this.GetComponent<Rigidbody2D>().angularVelocity = 0;
        }


        if (Math.Abs(this.GetComponent<Rigidbody2D>().rotation) < 0.8f)
        {
            if (Math.Abs(this.GetComponent<Rigidbody2D>().angularVelocity) > 15.0f)
                this.GetComponent<Rigidbody2D>().angularVelocity +=
                    this.GetComponent<Rigidbody2D>().angularVelocity > 0 ? -3.5f : 3.5f;
            else
                this.GetComponent<Rigidbody2D>().angularVelocity = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Sonar();
            if (transform.position.y < -29.0f)
            {
                _sonarPulse = true;
                transform.Find("Sonar Pulse").GetComponent<ParticleSystem>().Play();
                sonarSound.Play();
            }
        }

        if (transform.position.y > -29.0f)
        {
            _sonarPulse = false;
            transform.Find("Sonar Pulse").GetComponent<ParticleSystem>().Stop();
            transform.Find("Sonar Pulse").GetComponent<ParticleSystem>().Clear();
            sonarSound.Stop();
        }

        if (_sonarPulse && !transform.Find("Sonar Pulse").GetComponent<ParticleSystem>().isEmitting)
        {
            Sonar();
            _sonarPulse = false;
            transform.Find("Sonar Pulse").GetComponent<ParticleSystem>().Stop();
        }

        if (Health <= 0 || Fuel <= 0)
        {
            transform.position = new Vector3(-7.48f, -0.18f, 1);
            GetComponent<Rigidbody2D>().angularVelocity = 0;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            Fuel = MaxFuel;
            Health = MaxHealth;
            Inventory.Clear();
        }

    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision");

        if (speed <= 2.0f)
        {
            Health -= 1;
            collisonSound.Play();
        }

        if (speed > 2.0f && speed < 4.0f)
        {
            Health -= 5;
            collisonSound.Play();
        }

        if (speed > 4.0f)
        {
            Health -= 10;
            collisonSound.Play();
        }

        if (Health < 0)
        {
            Health = 0;
        }

        Fuel -= Time.deltaTime * speed * 2;
    }

    public void Sonar()
    {
        List<GameObject> ores = GlobalOre.AllOres; //= GetOreList
        foreach (var ore in ores)
        {
            if (Vector3.Distance(ore.gameObject.transform.position, this.transform.position) < SonarDistance)
            {
                ore.SetActive(true);
            }
        }
    }

    public int getMaxHealth()
    {
        return MaxHealth;
    }

    public float getMaxFuel()
    {
        return MaxFuel;
    }

    public void UpgradeSub(Upgrade value)
    {
        switch (value)
        {
            case Upgrade.Level0:
                MaxHealth = 50;
                speed = 2.0f;
                InventorySize = 30;
                break;
            case Upgrade.Level1:
                MaxHealth = 100;
                speed = 4.0f;
                InventorySize = 50;
                break;
            case Upgrade.Level2:
                MaxHealth = 175;
                speed = 7.5f;
                InventorySize = 80;
                break;
            case Upgrade.Level3:
                MaxHealth = 300;
                speed = 10.0f;
                InventorySize = 120;
                break;
        }
    }

    public void UpgradeFuel(Upgrade value)
    {
        switch (value)
        {
            case Upgrade.Level0:
                MaxFuel = 500;
                break;
            case Upgrade.Level1:
                MaxFuel = 1000;
                break;
            case Upgrade.Level2:
                MaxFuel = 1500;
                break;
            case Upgrade.Level3:
                MaxFuel = 3000;
                break;
        }
    }

    public void UpgradeLight(Upgrade value)
    {
        switch (value)
        {
            case Upgrade.Level0:
                spotLight.GetComponent<Light>().spotAngle = 35.0f;
                break;
            case Upgrade.Level1:
                spotLight.GetComponent<Light>().spotAngle = 45.0f;
                break;
            case Upgrade.Level2:
                spotLight.GetComponent<Light>().spotAngle = 60.0f;
                break;
            case Upgrade.Level3:
                spotLight.GetComponent<Light>().spotAngle = 70.0f;
                break;
        }
    }

    public void UpgradePipe(Upgrade value)
    {
        switch (value)
        {
            case Upgrade.Level0:
                this.gameObject.GetComponent<DistanceJoint2D>().distance = 2.5f;
                break;
            case Upgrade.Level1:
                this.gameObject.GetComponent<DistanceJoint2D>().distance = 4;
                break;
            case Upgrade.Level2:
                this.gameObject.GetComponent<DistanceJoint2D>().distance = 5;
                break;
            case Upgrade.Level3:
                this.gameObject.GetComponent<DistanceJoint2D>().distance = 6;
                break;
        }
    }

    public void UpgradeSonar(Upgrade value)
    {
        switch (value)
        {
            case Upgrade.Level0:
                SonarDistance = 5.0f;
                break;
            case Upgrade.Level1:
                SonarDistance = 7.0f;
                break;
            case Upgrade.Level2:
                SonarDistance = 10.0f;
                break;
            case Upgrade.Level3:
                SonarDistance = 25.0f;
                break;
        }
    }

    public void UpgradeDrill(Upgrade value)
    {
        switch (value)
        {
            case Upgrade.Level0:
                DrillLevel = Upgrade.Level0;
                break;
            case Upgrade.Level1:
                DrillLevel = Upgrade.Level1;
                break;
            case Upgrade.Level2:
                DrillLevel = Upgrade.Level2;
                break;
            case Upgrade.Level3:
                DrillLevel = Upgrade.Level3;
                break;
        }
    }
}