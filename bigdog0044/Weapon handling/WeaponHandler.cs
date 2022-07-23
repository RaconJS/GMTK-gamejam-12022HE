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
	public List<Sprite> spriteFrames;
	public int rangedShootCooldown = 50;
	public string projectile;
	public int projFireCount = 0;
	private WeaponSpawner weaponSpawner;
	private int pickupCooldown;


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
		pickupCooldown = 0;

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
			spriteFrames = new List<Sprite>();
			spriteFrames.Add(GetComponent<SpriteRenderer>().sprite);
		}
	}

	public void setProjectile(bool isProjectile1)
	{
		isProjectile = isProjectile1;
	}

	public void addSpriteFrame(Sprite sprite)
	{
		spriteFrames.Add(sprite);
	}

	public void addProjectile(string projectile1)
	{
		projectile = projectile1;
	}

	public void pickup(bool picked)
	{
		isPickedUp = picked;
	}

	private void Update()
	{
		if (isPickedUp)
		{

			transform.position = player.GetComponent<playerTurn>().hand.position;
			transform.rotation = player.GetComponent<playerTurn>().hand.rotation;
			transform.localScale = player.transform.localScale;

			GetComponent<Rigidbody2D>().velocity = Vector3.Lerp(transform.position, player.GetComponent<playerTurn>().hand.position, Time.deltaTime);
			Vector3 _vec = Vector3.Slerp(transform.rotation.eulerAngles, player.GetComponent<playerTurn>().hand.rotation.eulerAngles, Time.deltaTime);
			GetComponent<Rigidbody2D>().angularVelocity = Mathf.Rad2Deg * Mathf.Atan2(_vec.y, _vec.x);

		}
	}

    // Update is called once per frame
    void FixedUpdate()
	{
		if (isProjectile)
		{

			transform.right = Vector3.Slerp(transform.right, GetComponent<Rigidbody2D>().velocity.normalized, Time.fixedDeltaTime * 10);

			if (GetComponent<Rigidbody2D>().velocity.magnitude < 0.5 || transform.position.y < -25)
			{
				Destroy(gameObject, 5.0f);
			}

		}

		if (isPickedUp)
		{

			if (isRanged)
			{

				if (projFireCount > rangedShootCooldown)
				{
                    //Debug.Log(new Vector3(transform.forward.x * 100, transform.forward.y * 100, 0).ToString());
                    if (player.transform.localScale.x == 1)
                    {
						weaponSpawner.spawnProjectile(projectile, weaponLevel, transform.position, Quaternion.Euler(new Vector3(
							transform.rotation.eulerAngles.x,
							transform.rotation.eulerAngles.y,
							transform.rotation.eulerAngles.z)), transform.right * 7, gameObject);
					}
                    else
                    {
						weaponSpawner.spawnProjectile(projectile, weaponLevel, transform.position, Quaternion.Euler(new Vector3(
							transform.rotation.eulerAngles.x,
							transform.rotation.eulerAngles.y,
							150 - transform.rotation.eulerAngles.z)), -transform.right * 7, gameObject);
					}
					
					SoundManagerScript.playSound("bowFire");
					projFireCount = 0;
				}

                for (int i = 0; i < spriteFrames.Count; i++)
                {

                    if (projFireCount + 1 > i * (rangedShootCooldown / spriteFrames.Count))
                    {
						GetComponent<SpriteRenderer>().sprite = spriteFrames[i];
					}

                }

				projFireCount++;

			}

		}
	}

	public void dropWeapon()
	{

		GameObject heldObject = player.GetComponent<playerTurn>().holdingWepn;

		Debug.Log("dropped " + heldObject.name);

		Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), heldObject.GetComponent<Collider2D>(), false);
		heldObject.GetComponent<WeaponHandler>().pickup(false);
		heldObject.GetComponent<Rigidbody2D>().gravityScale = 1;
		if (heldObject.GetComponent<WeaponHandler>().isRanged)
		{
			heldObject.GetComponent<SpriteRenderer>().sprite = heldObject.GetComponent<WeaponHandler>().spriteFrames[0];
		}

		if (player.transform.localScale.x == 1)
		{
			heldObject.GetComponent<Rigidbody2D>().velocity = transform.right * 7;
		}
		else
		{
			heldObject.GetComponent<Rigidbody2D>().velocity = -transform.right * 7;
		}

		player.GetComponent<playerTurn>().holdingWepn = null;

	}

	//this sends a damage message for the health for the enemy
	private void OnCollisionEnter2D(Collision2D collision)
    {
		var part = collision.gameObject;
		if (part.GetComponent<WeaponHandler>() != null)
		{
			if (isProjectile || isPickedUp)
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
		else if (part.GetComponent<playerTurn>() != null && pickupCooldown < 10)
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

					//Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>(), true);
					GetComponent<WeaponHandler>().pickup(true);
					//transform.parent = player.transform;
					GetComponent<Rigidbody2D>().gravityScale = 0.2f;
					player.GetComponent<playerTurn>().holdingWepn = gameObject;
					SoundManagerScript.playSound("pickUpItem");

				}
				else if (heldObject != gameObject)
                {

					Debug.Log("picked up " + gameObject.name + ", dropped " + heldObject.name);

					Physics2D.IgnoreCollision(heldObject.GetComponent<Collider2D>(), GetComponent<Collider2D>(), true);
					Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), heldObject.GetComponent<Collider2D>(), false);
					heldObject.GetComponent<WeaponHandler>().pickup(false);
					//heldObject.transform.parent = transform.parent;
					heldObject.GetComponent<Rigidbody2D>().gravityScale = 1;
					//heldObject.GetComponent<Rigidbody2D>().velocity = (player.GetComponent<playerTurn>().hand.position - player.transform.position) * 5;
                    if (heldObject.GetComponent<WeaponHandler>().isRanged)
                    {
						heldObject.GetComponent<SpriteRenderer>().sprite = heldObject.GetComponent<WeaponHandler>().spriteFrames[0];
					}

					if (player.transform.localScale.x == 1)
					{
						heldObject.transform.position += heldObject.transform.right;
						heldObject.GetComponent<Rigidbody2D>().velocity = transform.right * 7;
					}
					else
					{
						heldObject.transform.position -= heldObject.transform.right;
						heldObject.GetComponent<Rigidbody2D>().velocity = -transform.right * 7;
					}

					GetComponent<WeaponHandler>().pickup(true);
					//transform.parent = player.transform;
					GetComponent<Rigidbody2D>().gravityScale = 0.2f;
					player.GetComponent<playerTurn>().holdingWepn = gameObject;
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