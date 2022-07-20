using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
	//private bool weaponTouching = true;
	public bool isPickedUp;
	//private GameObject playerHand;
	//private Vector3 player_position;
	public string weaponType;
	public int weaponLevel;
	private GameObject player;
	//private PolygonCollider2D collider;
	public bool isRanged;
	public bool isProjectile;
	public Sprite unloadedSprite;
	public Sprite loadedSprite;
	public string projectile;
	public int projFireCount = 0;
	private WeaponSpawner weaponSpawner;


	private void Awake()
	{

		//collider = gameObject.AddComponent<PolygonCollider2D>();
		//collider.autoTiling = true;
		//isPickedUp = false;
		//weaponType = "sword";
		//weaponLevel = 0;
		//isRanged = false;
		player = GameObject.Find("player");
		weaponSpawner = GameObject.Find("Weapon Spawner").GetComponent<WeaponSpawner>(); ;
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
        
		if (isRanged1)
        {
			unloadedSprite = GetComponent<SpriteRenderer>().sprite;
		}
	}

	public void setProjectile(bool isProjectile1)
	{
		isProjectile = isProjectile1;
	}

	public void addLoadedSprite(Sprite sprite)
	{
		loadedSprite = sprite;
	}

	public void addProjectile(string projectile1)
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
			GetComponent<Rigidbody2D>().velocity = ((player.GetComponent<playerTurn>().handPos + player.transform.position) - transform.position) * 20;
            //if (Mathf.Abs((player.GetComponent<playerTurn>().handRot % 360) - (GetComponent<Rigidbody2D>().rotation % 360)) > 5)
            {
				GetComponent<Rigidbody2D>().angularVelocity = (player.GetComponent<playerTurn>().handRot - GetComponent<Rigidbody2D>().rotation) * 2;
			}

		}
        if (isRanged)
        {

            if (projFireCount > 49)
            {
				weaponSpawner.spawnWeapon(projectile, weaponLevel, transform.position, transform.rotation, true, new Vector2(transform.forward.x, transform.forward.y) * 10);
				SoundManagerScript.playSound("bowFire");
				GetComponent<SpriteRenderer>().sprite = unloadedSprite;
				projFireCount = 0;
			}

            if (projFireCount > 25)
            {
				GetComponent<SpriteRenderer>().sprite = loadedSprite;
			}

			projFireCount++;

		}
		if (isProjectile && GetComponent<Rigidbody>().velocity.magnitude < 0.1)
		{
			Destroy(gameObject, 5.0f);
		}
	}

	//this sends a damage message for the health for the enemy
	private void OnCollisionEnter2D(Collision2D collision)
    {
		var part = collision.gameObject;
		if (part.GetComponent<WeaponHandler>() != null)
		{
			if (isProjectile)
			{
				Physics2D.IgnoreCollision(part.GetComponent<Collider2D>(), GetComponent<Collider2D>(), true);
			}
		}
		else if (part.GetComponent<enermyTurn>() != null)
		{
			//if(playerHand.GetComponent<spin>().isSpinning)
			{
				//Physics2D.IgnoreCollision(part.GetComponent<Collider2D>(), GetComponent<Collider2D>(), true);
				part.GetComponent<enermyTurn>().hurt(weaponDamage());
                if (!isRanged)
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

				//Debug.Log(heldObject.name);
				//Debug.Log(gameObject.name);

				if (heldObject == null)
				{

					Debug.Log("picked up " + gameObject.name);

					GetComponent<WeaponHandler>().pickup(true);
					transform.parent = player.transform;
					GetComponent<Rigidbody2D>().gravityScale = 0;
					player.GetComponent<playerTurn>().holdingWepn = gameObject;
					Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>(), true);
					SoundManagerScript.playSound("pickUpItem");

				}
				else if (heldObject != gameObject)
                {

					Debug.Log("picked up " + gameObject.name + ", dropped " + heldObject.name);

					heldObject.GetComponent<WeaponHandler>().pickup(false);
					heldObject.transform.parent = transform.parent;
					heldObject.GetComponent<Rigidbody2D>().gravityScale = 1;
					Vector2 launchVelocity = (Random.insideUnitCircle * 5);
					launchVelocity = new Vector2(Mathf.Abs(launchVelocity.x), launchVelocity.y);
					heldObject.GetComponent<Rigidbody2D>().velocity = launchVelocity;
					Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), heldObject.GetComponent<Collider2D>(), false);

					GetComponent<WeaponHandler>().pickup(true);
					transform.parent = player.transform;
					GetComponent<Rigidbody2D>().gravityScale = 0;
					player.GetComponent<playerTurn>().holdingWepn = gameObject;
					Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>(), true);
					SoundManagerScript.playSound("pickUpItem");

				}
                else
                {
					Debug.Log("still holding " + gameObject.name);
				}

			}
		}
	}

}