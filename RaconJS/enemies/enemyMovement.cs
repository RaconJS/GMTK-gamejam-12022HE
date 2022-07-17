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
	// Start is called before the first frame update
	void Start()
	{
		turn=GetComponent<enermyTurn>();
		//ASSUME: enemy is not touching  
		edgeCollisions=0;
		moveDirection=1f;
		isActive=false;
	}
	public bool isActive;
	public float start(){
		rb.constraints&=~RigidbodyConstraints2D.FreezePositionX;
		isActive=true;
		return actionLength;
	}
	public void stop(){
		isActive=false;
		var v=rb.velocity;
		rb.velocity=new Vector2(0,v.y);
		rb.constraints|=RigidbodyConstraints2D.FreezePositionX;
		Debug.Log("end move");
	}
	// Update is called once per frame
	void Update()
	{
		var v=rb.velocity;
		if(isActive){
			rb.velocity=new Vector2(moveDirection*moveSpeed,v.y);
		}else{
			rb.velocity=new Vector2(0,v.y);
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
