using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieScaling : MonoBehaviour
{

    public float scaleX;
    public float scaleY;
    public Vector3 size;
    public Vector3 screenSize;

    // Start is called before the first frame update
    void Start()
    {
        size = transform.GetChild(0).GetComponent<Collider>().bounds.size;
        scaleY = Camera.main.orthographicSize * 2;
        screenSize = Camera.main.ScreenToWorldPoint(new Vector3(1, 1, 0)) - Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        scaleX = scaleY * (screenSize.x / screenSize.y);
        transform.localScale = new Vector3(scaleX / size.x, scaleY / size.y, 1);
    }

    private void Update()
    {
        ;
    }

}
