using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicFPSMovement : MonoBehaviour
{
    public bool pressed;
    public float speed, coldown = 0.3333f,coldown_rotate = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += transform.forward * (Input.GetAxisRaw("Vertical") * speed * Time.fixedDeltaTime);
        if (coldown_rotate <= 0 || !pressed)
        {
            transform.Rotate(Vector3.up * (Input.GetAxisRaw("Horizontal") * 45));
            pressed = true;
            coldown_rotate = coldown;

        }
        else
        {
        pressed = Input.GetAxisRaw("Horizontal") != 0;
            if (pressed)
            {
                coldown_rotate -= Time.fixedDeltaTime;
                pressed = (Input.GetAxisRaw("Horizontal") != 0 || coldown_rotate > 0);

            }
        }

    }
}
