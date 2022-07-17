using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//enermy attack
public class en_attack : MonoBehaviour
{
	public bool isActive;
	public float actionLength = 0.3f;
	enermyTurn turn;
	// Start is called before the first frame update
	void Start()
	{
		isActive=false;
		turn=GetComponent<enermyTurn>();
	}
	public float start(){
		isActive=true;
		return actionLength;
	}
	public void stop(){
		isActive=false;
	}

	// Update is called once per frame
	void Update()
	{
		
	}
}
