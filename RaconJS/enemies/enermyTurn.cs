using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enermyTurn : MonoBehaviour
{
	// Start is called before the first frame update
	//:
	public GameObject enermyTurnHandlerObj;
		int maxActions=3;//public
	//---
	enermiesTurn enermyTurnHandler;
	public int actionsLeft;
	enemyMovement movePart;
	en_attack attackPart;
	string action;//move,attack
	public float actionLeft;
	public string[] actions={"move","attack"};
	void Start()
	{
		enermyTurnHandler=enermyTurnHandlerObj.GetComponent<enermiesTurn>();
		movePart=GetComponent<enemyMovement>();
		attackPart=GetComponent<en_attack>();
		actionsLeft=0;
		actionLeft=0f;
	}
	// Update is called once per frame
	public void endTurn(){
		actionsLeft=0;
	}
	void Update()
	{
		if(actionLeft>0){
			actionLeft-=Time.deltaTime;
		}
		if(actionLeft<=0){
			endAction();
			if(actionsLeft>0){
				pickNextAction();
			}
		}
	}
	bool oldState;
	void gainActions(){
		actionsLeft=maxActions;
	}
	void FixedUpdate(){//wait for turn-handler to tell enimies to move
		bool newState=enermyTurnHandler.startTurn;
		if(oldState!=newState&&newState){
			gainActions();
		}
		oldState=enermyTurnHandler.startTurn;
		if(actionsLeft>0||actionLeft>0f){
			enermyTurnHandler.numOfActiveObjs++;
		}
	}
	void endAction(){
		switch(action){
			case"move":movePart.stop();break;
			case"attack":attackPart.stop();break;
		}
		action="";
	}
	void pickNextAction(){
		action=actions[Random.Range(0,2)];
		Debug.Log(action);
		switch(action){
			case"move":
				actionsLeft--;
				actionLeft=movePart.start();
				break;
			case"attack":
				actionsLeft--;
				actionLeft=attackPart.start();
				break;
		}
	}
	void endMovementAction(){
	}
}
