using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBound : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //the code below is just destroying the prefab clone when it reach certain range it y axis;
        if(transform.position.y > 16)
        {
            Destroy(gameObject);
        }
        else if (transform.position.y < -5)
        {
            Destroy(gameObject);
        }
        
    }
}
