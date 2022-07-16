using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 3f;
    private GameObject player;
    private Vector3 player_position;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player");
        
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        float xMove = Input.GetAxisRaw("Horizontal"); // d key changes value to 1, a key changes value to -1
        float zMove = Input.GetAxisRaw("Vertical"); // w key changes value to 1, s key changes value to -1
        transform.Translate(Vector3.forward * Time.deltaTime * speed * zMove);
        transform.Translate(Vector3.right * Time.deltaTime * speed * xMove);

        player_position = player.transform.position;
        Debug.Log(player_position);
    }
} 