using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enermiesTurn : MonoBehaviour
{
	// Start is called before the first frame update
	public bool startTurn;
	public int numOfActiveObjs;
	float timeStart;
	public int state=0;
	void Start()
	{
		startTurn=true;
		state=2;
	}
	void FixedUpdate(){
		switch(state){
			case 0:
				timeStart=Time.time;
				startTurn=false;
				state++;
			break;
			case 1:
				if(Time.time-timeStart>2f){
					state++;
				}
			break;
			case 2:state++;break;
			case 3:
				startTurn=true;
				numOfActiveObjs=1;
				state++;
			break;
			case 4:
				if(numOfActiveObjs==0){
					state=0;
				}numOfActiveObjs=0;
			break;
		}
	}
	// Update is called once per frame
	void Update()
	{
		
	}
}
