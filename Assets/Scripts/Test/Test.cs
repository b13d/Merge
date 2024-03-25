using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private Test2 _test2;
   
    void Start()
    {
        StartCoroutine(_test2.CoroutineStart());
        
        Destroy(this, 1f);
    }

}
