using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneMovments : MonoBehaviour
{
    public Rigidbody rb;
    
    public float flyspeed ;
    public float RotateSpeed;
    public float drift;
    float horizontal;
    float vertical ;
    float MouseX ;
    float MouseZ;
    Vector3 Fwd ;
    Vector3 direction;
    Vector3 rotation;

    Vector3 Side;




    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        


    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        float MouseX = Input.GetAxis("Mouse X");
        float MouseZ = Input.GetAxis("Mouse Y");
        Vector3 Fwd = new Vector3(MouseX, 0, 0);
        Vector3 direction = new Vector3(0, vertical, 0);
        Vector3 rotation = new Vector3(0, horizontal, 0);

        Vector3 Side = new Vector3(0, 0, MouseZ);



        /*if (direction.magnitude >= 0.1)
        { 
            rb.AddForce(new Vector3( 0 , vertical * flyspeed * Time.deltaTime,0), ForceMode.Impulse );
            
        }
        if (Fwd.magnitude >= 0.1)
        {
            rb.AddForce(new Vector3(MouseX  * Time.deltaTime * flyspeed, 0,0), ForceMode.Impulse);
            rb.transform.Rotate(new Vector3(0 , 0, drift *MouseX * -RotateSpeed * Time.deltaTime));
        }
        if (Side.magnitude >= 0.1)
        {
            rb.AddForce(new Vector3(0,0, MouseZ   * Time.deltaTime* flyspeed), ForceMode.Impulse);
            rb.transform.Rotate(new Vector3(MouseZ * drift * RotateSpeed * Time.deltaTime,0, 0 ));
        }
        if (rotation.magnitude >= 0.1)
        {
            
            rb.transform.Rotate(new Vector3(0, horizontal * RotateSpeed * Time.deltaTime, 0));
        }*/
        fly();
    }

    void fly()
    {

        if (Input.GetKey(KeyCode.W))
        {

            rb.AddForce(new Vector3(0, flyspeed * Time.deltaTime, 0), ForceMode.Impulse);
        }
      else if (Input.GetKey(KeyCode.S))
        {

            rb.AddForce(new Vector3(0, -flyspeed * Time.deltaTime, 0), ForceMode.Impulse);
        }
        else if (Input.GetKey(KeyCode.A))
        {

            rb.transform.Rotate(new Vector3(0, -RotateSpeed * Time.deltaTime, 0));
        }
        else if (Input.GetKey(KeyCode.D))
        {

            rb.transform.Rotate (new Vector3(0, RotateSpeed * Time.deltaTime, 0));
        }
        if ( MouseX >= .1f)
        {

            rb.AddForce( Fwd * flyspeed * Time.deltaTime , ForceMode.Impulse);
        }



    }
}
