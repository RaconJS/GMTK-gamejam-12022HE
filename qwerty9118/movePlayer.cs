using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class movePlayer : MonoBehaviour
{
    public int nrg;
    public bool isMoving;
    private Vector2 direction;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        nrg = 10;
        isMoving = false;
        rb = GetComponent<Rigidbody2D>();
        StartMovement();
    }

    void StartMovement(){
        isMoving = true;
        direction = new Vector2();
        GetComponent<Renderer>().material.SetColor("_Color", Color.red);
    }

    private float xMovement(float xDirection)
    {
        float posOrNeg = xDirection / Mathf.Abs(xDirection);
        switch (Mathf.Abs(xDirection))
        {
            case 0:
                return 1 * posOrNeg;
            case 1:
                return 1.25f * posOrNeg;
            case 2:
                return 1.5f * posOrNeg;
            case 3:
                return 1.75f * posOrNeg;
            case 4:
                return 2 * posOrNeg;
            default:
                return xDirection;
        }

    }

    private float yMovement(float yDirection)
    {
        return yDirection * 2;
    }

    void Update(){
        if(isMoving)
        {
             if(Input.GetKeyDown(KeyCode.LeftArrow)){
                direction.x -= 1;
                Debug.Log(direction.x+", "+direction.y);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                direction.x += 1;
                Debug.Log(direction.x + ", " + direction.y);
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                direction.y += 1;
                Debug.Log(direction.x + ", " + direction.y);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                direction.y -= 1;
                Debug.Log(direction.x + ", " + direction.y);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                isMoving = false;
                rb.velocity = new Vector2(xMovement(direction.x), yMovement(direction.y)) * 2;
                GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                Debug.Log(direction.x + ", " + direction.y);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            StartMovement();
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
