using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    private bool weaponTouching = true;
    private int weaponDamage = 2;
    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            GameObject.Find("player");
        }

    }

    private Vector3 player_position;
    private string weapon_type;
    private GameObject player;

    // Update is called once per frame
    void Update()
    {
    }

    //this sends a damage message for the health for the enemy
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            col.gameObject.SendMessage("ApplyDamageEnemy", 10);
        }
    }
}


 