using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diceRoller : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{

	}
	int rollDice()
	{
		return Random.Range(1, 6);
	}
	// Update is called once per frame
	void Update()
	{

	}
}