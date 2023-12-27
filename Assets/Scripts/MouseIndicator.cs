using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseIndicator : MonoBehaviour
{
    public Sprite[] indicatorSprites;
    public Transform cam;
   
    public LayerMask layerMask;
    public float sphereRadius = 1f;
    public Image IndicatorUÝ;
    void Start()
    {
        ;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(cam.position, cam.forward, out hit, ElementRaycastController.maxInteractionDistance, layerMask))
           
        {

            if (hit.collider.tag == "Water")
            {
                IndicatorUÝ.sprite = indicatorSprites[1];
            }

            if (hit.collider.tag == "Earth")
            {
                IndicatorUÝ.sprite = indicatorSprites[2];
            }

            if (hit.collider.tag == "Fire")
            {
                IndicatorUÝ.sprite = indicatorSprites[1];
            }

            if (hit.collider.tag == "Ice")
            {
                IndicatorUÝ.sprite = indicatorSprites[3];
            }




        }

        else
        {
            IndicatorUÝ.sprite = indicatorSprites[0];
        }


        
    }
}
