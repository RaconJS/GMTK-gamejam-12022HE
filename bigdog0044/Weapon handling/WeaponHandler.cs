using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
	//private bool weaponTouching = true;
	private int weaponDamage = 2;
	bool isPickedUp=false;
	public Transform playerHand;
	// Start is called before the first frame update
	void Start()
	{
		isPickedUp=false;
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
		if(isPickedUp){
			transform.position=playerHand.transform.position;
			transform.rotation=playerHand.transform.rotation;
		}
	}

	//this sends a damage message for the health for the enemy
	private void OnTriggerEnter2D(Collider2D col)
	{
		var part=col.gameObject.GetComponent<enermyTurn>();
		if (part != null)
		{
			//if(playerHand.GetComponent<spin>().isSpinning)
			{
				part.hurt(weaponDamage);
				SoundManagerScript.playSound("swordAttack");
			}
			if (part.gameObject.tag == "player")
			{
				if (!isPickedUp)
				{
					isPickedUp = true;
					SoundManagerScript.playSound("pickUpItem");
				}
			}
		}
	}
}