using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class movePlayer : MonoBehaviour
{
    public bool isMoving;
    public bool isActive;
    public float landingTime=2f;
    private KeyCode keyReload = KeyCode.R;
    private KeyCode keyLaunch = KeyCode.E;
    private KeyCode keyUp = KeyCode.W;
    private KeyCode keyDown = KeyCode.S;
    private KeyCode keyLeft = KeyCode.A;
    private KeyCode keyRight = KeyCode.D;
    private Vector2 direction;
    private Rigidbody2D rb;
    private DieLauncher diceGun;
    private int waitForDice;

    //RaconJS var's
    public float maxEnergy = 0;// = 4f;
    public int isLanding;
    float timeStartJump;

    // Start is called before the first frame update
    void Start()
    {
    	isLanding=0;
        isMoving = false;
        rb = GetComponent<Rigidbody2D>();
        diceGun = GameObject.Find("Dice Gun").GetComponent<DieLauncher>();
        waitForDice = 0;
    }

    public void StartMovement(){
        maxEnergy = 0;
        diceGun.rollDice();
        diceGun.rollDice();
        waitForDice += 2;
        isMoving = true;
        isLanding = 0;
        isActive=true;
        direction = new Vector2(0, 0);
        GetComponent<Renderer>().material.SetColor("_Color", Color.red);
    }
    public void StopMovement(){
        isMoving = false;
        isLanding = 0;
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
            return ((2 * Mathf.Abs(yDirection) + 4) / 3) * (yDirection / Mathf.Abs(yDirection));
        }
        return 0;
    }

    void Update(){
        if (waitForDice > 0 && diceGun.diceOutput.Count > 0)
        {
            waitForDice--;
            maxEnergy += diceGun.diceOutput[0];
            diceGun.diceOutput.RemoveAt(0);
        }
        if(isMoving)
        {
             if(Input.GetKeyDown(keyLeft))
            {

                direction.x -= 1;
                if (direction.x + direction.y > maxEnergy)
                {
                    direction.x += 1;
                }
                Debug.Log(direction.x+", "+direction.y);

            }
            if (Input.GetKeyDown(keyRight))
            {

                direction.x += 1;
                if (direction.x + direction.y > maxEnergy)
                {
                    direction.x -= 1;
                }
                Debug.Log(direction.x + ", " + direction.y);

            }
            if (Input.GetKeyDown(keyUp))
            {

                direction.y += 1;
                if (direction.x + direction.y > maxEnergy)
                {
                    direction.y -= 1;
                }
                Debug.Log(direction.x + ", " + direction.y);

            }
            if (Input.GetKeyDown(keyDown) && direction.y > 0)
            {

                direction.y -= 1;
                if (direction.x + direction.y > maxEnergy)
                {
                    direction.y += 1;
                }
                Debug.Log(direction.x + ", " + direction.y);

            }
            if (Input.GetKeyDown(keyLaunch))
            {

                isMoving = false;
                isLanding=1;
                timeStartJump=Time.time;
                rb.velocity = new Vector3(xMovement(direction.x), yMovement(direction.y), 0);
                GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                Debug.Log(rb.velocity.x + ", " + rb.velocity.y);

            }
        }
        //if(Input.GetKeyDown(keyReload))
        //{
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //}
        isActive = isMoving || isLanding != 0;
        if(isLanding == 2 && Time.time-timeStartJump > landingTime){
        	isLanding = 0;
        }
    }
    void OnCollisionEnter2D(){
        if(isLanding==1){
            isLanding=2;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!SoundManagerScript.playerWalking && rb.velocity.magnitude > 0.1)
        {
            SoundManagerScript.playerWalking = true;
        }

        if (SoundManagerScript.playerWalking && rb.velocity.magnitude < 0.1)
        {
            SoundManagerScript.playerWalking = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        SoundManagerScript.playerWalking = false;
    }

}
