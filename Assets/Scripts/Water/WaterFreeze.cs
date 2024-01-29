using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFreeze : MonoBehaviour
{   private AudioSource waterFreezeSFX;
    public GameObject iceLayer;
    public float freezeAmount = 1;
    public float freezeSpeed = 5;
    private bool isFreezin = false;

    MeshRenderer freezeShaderr;


    MaterialPropertyBlock mpb;
    public MaterialPropertyBlock Mpb
    {
        get
        {
            if (mpb == null)
            {
                mpb = new MaterialPropertyBlock();
            }
            return mpb;
        }
    }

    void Start()
    {
      
        waterFreezeSFX = GetComponent<AudioSource>();
        freezeShaderr = iceLayer.GetComponent<MeshRenderer>();
        Mpb.SetFloat("dissolveAmount", 1);
        freezeShaderr.SetPropertyBlock(Mpb);


    }





    void Update()
    {

        if (isFreezin && freezeAmount > 0)
        {
            freezeAmount -= (Time.deltaTime / freezeSpeed);
            Mpb.SetFloat("dissolveAmount", freezeAmount);
            freezeShaderr.SetPropertyBlock(Mpb);
        }
    }
    public void FreezeWater()
    {   if(!isFreezin) 
        {

            isFreezin = true;
            iceLayer.SetActive(true);
            waterFreezeSFX.PlayOneShot(waterFreezeSFX.clip);
            this.gameObject.tag = "Untagged";

        }
       
    }
}
