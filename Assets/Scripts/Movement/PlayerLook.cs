using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
<<<<<<< Updated upstream
    [Range(0f, 100f)]
    [SerializeField]public float sensX=80f;
    
    [Range(0f, 100f)]
    [SerializeField]public float sensY=80f;

    [SerializeField]Transform cam;
    [SerializeField]Transform orientation;
=======
    
    [SerializeField] public static float sensX = 80f;

    
    [SerializeField] public static float sensY = 80f;

    [SerializeField] Transform cam;
    [SerializeField] Transform orientation;
>>>>>>> Stashed changes

    [SerializeField] private float mouseX;
    [SerializeField] private float mouseY;

<<<<<<< Updated upstream
    float multiplier=0.01f;
=======
    float multiplier = 0.01f;
>>>>>>> Stashed changes

    float xRotation;
    float yRotation;

<<<<<<< Updated upstream
    
    // Start is called before the first frame update
    void Start()
    {
        
=======

    // Start is called before the first frame update
    void Start()
    {
      
        
            
>>>>>>> Stashed changes

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

<<<<<<< Updated upstream
    
    void Update()
    {   if (!GameManager.gameOver)
        {
            if (!GameManager.gamePaused) 
            { 

            MouseInput();

            cam.transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
            orientation.transform.rotation = Quaternion.Euler(0f, yRotation, 0f);

            }
        }
    
        
        
=======

    void Update()
    {
        if (!GameManager.gameOver)
        {
            if (!GameManager.gamePaused)
            {

                MouseInput();

                cam.transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
                orientation.transform.rotation = Quaternion.Euler(0f, yRotation, 0f);

            }
        }



>>>>>>> Stashed changes
    }


    void MouseInput()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        yRotation += mouseX * sensX * multiplier;
        xRotation -= mouseY * sensY * multiplier;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

    }
<<<<<<< Updated upstream
}
=======
}
>>>>>>> Stashed changes
