using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enermiesTurn : MonoBehaviour
{
	// Start is called before the first frame update
	public bool startTurn;
	public bool startPLayerTurn;
	public int numOfActiveObjs;
	float timeStart;
	public int state=0;
	int tics=0;
	void Start()
	{
		startTurn=true;
		state=2;
	}
	void FixedUpdate(){
		int i=0;
		switch(state){
			//enemies:
				case 0:
					timeStart=Time.time;
					startTurn=false;
					state++;
				break;
				case 1:
					if(Time.time-timeStart>0f){
						state++;
					}
				break;
				case 2:state++;break;
				case 3:
					startTurn=true;
					numOfActiveObjs=1;
					state++;
					tics=0;
				break;
				case 4:
					if(numOfActiveObjs==0){
						tics++;
						if(tics>4)state++;
					}numOfActiveObjs=0;
					timeStart=Time.time;
				break;

			//player:
				case 5:
					timeStart=Time.time;
					startPLayerTurn=false;
					state++;
				break;
				case 6:
					if(Time.time-timeStart>0f){
						state++;
					}
				break;
				case 7:state++;break;
				case 8:
					startPLayerTurn=true;
					numOfActiveObjs=1;
					state++;
				break;
				case 9:
					tics=0;
					if(numOfActiveObjs==0){
						state=0;
						tics++;
						if(tics>4)state++;
					}numOfActiveObjs=0;
					timeStart=Time.time;
				break;
		}
	}
	// Update is called once per frame
	void Update()
	{
		;
	}
}
