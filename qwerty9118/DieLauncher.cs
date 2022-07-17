using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading;

public class DieLauncher : MonoBehaviour
{

    public GameObject die;

    public int rollDice()
    {
        int[] result = rollDie().ToArray();
        Debug.Log(result.Length);
        Debug.Log(result[0]);
        return result[0];
    }

    private IEnumerable<int> rollDie()
    {
        GameObject dieInstance = Instantiate(die, new Vector3(0, 0, -14), Random.rotation);
        while (dieInstance.GetComponent<dieMovement>().rolling)
        {
            Thread.Sleep(3000);
        }
        yield return dieInstance.GetComponent<dieMovement>().result;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            rollDice();
        }
    }

}
