using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
	enemyMovement em;
	public float moveDirection;
	Rigidbody2D rb;
	int edgeCollisions;
	// Start is called before the first frame update
	void Start()
	{
		rb=GetComponent<Rigidbody2D>();
	}
	// Update is called once per frame
	void Update()
	{

	}
	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.tag=="edge"){
			moveDirection*=-1;
		}
		edgeCollisions++;
	}
	void OnColliderExit2D(){

	}
}
