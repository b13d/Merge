using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
   
    void Start()
    {
        Debug.LogError("Script before Test");

        Debug.LogError("Script after Test");


        for (int i = 0; i < 10000; i++)
        {
            Debug.LogError(i);
        }
    }

}
