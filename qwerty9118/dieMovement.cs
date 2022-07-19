using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class dieMovement : MonoBehaviour
{
    public bool rolling;
    public bool startRoll;
    private DieLauncher dieGun;
    private DieScaling dieScale;
    private int diceSound;

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

            //Debug.Log(transform.GetChild(closestIndex).name);

            dieGun.diceOutput.Add(int.Parse(Regex.Match(transform.GetChild(closestIndex).name, @"\d+").Value));

            rolling = false;

            //Destroy(gameObject, 5.0f);

        }
    }

    private void Awake()
    {
        dieScale = GameObject.Find("Dice tray").GetComponent<DieScaling>();
        transform.localScale = new Vector3(
            dieScale.scaleY / dieScale.size.y,
            dieScale.scaleY / dieScale.size.y,
            dieScale.scaleY / dieScale.size.y);

        dieGun = GameObject.Find("Dice Gun").GetComponent<DieLauncher>();
        float angle = 2f * Mathf.PI * Random.Range(0f,1f);
        GetComponent<Rigidbody>().velocity += new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), Random.Range(0f, 1.5f)) * Random.Range(5f, 10f);
        GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * Random.Range(5f, 10f);
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody>().velocity += new Vector3(0, 0, 9.85f) / 50;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (diceSound < 6)
        {
            diceSound++;
        }
        SoundManagerScript.playSound("diceRoll"+diceSound);
    }

}
