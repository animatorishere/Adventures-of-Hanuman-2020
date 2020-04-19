using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    // Use this for initialization  
    void Start()
    {
     
    }

    // Update is called once per frame  
    void Update()
    {
       
        transform.Translate(0.0f, 0.0f, 0.04f);
     
        Destroy(gameObject, 20.0f);
        

    }
    
}