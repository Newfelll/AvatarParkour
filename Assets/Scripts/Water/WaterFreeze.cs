using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFreeze : MonoBehaviour
{   private AudioSource waterFreezeSFX;
    public GameObject iceLayer;
    private Material freezeShader;
    public float freezeAmount = 1;
    public float freezeSpeed = 5;
    private bool isFreezin = false;


    void Start()
    {
        freezeShader = iceLayer.GetComponent<Renderer>().material;
        freezeShader.SetFloat("dissolveAmount", 1);
        waterFreezeSFX = GetComponent<AudioSource>();

    }





    void Update()
    {
        if(isFreezin && freezeAmount > 0)
        {   
            freezeAmount -= (Time.deltaTime/freezeSpeed);
            freezeShader.SetFloat("dissolveAmount",freezeAmount );
        }
    }
    public void FreezeWater()
    {   if(!isFreezin) 
        {

            isFreezin = true;
            iceLayer.SetActive(true);
            waterFreezeSFX.PlayOneShot(waterFreezeSFX.clip);

        }
       
    }
}
