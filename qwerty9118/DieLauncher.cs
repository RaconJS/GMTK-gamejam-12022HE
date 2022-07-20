using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading;

public class DieLauncher : MonoBehaviour
{

    public GameObject die;
    public List<int> diceOutput = new List<int>();
    public KeyCode throwDie = KeyCode.G;

    public void rollDice()
    {
        GameObject dieInstance = Instantiate(die, new Vector3(transform.position.x, transform.position.y, Random.Range(-3f, -5f)), Random.rotation);
        dieInstance.transform.parent = transform;
    }

    public void rollDice(float seconds)
    {
        StartCoroutine(rollDiceDelayed(seconds));
    }

    private IEnumerator rollDiceDelayed(float seconds)
    {
        new WaitForSeconds(3);
        GameObject dieInstance = Instantiate(die, new Vector3(transform.position.x, transform.position.y, Random.Range(-3f, -5f)), Random.rotation);
        dieInstance.transform.parent = transform;
        yield return null;
    }

    private void Update()
    {
        //if (Input.GetKeyDown(throwDie))
        //{
        //    rollDice();
        //}
    }

}
