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
    public DieLauncher diceRoller;
    int action;//0:move,1:fight
	movePlayer mover;
	playerFight fighter;
	public float actionsLeft;
	bool waitingForRoll;
	//player
	void Start(){
		waitingForRoll=false;
		mover=GetComponent<movePlayer>();
		fighter=GetComponent<playerFight>();
	}
	void FixedUpdate(){
		bool newState=turn.startPLayerTurn;
		if(newState&&newState!=oldState){
			isActive=true;
			generateActions();
		}
		if(isActive){
			turn.numOfActiveObjs++;
			if(!(action==0?mover.isActive:fighter.isFighting)){
				if(actionsLeft>0)nextAction();
				else isActive=false;
			}
		}
		oldState=newState;
	}
	void generateActions(){
		waitingForRoll=true;
		//diceRoller.startRoll();
		{
			waitingForRoll=false;
			actionsLeft=Random.Range(1,6+1);
		}
	}
	void nextAction(){
		action=0;
		if(action==0)mover.StartMovement();
		else fighter.startFight();
		actionsLeft--;
		isActive=true;
	}
	void Update(){
		if(waitingForRoll){
			//if(diceRoller.isDone){
			//	waitingForRoll=false;
			//	actionsLeft=diceRoller.value;
			//}
		}
		else{
			if(isActive){
		        if (Input.GetKeyDown(keyStartMove))
		        {
		        	action=0;
		            mover.StartMovement();
		        }
		        else if(Input.GetKeyDown(keyStartAttack)){
		            mover.StopMovement();
		            mover.isMoving=false;
		        }
		    }
		}
	}
}
