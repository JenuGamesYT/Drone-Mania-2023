using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


namespace DroneMania
{
    [RequireComponent(typeof(IP_Drone_Inputs))]



    public class IP_Drone_Controller : IP_Drone_Rigidbody
    {
        #region Variables

        [Header("Control Properties")]
        [SerializeField] private float minMaxPitch = 30f;
        [SerializeField] private float minMaxRoll = 30f;
        [SerializeField] private float yawPower = 40f;
        [SerializeField] private float lerpSpeed = 20f;
        [SerializeField] private float MoveSpeed = 20f;
        


        private IP_Drone_Inputs input;
        private List<IEngine> engines = new List<IEngine>();

        private float pitch;
        private float MoveX;
        private float MoveY;        
        private float roll;
        private float finalPitch;
        private float finalRoll;
        private float yaw;
        private float finalYaw;

        #endregion
        #region Main Methods

        // Start is called before the first frame update
        void Start()
        {
            input = GetComponent<IP_Drone_Inputs>();
            engines = GetComponentsInChildren<IEngine>().ToList<IEngine>();
            
        }
        #endregion



        #region Custom Methods
        protected override void HandlePhysics()
        {
            HandleEngines();
            HandleControls();
        }
        protected virtual void HandleEngines()
        {
           rb.AddForce(Vector3.up * (rb.mass * Physics.gravity.magnitude ));
           foreach(IEngine engine in engines)
            {
                engine.UpdateEngine(rb , input);
            }
        }
        protected virtual void HandleControls()
        {
            pitch = input.Cyclic.y * minMaxPitch;
            MoveX = input.Cyclic.x * MoveSpeed ;
            MoveY = input.Cyclic.y * MoveSpeed;           
            roll = -input.Cyclic.x * minMaxRoll;
            yaw += input.Pedals * yawPower;
            Vector3 enginRight = Vector3.zero;
            Vector3 enginForward = Vector3.zero;

            enginForward = transform.forward * MoveY;
            enginRight = transform.right * MoveX;            
            finalPitch = Mathf.Lerp(finalPitch, pitch, Time.deltaTime * lerpSpeed);
            finalRoll = Mathf.Lerp(finalRoll, roll, Time.deltaTime * lerpSpeed);
            finalYaw= Mathf.Lerp(finalYaw, yaw, Time.deltaTime * lerpSpeed);


            
            Quaternion rot = Quaternion.Euler(finalPitch, finalYaw, finalRoll);
            rb.AddForce(enginForward, ForceMode.Force );
            rb.AddForce(enginRight, ForceMode.Force);
            rb.MoveRotation(rot);
            

        }
        
        #endregion
    }
}
