using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformEdge : MonoBehaviour
{
	bool isTest=false;
	// Start is called before the first frame update
	void Start()
	{
		if(!isTest){
			GetComponent<SpriteRenderer>().enabled=false;
		}
	}

	// Update is called once per frame
	void Update()
	{
		
	}
}
