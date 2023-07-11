using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ElementRaycastController : MonoBehaviour
{
    public Transform cam;
    public float maxInteractionDistance = 10f;
    public LayerMask layerMask;
    public float sphereRadius=1f;
    public GameObject fireball;
    public Transform fireballSpawnPoint;

    [Header("References")]
    private WaterSling waterSling;
    
    



    void Start()
    {
        waterSling = GetComponent<WaterSling>();
       

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0)) // Right Mouse Button (RMB1)
        {
            RaycastHit hit;
          /*  GameObject fireballInstance = Instantiate(fireball, fireballSpawnPoint.position, Quaternion.identity);
                    FireballBehaviour fireballBehaviour = fireballInstance.GetComponent<FireballBehaviour>();
                    if (fireballBehaviour != null)
                    {
                        fireballBehaviour.DirectionCalc(cam.forward);
                    }*/

                if (Physics.SphereCast(cam.position, sphereRadius, cam.forward, out hit, maxInteractionDistance, layerMask))
               // if (Physics.Raycast(cam.position, cam.forward, out hit, maxInteractionDistance, layerMask))
                {
                string hitTag = hit.collider.tag;
                

                if (hitTag == "Water")
                {
                    
                    waterSling.SlingSpherecast(hit);
                }
                else if (hitTag == "Earth")
                {
                    // Get the EarthPlatformController component
                    EarthPlatformController earthPlatform = hit.collider.GetComponent<EarthPlatformController>();
                    
                   
                    if (earthPlatform != null)
                    {
                        // Call the MoveToPosition method
                        earthPlatform.PullingPlatform(transform.position);
                    }
                }
                else if (hitTag == "Fire")
                {
                    /*GameObject fireballInstance = Instantiate(fireball, fireballSpawnPoint.position, Quaternion.identity);
                    FireballBehaviour fireballBehaviour = fireballInstance.GetComponent<FireballBehaviour>();
                    if (fireballBehaviour != null)
                    {
                        fireballBehaviour.DirectionCalc(cam.forward);
                    }*/
                    GameObject fireballInstance = Instantiate(fireball, fireballSpawnPoint.position, Quaternion.identity);
                    FireballBehaviour fireballBehaviour = fireballInstance.GetComponent<FireballBehaviour>();
                    if (fireballBehaviour != null)
                    {
                        fireballBehaviour.DirectionCalc(cam.forward);
                    }


                }
            }

        }
        if (Input.GetMouseButtonDown(1)) // Right Mouse Button (RMB1)
        {
            RaycastHit hit;


            if (Physics.Raycast(cam.position, cam.forward, out hit, maxInteractionDistance, layerMask))
            {   
                
                string hitTag = hit.collider.tag;

                if (hitTag == "Water")
                {

                }
                else if (hitTag == "Earth")
                {
                    // Get the EarthPlatformController component
                    EarthPlatformController earthPlatform = hit.collider.GetComponent<EarthPlatformController>();
                    if (earthPlatform != null)
                    {
                        // Call the MoveInDirection method
                        earthPlatform.PushingPlatform(cam.forward);
                    }

                }
                else if (hitTag == "Fire")
                {

                }
            }







        }
    } }
