using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoClipPlayerMovement : MonoBehaviour
{
    public float speed = 3;
    public static Vector2 Mouse_Sensitivity = new Vector3 (90,180);
    
    private void OnEnable() 
    {
        Cursor.lockState = CursorLockMode.Locked;
        
    }
    private void OnDisable() 
    {
        Cursor.lockState = CursorLockMode.None;
    }
  
    void FixedUpdate()
    {
        Vector3 InputVector = (
            (transform.forward * Input.GetAxis("Vertical") * speed) + (transform.right * Input.GetAxis("Horizontal") * speed )
        );
        transform.position += InputVector;
       
        transform.Rotate(new Vector3 (
                (-Input.GetAxisRaw("Mouse Y") * Mouse_Sensitivity.y * Time.fixedDeltaTime),
                Input.GetAxisRaw("Mouse X")* Mouse_Sensitivity.x*Time.fixedDeltaTime,
                0
                ));

       transform.eulerAngles = new Vector3 (
           transform.eulerAngles.x,
           transform.eulerAngles.y,
           0
        );
    }
    
}
