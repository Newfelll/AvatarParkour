using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class WaterSling : MonoBehaviour
{
    // Start is called before the first frame update
   // public Transform cam;
   // public LayerMask layerMask;
    public Rigidbody rb;
    public LineRendererAnimation lr;

    public KeyCode slingKey;
    public float maxSlingDistance = 10f;
    private Vector3 slingDir;

    public float slingForce = 10f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {


    }




    public void SlingSpherecast(RaycastHit hit)
    {
        /* RaycastHit hit;
         if (Physics.SphereCast(cam.position, 2, cam.forward, out hit, maxSlingDistance, layerMask))
         {
             slingDir = hit.collider.gameObject.transform.position-transform.position;

             Sling(slingDir.normalized);
             lr.ThrowWaterHook(hit.collider.gameObject.transform.position);

         }*/
        
        slingDir = hit.collider.gameObject.transform.position - transform.position;

        Sling(slingDir.normalized);
        lr.ThrowWaterHook(hit.collider.gameObject.transform.position);

    }

    void Sling(Vector3 dir)
    {   
        rb.AddForce(dir * slingForce, ForceMode.Impulse);
      
    }
}
