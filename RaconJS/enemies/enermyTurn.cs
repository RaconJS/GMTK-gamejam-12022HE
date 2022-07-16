using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enermyTurn : MonoBehaviour
{
	// Start is called before the first frame update
	public GameObject enermyTurnHandler;
	enemyMovement movePart;
	en_attack attackPart;
	float actionLeft;
	int actionsLeft;
	string action;//move,attack
	public string[] actions={"move","attack"};
	void Start()
	{
		movePart=GetComponent<enemyMovement>();
		attackPart=GetComponent<en_attack>();
		attackPart.enabled=false;
		movePart.enabled=false;
	}
	// Update is called once per frame
	void Update()
	{
		if(actionLeft>0){
			actionLeft-=Time.deltaTime;
		}
		if(actionLeft<=0&&actionsLeft>0){
			pickNextAction();
		}
	}
	void pickNextAction(){
		actionsLeft--;
		actionLeft=1f;
		action=actions[Random.Range(0,1)];
		Debug.Log(action);
		movePart.enabled=false;
		attackPart.enabled=false;
		switch(action){
			case"move":{
				movePart.enabled=true;
			}break;
			case"attack":{
				attackPart.enabled=true;
			}break;
		}
	}
	void endMovementAction(){
	}
}
