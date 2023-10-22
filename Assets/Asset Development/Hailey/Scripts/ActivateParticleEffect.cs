using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateParticleEffect : MonoBehaviour
{
    public GameObject particleEffect;

    public bool activate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (particleEffect != null)
        {
            if (activate)
            {
                particleEffect.SetActive(true);
            }
            else
            {
                particleEffect.SetActive(false);
            }
        }
    }
}
