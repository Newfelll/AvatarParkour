using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassBehaviour : MonoBehaviour
{   private AudioSource grassSFX;
    public AudioClip grassBurnSFX;
    public AudioClip updraftSFX;

    public BoxCollider upDraftColl;
    private Material burningShader;
    public ParticleSystem updraftFX;
    public float burnAmount = 1;
    public float burnSpeed =5;
    bool isBurning = false;

    void Start()
    {
        
       burningShader= GetComponent<Renderer>().materials[1];
       burningShader.SetFloat("burnAmount", 1);

        grassSFX = GetComponent<AudioSource>();



    }

    // Update is called once per frame
    void Update()
    {
       if(isBurning&&burnAmount>0)
        {   
            burnAmount -= (Time.deltaTime/burnSpeed);
            burningShader.SetFloat("burnAmount",burnAmount );
        }
       
       
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fire"))
        {
            upDraftColl.enabled = true;
            isBurning = true;
            updraftFX.Play();
            grassSFX.PlayOneShot(grassBurnSFX);
            grassSFX.PlayOneShot(updraftSFX);
        }
        
    }

}