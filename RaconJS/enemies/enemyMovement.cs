using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class enemyMovement : MonoBehaviour
{
	public Rigidbody2D rb;
	public float moveDirection=1;
	enemyMovement em;
	GameObject obj;
	float moveSpeed=2;
	int edgeCollisions;
	// Start is called before the first frame update
	void Start()
	{
		Debug.Log("morros");
		//ASSUME: enemy is not touching  
		edgeCollisions=0;
		moveDirection=1f;
	}
	// Update is called once per frame
	void Update()
	{
		var v=rb.velocity;
		rb.velocity=new Vector2(moveDirection*moveSpeed,v.y);
	}
	void OnTriggerEnter2D(Collider2D col){
		Debug.Log("asd");
		if(col.gameObject.tag=="edge"){
			if(edgeCollisions==0)moveDirection*=-1;
			edgeCollisions++;
		}
	}
	void OnColliderExit2D(Collision2D col){
		if(col.gameObject.tag=="edge"){
			moveDirection--;
		}
	}
}
