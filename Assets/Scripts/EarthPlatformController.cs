using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class EarthPlatformController : MonoBehaviour
{
    public float moveSpeed = 5f;
    [SerializeField] private bool isPulling;
    [SerializeField] private bool isPushing;

    private Vector3 targetPosition;
    private Rigidbody platformRb;

    private Vector3 targetDir;



    private void Start()
    {
        platformRb = GetComponent<Rigidbody>();
       
    }



    private void Update()
    {
       


        if (!isPulling&&!isPushing) 
        {
            platformRb.velocity = Vector3.zero;
            

        }
       
    }

    private void FixedUpdate()
    {
        if (isPulling)
        {
            MoveToPosition();
        }

        if (isPushing)
        {
            MoveToPosition();
        }
    }
    public void PullingPlatform(Vector3 newPosition)
    {   

        isPushing = false;
        targetPosition = new Vector3(newPosition.x, transform.position.y, newPosition.z);
        targetDir = targetPosition-transform.position;
        
        targetDir=targetDir.normalized;
        
        isPulling = true;
       
    }

    void MoveToPosition()
    {
        
       

        if (Vector3.Distance(transform.position, targetPosition) > 0.5)
        {   
           //platformRb.velocity = targetDir * moveSpeed * Time.deltaTime;
           //platformRb.MovePosition(transform.position+(targetDir*moveSpeed*Time.deltaTime));
           transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
        else
        {
            
            isPulling = false;
            isPushing = false;
        }

    }
    public void PushingPlatform(Vector3 direction)
    {
        isPulling = false;
        direction.y = 0;
        targetPosition = transform.position + direction.normalized * 10;
        targetPosition.y = transform.position.y;
        targetDir = targetPosition-transform.position;
        targetDir=targetDir.normalized;
        isPushing = true;
        

    }





    

   
}
