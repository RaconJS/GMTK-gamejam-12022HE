using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class dieMovement : MonoBehaviour
{
    private Vector3 gravity;
    public bool rolling;
    public bool startRoll;
    public GameObject die;
    public int result;

    public int rollDice()
    {
        int[] result = rollDie().ToArray();
        Debug.Log(result.Length);
        Debug.Log(result[0]);
        return result[0];
    }

    private IEnumerable<int> rollDie()
    {
        GameObject dieInstance = Instantiate(die, new Vector3(0, 0, -15), Random.rotation);
        while (dieInstance.GetComponent<dieMovement>().rolling)
        {
            Thread.Sleep(3000);
        }
        yield return dieInstance.GetComponent<dieMovement>().result;
    }

    // Start is called before the first frame update
    void Awake()
    {
        rolling = true;
    }

    void Update()
    {
        if(startRoll == false) {
            rolling = true;
            startRoll = true;
        }
        if (GetComponent<Rigidbody>().velocity.magnitude < 0.1 && rolling)
        {
            int closestIndex = 0;
            float minZ = 100;

            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).position.z < minZ)
                {
                    minZ = transform.GetChild(i).position.z;
                    closestIndex = i;
                }
            }

            Debug.Log(transform.GetChild(closestIndex).name);

            rolling = false;

        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            rollDice();
        }
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody>().velocity += new Vector3(0, 0, 9.85f) / 50;
    }
}
