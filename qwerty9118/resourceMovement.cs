using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class resourceMovement : MonoBehaviour
{
    private DieLauncher dieGun;
    private DieScaling dieScale;
    private int diceSound;
    private bool killResource = false;

    void Update()
    {
        if (transform.position.z > 5)
        {
            transform.position = new Vector3(transform.parent.position.x, transform.parent.position.y, Random.Range(-3f, -5f));
        }
        if (killResource)
        {
            Destroy(gameObject);
        }
    }

    private void Awake()
    {
        dieScale = GameObject.Find("Dice tray").GetComponent<DieScaling>();
        transform.localScale = new Vector3(
            dieScale.scaleY / dieScale.size.y,
            dieScale.scaleY / dieScale.size.y,
            dieScale.scaleY / dieScale.size.y) * 0.75f;

        dieGun = GameObject.Find("Dice Gun").GetComponent<DieLauncher>();
        float angle = 2f * Mathf.PI * Random.Range(0f, 1f);
        //GetComponent<Rigidbody>().velocity += new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), Random.Range(0f, 1.5f)) * Random.Range(5f, 10f);
        //GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * Random.Range(5f, 10f);
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
        SoundManagerScript.playSound("diceRoll" + diceSound);
    }

}
