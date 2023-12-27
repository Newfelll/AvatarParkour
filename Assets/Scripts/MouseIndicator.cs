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
    public Image IndicatorU�;
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
                IndicatorU�.sprite = indicatorSprites[1];
            }

            if (hit.collider.tag == "Earth")
            {
                IndicatorU�.sprite = indicatorSprites[2];
            }

            if (hit.collider.tag == "Fire")
            {
                IndicatorU�.sprite = indicatorSprites[1];
            }

            if (hit.collider.tag == "Ice")
            {
                IndicatorU�.sprite = indicatorSprites[3];
            }




        }

        else
        {
            IndicatorU�.sprite = indicatorSprites[0];
        }


        
    }
}
