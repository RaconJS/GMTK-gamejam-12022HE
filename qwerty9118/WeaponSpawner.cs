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
    public static List<GameObject> meleeWeapons = new List<GameObject>();
    public static List<GameObject> rangedWeapons = new List<GameObject>();
    private static int requestedWeapons = 0;
    private static int waitForDice = 0;
    private static DieLauncher diceGun;
    private List<Vector3> weaponPositions = new List<Vector3>();
    private List<int> diceResults = new List<int>();

    public void spawnWeapon(string weapon, int level, Vector3 position)
    {
        GameObject tempWeapon;

        if (weapon == "bow")
        {
            tempWeapon = rangedWeapons.Find(i => i.GetComponent<SpriteRenderer>().sprite.name == weapon + "_A_" + level);
        }
        else
        {
            tempWeapon = meleeWeapons.Find(i => i.GetComponent<SpriteRenderer>().sprite.name == weapon + "_" + level);
        }

        GameObject weaponInstance = Instantiate(tempWeapon, position, Quaternion.identity);
        weaponInstance.SetActive(true);
        weaponInstance.AddComponent<PolygonCollider2D>();
        weaponInstance.transform.parent = transform;
    }

    public void spawnRandomWeapon(Vector3 position)
    {

        requestedWeapons++;
        weaponPositions.Add(position);
        diceGun.rollDice();
        diceGun.rollDice();
        diceGun.rollDice();
        diceGun.rollDice();
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
            weapon.SetActive(false);
            weapon.GetComponent<SpriteRenderer>().sprite = s;
            weapon.GetComponent<WeaponHandler>().setType(name[0]);
            weapon.GetComponent<WeaponHandler>().setLevel(Int32.Parse(name[name.Length-1]));
            weapon.transform.parent = transform;
            meleeWeapons.Add(weapon);

        }

        int count = -1;
        foreach (Sprite s in rangedWeaponSprites)
        {
            string[] name = s.name.Split('_');

            switch (name[1])
            {
                case "A":

                    GameObject weapon = Instantiate(weaponBase, new Vector3(0, 0, 0), Quaternion.identity);
                    weapon.SetActive(false);
                    weapon.GetComponent<SpriteRenderer>().sprite = s;
                    weapon.GetComponent<WeaponHandler>().setRanged(true);
                    weapon.GetComponent<WeaponHandler>().setType(name[0]);
                    weapon.GetComponent<WeaponHandler>().setLevel(Int32.Parse(name[name.Length - 1]));
                    weapon.transform.parent = transform;
                    rangedWeapons.Add(weapon);
                    count++;

                    break;
                case "B":

                    rangedWeapons[count].GetComponent<WeaponHandler>().addLoadedSprite(s);

                    break;
                case "C":

                    GameObject projectile = Instantiate(weaponBase, new Vector3(0, 0, 0), Quaternion.identity);
                    projectile.SetActive(false);
                    projectile.GetComponent<SpriteRenderer>().sprite = s;
                    projectile.GetComponent<WeaponHandler>().setType("arrow");
                    projectile.GetComponent<WeaponHandler>().setLevel(Int32.Parse(name[name.Length - 1]));
                    projectile.transform.parent = transform;
                    meleeWeapons.Add(projectile);
                    rangedWeapons[count].GetComponent<WeaponHandler>().addProjectile(projectile);

                    break;
                default:
                    break;
            }

        }

    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(spawn))
        {
            spawnRandomWeapon(new Vector3(diceGun.transform.position.x, diceGun.transform.position.y, 0));
        }

        if (requestedWeapons > 0 && diceGun.diceOutput.Count > 0)
        {

            diceResults.Add(diceGun.diceOutput[0]);
            diceGun.diceOutput.RemoveAt(0);
            waitForDice--;

            if (waitForDice % 4 == 0)
            {

                string weapon = "";
                int level = 0;
                //bool ranged = false;

                if (diceResults[0] == diceResults[1])
                {
                    switch (diceResults[0])
                    {
                        case 1:
                            weapon = "arrow";
                            break;
                        case 6:
                            level = 3;
                            break;
                        default:
                            level = 2;
                            break;
                    }
                }
                else
                {
                    level = 1;
                }

                if (diceResults[2] == 6)
                {
                    //ranged = true;
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
                            weapon = "arrow";
                            break;
                    }
                }

                requestedWeapons--;
                spawnWeapon(weapon, level, weaponPositions[0]);
                weaponPositions.RemoveAt(0);
                diceResults = new List<int>();

            }

        }

    }

}
