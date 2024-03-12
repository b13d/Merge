using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinParticle : MonoBehaviour
{
    public void PlayParticle()
    {
        StartCoroutine(ParticleStart());
    }

    IEnumerator ParticleStart()
    {
        GetComponent<ParticleSystem>().Play();
        
        yield return new WaitForSeconds(1.2f);
        
        Destroy(gameObject);
    }
}
