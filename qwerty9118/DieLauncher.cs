using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading;

public class DieLauncher : MonoBehaviour
{

    public GameObject die;
    public List<int> diceOutput = new List<int>();

    public void rollDice()
    {
        GameObject dieInstance = Instantiate(die, new Vector3(0, 0, -14), Random.rotation);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            rollDice();
        }
    }

}
