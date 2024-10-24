using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testMotion : MonoBehaviour
{
    Animator animator;
    float test;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            test = 0;
        }
        if(Input.GetKey(KeyCode.Space))
        {
            test = 1;
        }
        if(Input.GetKey(KeyCode.LeftAlt)) 
        { 
            test = 2; 
        }
        if(Input.GetKey(KeyCode.A))
        {
            test = 3;
        }

        animator.SetFloat("motionTest", test);
    }
}
