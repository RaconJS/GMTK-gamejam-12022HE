using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerFight : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isFighting;
    float startTime;
    void Start()
    {
        isFighting=false;
    }
    public void startFight(){
        isFighting=true;
        startTime=Time.time;
    }
    // Update is called once per frame
    void Update()
    {
        float t=Time.time-startTime;
    }
}
