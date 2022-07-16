using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class dieMovement : MonoBehaviour
{
    private Vector3 gravity;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        gravity = new Vector3(0, 0, 9.85f);
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (rb.velocity.magnitude < 0.1)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(transform.position);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.triangleIndex);
            }
        }
    }

    private void FixedUpdate()
    {
        rb.velocity += gravity/50;
    }
}
