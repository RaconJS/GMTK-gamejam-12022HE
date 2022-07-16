using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerTurn : MonoBehaviour
{
	bool oldState;
	bool isActive;
	public enermiesTurn turn;
    public KeyCode keyStartMove = KeyCode.Q;
    public KeyCode keyStartAttack = KeyCode.F;
    int action;//0:move,1:fight
	movePlayer mover;
	playerFight fighter;
	//player
	void Start(){
		mover=GetComponent<movePlayer>();
		fighter=GetComponent<playerFight>();
	}
	void FixedUpdate(){
		bool newState=turn.startPLayerTurn;
		if(newState&&newState!=oldState){
			isActive=true;
			if(action==0)mover.StartMovement();
			else fighter.startFight();
		}
		if(isActive){
			turn.numOfActiveObjs++;
			isActive=action==0?mover.isMoving:fighter.isFighting;
		}
		oldState=newState;
	}
	void Update(){
		if(isActive){
	        if (Input.GetKeyDown(keyStartMove))
	        {
	        	action=0;
	            mover.StartMovement();
	        }
	        else if(Input.GetKeyDown(keyStartAttack)){
	            mover.isMoving=false;
	        }
	    }
	}
}
