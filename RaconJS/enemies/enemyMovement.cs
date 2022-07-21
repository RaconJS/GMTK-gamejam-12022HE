using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class enemyMovement : MonoBehaviour
{
	public Rigidbody2D rb;
	public float actionLength = 0.3f;
	public float moveDirection=1;
	enemyMovement em;
	GameObject obj;
	float moveSpeed=2;
	int edgeCollisions;
	enermyTurn turn;
	
	
	public float speed;
	public float distance;

	private bool movingRight = true;
	//public Transform groundDetection;
	private Vector3 groundDetectionPos;
	private Quaternion groundDetectionRot;





	// Start is called before the first frame update
	void Start()
	{
		turn=GetComponent<enermyTurn>();
		//ASSUME: enemy is not touching  
		edgeCollisions=0;
		moveDirection=1f;
		isActive=false;
		//groundDetection = gameObject.AddComponent<Transform>();
	}
	public bool isActive;
	public float start(){
		rb.constraints&=~RigidbodyConstraints2D.FreezePositionX;
		isActive=true;
		SoundManagerScript.setWalking(false, true);
		return actionLength;
	}
	public void stop(){
		isActive=false;
		SoundManagerScript.setWalking(false, false);
		var v=rb.velocity;
		v.x = 0;
		rb.velocity=v;
		rb.constraints|=RigidbodyConstraints2D.FreezePositionX;
		Debug.Log("end move");
	}
	// Update is called once per frame
	void Update()
	{
		var v=rb.velocity;
		if(isActive){
			//rb.velocity=new Vector2(moveDirection*moveSpeed,v.y);
			rb.angularVelocity = moveDirection * moveSpeed;
		}else{
			rb.velocity=new Vector2(0,v.y);
			rb.angularVelocity = 0;
		}

		//transform.Translate(Vector2.right * speed * Time.deltaTime);
		RaycastHit2D groundInfo = Physics2D.Raycast(groundDetectionPos + transform.position, groundDetectionRot * Vector3.down, distance);

		if (groundInfo.collider == false)
		{
			if (movingRight == true)
			{
				groundDetectionRot.eulerAngles = new Vector2(0, 180);
				groundDetectionPos = new Vector3(1, 0, 0);
				movingRight = false;
				moveDirection *= -1;
			}
			else
			{
				groundDetectionRot.eulerAngles = new Vector2(0, 360);
				groundDetectionPos = new Vector3(-1, 0, 0);
				movingRight = true;
				moveDirection *= -1;
			}
		}
	}
	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag=="edge"){
			if(edgeCollisions==0)moveDirection*=-1;
			edgeCollisions++;
		}
	}
	void OnTriggerExit2D(Collider2D col){
		if(col.gameObject.tag=="edge"){
			edgeCollisions--;
		}
	}
}
