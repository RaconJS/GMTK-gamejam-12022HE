using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void ApplyDamageEnemy(float damage)
    {
        Debug.Log("applying:" + damage);
    }
}
