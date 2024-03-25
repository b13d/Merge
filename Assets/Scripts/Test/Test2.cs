using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test2 : MonoBehaviour
{
    public IEnumerator CoroutineStart()
    {
        Debug.Log("Начало корутины");

        yield return new WaitForSeconds(2f);
        
        
        Debug.Log("Конец корутины");
    }
}
