using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class enemyMovement : MonoBehaviour
{
	//private Rigidbody2D rb;
	private float actionLength = 0.3f;
	private float moveDirection = 1;
	enemyMovement em;
	GameObject obj;
	float moveSpeed = 270;
	int edgeCollisions;
	enermyTurn turn;
	private bool isActive;


	//public float speed;
	private float distance = 1.5f;

	private bool movingRight = false;
	//public Transform groundDetection;
	private Vector3 groundDetectionPos;
	private Quaternion groundDetectionRot;





	// Start is called before the first frame update
	void Start()
	{
		turn=GetComponent<enermyTurn>();
		//ASSUME: enemy is not touching  
		//edgeCollisions=0;
		moveDirection = 1f;
		groundDetectionPos = new Vector3(-0.5f, 0, 0);
		isActive = false;
		//groundDetection = gameObject.AddComponent<Transform>();
	}

	public float start(){
		//GetComponent<Rigidbody2D>().constraints &=~ RigidbodyConstraints2D.FreezePositionX;
		isActive = true;
		SoundManagerScript.setWalking(false, true);
		return actionLength;
	}

	public void stop(){
		isActive = false;
		SoundManagerScript.setWalking(false, false);
		var v = GetComponent<Rigidbody2D>().velocity;
		v.x = 0;
		GetComponent<Rigidbody2D>().velocity = v;
		//GetComponent<Rigidbody2D>().constraints |= RigidbodyConstraints2D.FreezePositionX;
		//Debug.Log("end move");
	}

	// Update is called once per frame
	void FixedUpdate()
	{

		RaycastHit2D aheadGroundInfo = Physics2D.Raycast(transform.position + groundDetectionPos, groundDetectionRot * Vector3.down, distance);
		Debug.DrawRay(transform.position + groundDetectionPos, groundDetectionRot * Vector3.down, Color.green, distance);

		RaycastHit2D groundInfo = Physics2D.Raycast(transform.position, Vector3.down, distance);
		Debug.DrawRay(transform.position, Vector3.down, Color.blue, distance);

        if (groundInfo.transform == null)
        {
			Debug.Log("HELPAMFALLING");
        }
		else if (aheadGroundInfo.transform == null)
		{
			Debug.Log("i haz seen hole!1!");

			if (movingRight)
			{
				groundDetectionRot.eulerAngles = new Vector2(0, 180);
				groundDetectionPos *= -1;//new Vector3(-0.5f, 0, 0);
				movingRight = false;
				moveDirection *= -1;
				transform.localScale = new Vector3(1, 1, 1);
			}
			else
			{
				groundDetectionRot.eulerAngles = new Vector2(0, 360);
				groundDetectionPos *= -1;//new Vector3(0.5f, 0, 0);
				movingRight = true;
				moveDirection *= -1;
				transform.localScale = new Vector3(-1, 1, 1);
			}

			GetComponent<Rigidbody2D>().angularVelocity = 0;
			GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

		}

		//var v = GetComponent<Rigidbody2D>().velocity;
		if (isActive)
		{
			//rb.velocity=new Vector2(moveDirection*moveSpeed,v.y);
			GetComponent<Rigidbody2D>().angularVelocity = moveDirection * moveSpeed;
		}
		//else
		{
			//rb.velocity=new Vector2(0,v.y);
			//GetComponent<Rigidbody2D>().angularVelocity = 0;
		}

		//transform.Translate(Vector2.right * speed * Time.deltaTime);

	}
	/*void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag=="edge"){
			if(edgeCollisions==0)moveDirection*=-1;
			edgeCollisions++;
		}
	}
	void OnTriggerExit2D(Collider2D col){
		if(col.gameObject.tag=="edge"){
			edgeCollisions--;
		}
	}*/
}
