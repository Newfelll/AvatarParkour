using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class WaterSling : MonoBehaviour
{
    
    private AudioSource waterSlingSFX;
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
        waterSlingSFX = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {


    }




    public void SlingSpherecast(RaycastHit hit)
    {
        
        
        slingDir = hit.collider.gameObject.transform.position - transform.position;

        Sling(slingDir.normalized);
        lr.ThrowWaterHook(hit.collider.gameObject.transform.position);
        Destroy(hit.collider.gameObject);

    }

    void Sling(Vector3 dir)
    {   
        rb.AddForce(dir * slingForce, ForceMode.Impulse);
        PlayerMovement.canDash = true;
        waterSlingSFX.PlayOneShot(waterSlingSFX.clip);
      
    }
}
