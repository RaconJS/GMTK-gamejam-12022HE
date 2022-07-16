using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionChooser : MonoBehaviour
{
	bool choosingAction;
	public movementX script;//:{float timer}
	// Start is called before the first frame update
	void Start()
	{
		choosingAction=true;
		Debug.Log("move, jump, attack");
	}
	void FixedUpdate(){
		if(choosingAction){
			;
		}
		bool movementKeyDown=
			Input.GetKeyDown(KeyCode.W)||
			Input.GetKeyDown(KeyCode.A)||
			Input.GetKeyDown(KeyCode.S)||
			Input.GetKeyDown(KeyCode.D);
		if(
			movementKeyDown
		){
			script.startMovement();
			choosingAction=false;
		}
	}
	// Update is called once per frame
	void Update()
	{
		//yield WaitForSeconds(5);
		//setActive(false);
	}
}