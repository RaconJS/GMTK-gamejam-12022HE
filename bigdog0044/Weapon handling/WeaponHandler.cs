using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
	//private bool weaponTouching = true;
	bool isPickedUp;
	//private GameObject playerHand;
	//private Vector3 player_position;
	private string weaponType;
	private int weaponLevel;
	private GameObject player;
	//private PolygonCollider2D collider;
	private bool isRanged;
	private Sprite loadedSprite;
	private GameObject projectile;
	private static int projFireCount = 0;


	private void Awake()
	{

		//collider = gameObject.AddComponent<PolygonCollider2D>();
		//collider.autoTiling = true;
		isPickedUp = false;
		weaponType = "sword";
		weaponLevel = 0;
		isRanged = false;
		player = GameObject.Find("player");
		//Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>(), false);

	}

	private int weaponDamage()
    {

        switch (weaponType)
        {
			case "dagger":
				return weaponLevel;
			case "sword":
				return weaponLevel * 2;
			case "rapier":
				return weaponLevel * (int) GetComponent<Rigidbody2D>().velocity.magnitude;
			case "bow":
				return 0;
			case "arrow":
				return weaponLevel * (int) (GetComponent<Rigidbody2D>().velocity.magnitude / 2);
			default:
				return 1;
		}

    }

	public void setType(string type)
    {
		weaponType = type;
	}

	public void setLevel(int level)
	{
		weaponLevel = level;
	}

	public void setRanged(bool isRanged1)
    {
		isRanged = isRanged1;
    }

	public void addLoadedSprite(Sprite sprite)
	{
		loadedSprite = sprite;
	}

	public void addProjectile(GameObject projectile1)
	{
		projectile = projectile1;
	}

	public void pickup(bool picked)
	{
		isPickedUp = picked;
	}

	// Update is called once per frame
	void Update()
	{
		if(isPickedUp)
		{

			//transform.position = player.GetComponent<playerTurn>().handPos;//playerHand.transform.position;
			//transform.rotation = player.GetComponent<playerTurn>().handRot;//playerHand.transform.rotation;
			GetComponent<Rigidbody2D>().velocity = ((player.GetComponent<playerTurn>().handPos + player.transform.position) - transform.position) * 10;
            //if (Mathf.Abs((player.GetComponent<playerTurn>().handRot % 360) - (GetComponent<Rigidbody2D>().rotation % 360)) > 5)
            {
				GetComponent<Rigidbody2D>().angularVelocity = player.GetComponent<playerTurn>().handRot - GetComponent<Rigidbody2D>().rotation;
			}

		}
        if (weaponType == "bow")
        {

            if (projFireCount == 10)
            {
				GameObject arrow = Instantiate(projectile, transform.position, transform.rotation);
				arrow.transform.parent = transform;
				arrow.GetComponent<Rigidbody2D>().velocity += new Vector2(transform.forward.x, transform.forward.y) * 10;
				SoundManagerScript.playSound("bowFire");
				projFireCount = 0;
			}
            else
            {
				projFireCount++;
            }

		}
		if (weaponType == "arrow" && GetComponent<Rigidbody>().velocity.magnitude < 0.1)
		{
			Destroy(gameObject, 5.0f);
		}
	}

	//this sends a damage message for the health for the enemy
	private void OnCollisionEnter2D(Collision2D collision)
    {
		var part = collision.gameObject;
		if (part.GetComponent<enermyTurn>() != null)
		{
			//if(playerHand.GetComponent<spin>().isSpinning)
			{
				//Physics2D.IgnoreCollision(part.GetComponent<Collider2D>(), GetComponent<Collider2D>(), true);
				part.GetComponent<enermyTurn>().hurt(weaponDamage());
                if (weaponType != "bow")
                {
					SoundManagerScript.playSound("swordAttack");
				}
			}
		}
		else if (part.GetComponent<playerTurn>() != null)
		{
			Physics2D.IgnoreCollision(part.GetComponent<Collider2D>(), GetComponent<Collider2D>(), true);

			if (!isPickedUp && weaponType != "arrow")
			{
				GameObject heldObject = player.GetComponent<playerTurn>().holdingWepn;

				if (player.GetComponent<playerTurn>().holdingWepn != gameObject)
                {
					player.GetComponent<playerTurn>().holdingWepn.GetComponent<WeaponHandler>().pickup(false);
					player.GetComponent<playerTurn>().holdingWepn.transform.parent = transform.parent;
					player.GetComponent<playerTurn>().holdingWepn.GetComponent<Rigidbody2D>().gravityScale = 1;
					player.GetComponent<playerTurn>().holdingWepn.GetComponent<Rigidbody2D>().velocity = Random.insideUnitCircle * 5;
					Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), player.GetComponent<playerTurn>().holdingWepn.GetComponent<Collider2D>(), false);
				}

				isPickedUp = true;
				//GameObject weapon = Instantiate(gameObject, transform.position, transform.rotation);
				transform.parent = player.transform;
				GetComponent<Rigidbody2D>().gravityScale = 0;
				GetComponent<WeaponHandler>().pickup(true);
				player.GetComponent<playerTurn>().holdingWepn = gameObject;
				Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>(), true);
				SoundManagerScript.playSound("pickUpItem");
				//gameObject.SetActive(false);
				//Destroy(gameObject, 2);

			}
		}
	}

    /*private void OnCollisionStay2D(Collision2D collision)
	{
		var part = collision.gameObject;
		if (part.GetComponent<playerTurn>() != null)
		{
			//Physics2D.IgnoreCollision(part.GetComponent<Collider2D>(), GetComponent<Collider2D>(), true);

			if (!isPickedUp && (
				player.GetComponent<playerTurn>().holdingWepn == null || (
				player.GetComponent<playerTurn>().holdingWepn.GetComponent<WeaponHandler>().weaponType != weaponType &&
				player.GetComponent<playerTurn>().holdingWepn.GetComponent<WeaponHandler>().weaponLevel != weaponLevel &&
				weaponType != "arrow")))
			{

				isPickedUp = true;
				GameObject weapon = Instantiate(gameObject, transform.position, transform.rotation);
				weapon.transform.parent = transform.parent;
				weapon.GetComponent<WeaponHandler>().pickup(true);
				player.GetComponent<playerTurn>().holdingWepn = weapon;
				Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), weapon.GetComponent<Collider2D>(), true);
				SoundManagerScript.playSound("pickUpItem");
				gameObject.SetActive(false);
				Destroy(gameObject, 2);

			}
		}
	}*/

}