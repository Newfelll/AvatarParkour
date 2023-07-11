using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FireballBehaviour : MonoBehaviour
{   private Vector3 targetPosition;
    [SerializeField] private float moveSpeed = 10f;
    private bool isMoving;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            Move();
        }
    }


   
    private void OnTriggerEnter(Collider other)
    {   if (!other.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
        
    }

    public void DirectionCalc(Vector3 direction)
    {   

        targetPosition = transform.position + direction.normalized * 10;
       // targetPosition.y = transform.position.y;
        isMoving = true;

        
    }


    void Move()
    {

        if (Vector3.Distance(transform.position, targetPosition) > 0.5)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
        else
        {
            isMoving = false;
            Destroy(this.gameObject);
        }
    }



   
}
