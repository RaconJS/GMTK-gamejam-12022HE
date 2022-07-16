using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class movePlayer : MonoBehaviour
{
    public bool isMoving;
    public bool isActive;
    public KeyCode keyReload = KeyCode.R;
    public KeyCode keyLaunch = KeyCode.E;
    public KeyCode keyUp = KeyCode.UpArrow;
    public KeyCode keyDown = KeyCode.DownArrow;
    public KeyCode keyLeft = KeyCode.LeftArrow;
    public KeyCode keyRight = KeyCode.RightArrow;
    private Vector2 direction;
    private Rigidbody2D rb;

    //RaconJS var's
    public float maxEnergy = 4f;
    bool isLanding;
    float timeStartJump;

    // Start is called before the first frame update
    void Start()
    {
        isMoving = false;
        rb = GetComponent<Rigidbody2D>();
    }

    public void StartMovement(){
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
            Vector2 oldDirection=direction;
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
                isLanding=true;
                timeStartJump=Time.time;
                rb.velocity = new Vector2(xMovement(direction.x), yMovement(direction.y));
                GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                Debug.Log(rb.velocity.x + ", " + rb.velocity.y);
            }
            if(direction.magnitude>maxEnergy){//raconJS
                direction=oldDirection;
            }
        }
        if(isLanding){
            if(Time.time-timeStartJump>1){

            }
        }
        if(Input.GetKeyDown(keyReload))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        isActive=isMoving||isLanding;
    }
}
