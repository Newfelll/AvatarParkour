using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
   [SerializeField]private float sensX=80f;
   [SerializeField] private float sensY=80f;

   [SerializeField]Transform cam;
   [SerializeField]Transform orientation;

    float mouseX;
    float mouseY;

    float multiplier=0.01f;

    float xRotation;
    float yRotation;

    
    // Start is called before the first frame update
    void Start()
    {
        

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        MouseInput();
        
        cam.transform.localRotation= Quaternion.Euler(xRotation, yRotation, 0f);
        orientation.transform.rotation=Quaternion.Euler(0f, yRotation, 0f);
    }


    void MouseInput()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        yRotation += mouseX * sensX * multiplier;
        xRotation -= mouseY * sensY * multiplier;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

    }
}
