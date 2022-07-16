using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class movePlayer : MonoBehaviour
{
    public int nrg;
    public bool isMoving;
    public KeyCode keyReload = KeyCode.R;
    public KeyCode keyLaunch = KeyCode.E;
    public KeyCode keyStartMove = KeyCode.Q;
    public KeyCode keyUp = KeyCode.UpArrow;
    public KeyCode keyDown = KeyCode.DownArrow;
    public KeyCode keyLeft = KeyCode.LeftArrow;
    public KeyCode keyRight = KeyCode.RightArrow;
    private Vector2 direction;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        nrg = 10;
        isMoving = false;
        rb = GetComponent<Rigidbody2D>();
    }

    void StartMovement(){
        isMoving = true;
        direction = new Vector2(0, 0);
        GetComponent<Renderer>().material.SetColor("_Color", Color.red);
    }

    private float xMovement(float xDirection)
    {
        if (xDirection != 0)
        {
            return ((2 * Mathf.Abs(xDirection) + 4) / 3) * (xDirection / Mathf.Abs(xDirection));
        }
        return 0;
    }

    private float yMovement(float yDirection)
    {
        if (yDirection != 0)
        {
            return (2 * yDirection + 4) / 3;
        }
        return 0;
    }

    void Update(){
        if(isMoving)
        {
             if(Input.GetKeyDown(keyLeft)){
                direction.x -= 1;
                Debug.Log(direction.x+", "+direction.y);
            }
            if (Input.GetKeyDown(keyRight))
            {
                direction.x += 1;
                Debug.Log(direction.x + ", " + direction.y);
            }
            if (Input.GetKeyDown(keyUp))
            {
                direction.y += 1;
                Debug.Log(direction.x + ", " + direction.y);
            }
            if (Input.GetKeyDown(keyDown))
            {
                direction.y -= 1;
                Debug.Log(direction.x + ", " + direction.y);
            }
            if (Input.GetKeyDown(keyLaunch))
            {
                isMoving = false;
                rb.velocity = new Vector2(xMovement(direction.x), yMovement(direction.y));
                GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                Debug.Log(rb.velocity.x + ", " + rb.velocity.y);
            }
        }
        else if (Input.GetKeyDown(keyStartMove))
        {
            StartMovement();
        }

        if(Input.GetKeyDown(keyReload))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
