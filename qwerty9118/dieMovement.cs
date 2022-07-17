using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class dieMovement : MonoBehaviour
{
    private Vector3 gravity;
    public bool rolling;
    public bool startRoll;
    public int result;

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
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody>().velocity += new Vector3(0, 0, 9.85f) / 50;
    }
}
