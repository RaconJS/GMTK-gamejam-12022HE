using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{

    public GameObject weaponBase;
    public static KeyCode spawn = KeyCode.H;
    public static Sprite[] meleeWeaponSprites;
    public static Sprite[] rangedWeaponSprites;
    public static List<GameObject> weapons = new List<GameObject>();
    public static List<GameObject> projectiles = new List<GameObject>();
    private static int requestedWeapons = 0;
    private static int waitForDice = 0;
    private static DieLauncher diceGun;
    private List<Vector3> weaponPositions = new List<Vector3>();
    private List<int> diceResults = new List<int>();

    public void spawnWeapon(string weapon, int level, Vector3 position, Quaternion rotation)
    {
        spawnWeapon(weapon, level, position, rotation, false, new Vector2(0, 0));
    }
    
    public void spawnWeapon(string weapon, int level, Vector3 position, Quaternion rotation, bool projectile, Vector3 velocity)
    {
        GameObject tempWeapon;

        Debug.Log(weapon);
        Debug.Log(level);
        Debug.Log(position.ToString());

        if (projectile)
        {
            weapon += "_projectile";
            tempWeapon = projectiles.Find(i => i.name == weapon + "_" + level);
        }
        else
        {
            tempWeapon = weapons.Find(i => i.name == weapon + "_" + level);
        }

        if (tempWeapon == null)
        {
            Debug.LogWarning(weapon + " level " + level + " does not exist(?). Has my code broken, or has yours?");
            return;
        }

        GameObject weaponInstance = Instantiate(tempWeapon, position, rotation);
        weaponInstance.SetActive(true);
        weaponInstance.transform.parent = transform;
        weaponInstance.GetComponent<Rigidbody2D>().velocity = velocity;
        weaponInstance.AddComponent<PolygonCollider2D>();
    }

    public void spawnProjectile(string weapon, int level, Vector3 position, Quaternion rotation, Vector3 velocity, GameObject parentObject)
    {
        GameObject tempProjectile;

        Debug.Log(weapon);
        Debug.Log(level);
        Debug.Log(position.ToString());

        weapon += "_projectile";
        tempProjectile = projectiles.Find(i => i.name == weapon + "_" + level);

        if (tempProjectile == null)
        {
            Debug.LogWarning(weapon + " level " + level + " does not exist(?). Has my code broken, or has yours?");
            return;
        }

        GameObject weaponInstance = Instantiate(tempProjectile, position, rotation);
        weaponInstance.SetActive(true);
        weaponInstance.transform.parent = transform;
        //weaponInstance.transform.parent = parentObject.transform;
        weaponInstance.GetComponent<Rigidbody2D>().velocity = velocity;
        weaponInstance.AddComponent<PolygonCollider2D>();
        Physics2D.IgnoreCollision(weaponInstance.GetComponent<Collider2D>(), parentObject.GetComponent<Collider2D>(), true);
    }

    public void spawnRandomWeapon(Vector3 position)
    {

        requestedWeapons++;
        weaponPositions.Add(position);
        diceGun.rollDice();
        diceGun.rollDice();
        diceGun.rollDice(1);
        diceGun.rollDice(2);
        waitForDice += 4;

    }

    // Start is called before the first frame update
    void Start()
    {

        diceGun = GameObject.Find("Dice Gun").GetComponent<DieLauncher>();
        meleeWeaponSprites = Resources.LoadAll<Sprite>("Weapons/Melee");
        rangedWeaponSprites = Resources.LoadAll<Sprite>("Weapons/Ranged");

        foreach (Sprite s in meleeWeaponSprites)
        {

            string[] name = s.name.Split('_');
            GameObject weapon = Instantiate(weaponBase, new Vector3(0,0,0), Quaternion.identity);
            weapon.name = s.name;
            weapon.SetActive(false);
            weapon.GetComponent<SpriteRenderer>().sprite = s;
            weapon.GetComponent<WeaponHandler>().setType(name[0]);
            weapon.GetComponent<WeaponHandler>().setLevel(Int32.Parse(name[name.Length-1]));
            weapon.GetComponent<Rigidbody2D>().mass = weight(name[0]);
            weapon.transform.parent = transform;
            weapons.Add(weapon);

        }

        foreach (Sprite s in rangedWeaponSprites)
        {
            string[] name = s.name.Split('_');

            switch (name[1])
            {
                case "A":

                    GameObject weapon = Instantiate(weaponBase, new Vector3(0, 0, 0), Quaternion.identity);
                    weapon.name = name[0] + "_" + name[2];//s.name;
                    weapon.SetActive(false);
                    weapon.GetComponent<SpriteRenderer>().sprite = s;
                    weapon.GetComponent<WeaponHandler>().setRanged(true);
                    weapon.GetComponent<WeaponHandler>().setType(name[0]);
                    weapon.GetComponent<WeaponHandler>().setLevel(Int32.Parse(name[name.Length - 1]));
                    weapon.transform.parent = transform;
                    weapons.Add(weapon);

                    break;
                case "proj":

                    GameObject projectile = Instantiate(weaponBase, new Vector3(0, 0, 0), Quaternion.identity);
                    projectile.name = name[0] + "_projectile_" + name[2];
                    projectile.SetActive(false);
                    projectile.GetComponent<SpriteRenderer>().sprite = s;
                    projectile.GetComponent<WeaponHandler>().setProjectile(true);
                    projectile.GetComponent<WeaponHandler>().setType("arrow");
                    projectile.GetComponent<WeaponHandler>().setLevel(Int32.Parse(name[name.Length - 1]));
                    projectile.transform.parent = transform;
                    projectiles.Add(projectile);
                    weapons.Find(i => i.name == name[0] + "_" + name[2]).GetComponent<WeaponHandler>().addProjectile(name[0]);

                    break;
                default:

                    weapons.Find(i => i.name == name[0] + "_" + name[2]).GetComponent<WeaponHandler>().addSpriteFrame(s);

                    break;
            }

        }

    }

    private static float weight(string type)
    {

        switch (type)
        {
            case "sword":
                return 1f;
            case "dagger":
                return 0.2f;
            case "rapier":
                return 0.4f;
            case "bow":
                return 0.3f;
            case "arrow":
                return 0.1f;
            default:
                return 0f;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(spawn))
        {
            spawnRandomWeapon(new Vector3(diceGun.transform.position.x, diceGun.transform.position.y + 2, 0));
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            spawnWeapon("bow", 3, new Vector3(diceGun.transform.position.x, diceGun.transform.position.y + 2, 0), Quaternion.identity);
        }

        if (requestedWeapons > 0 && diceGun.diceOutput.Count > 0)
        {

            diceResults.Add(diceGun.diceOutput[0]);
            diceGun.diceOutput.RemoveAt(0);
            waitForDice--;

            if (waitForDice % 4 == 0)
            {

                string weapon = "";
                int level = 1;
                bool projectile = false;

                if (diceResults[0] == diceResults[1])
                {
                    switch (diceResults[0])
                    {
                        case 1:
                            weapon = "bow";
                            projectile = true;
                            break;
                        case 6:
                            level = 3;
                            break;
                        default:
                            level = 2;
                            break;
                    }
                }

                if (diceResults[2] == 6 && weapon == "")
                {
                    weapon = "bow";
                }

                if (weapon == "")
                {
                    switch (diceResults[3])
                    {
                        case 1:
                        case 5:
                            weapon = "dagger";
                            break;
                        case 2:
                        case 3:
                            weapon = "sword";
                            break;
                        case 4:
                        case 6:
                            weapon = "rapier";
                            break;
                        default:
                            weapon = "bow";
                            projectile = true;
                            break;
                    }
                }

                requestedWeapons--;
                spawnWeapon(weapon, level, weaponPositions[0], Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 360)), projectile, new Vector2(0, 0));
                weaponPositions.RemoveAt(0);
                diceResults = new List<int>();

            }

        }

    }

}
