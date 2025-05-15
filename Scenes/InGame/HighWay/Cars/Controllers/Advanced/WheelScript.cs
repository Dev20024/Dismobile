using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class WheelScript : MonoBehaviour  {
    private Rigidbody rb;
    private AdvancedCarController carController;

    public bool WheelFrontLeft;
    public bool WheelFrontRight;
    public bool WheelBackLeft;
    public bool WheelBackRight;

    [Header("Suspension")]
    [SerializeField] float restLength;
    [SerializeField] float springTravel;
    [SerializeField] float springPower;
    [SerializeField] float dampening;
    

     float minLength;
     float maxLength;
     float lastLength;
     float springLength;
     private float springForce;
     private float damperForce;
     private float springVelocity;

     [Header("Wheel")]
     private float tireGripFactor;
     private float accelPower;
     private float maxSpeed;
     private float BreakingPower;
     

     private Vector3 suspensionForce;
     private Vector3 wheelVelocityLS;
     
     private float Fx;
     private float Fy;
     

    [Header("Wheel")]
    [SerializeField]  float wheelRadius;



    void Start()
    {
        // setting property variables
        rb = transform.root.GetComponent<Rigidbody>();
        carController = transform.root.GetComponent<AdvancedCarController>(); 
        // setting spring variables
        minLength = restLength - springTravel;
        maxLength = restLength + springTravel;
    }

    private void Update() {
        // updating car stats
        tireGripFactor = carController.tireGripFactor;
        accelPower = carController.accelPower;
        maxSpeed = carController.maxSpeed;
        BreakingPower = carController.BreakingPower;
    }

   

   
    void FixedUpdate() {
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, maxLength + wheelRadius)) {
            // suspension
            lastLength =     springLength;
            springLength =   hit.distance - wheelRadius;
            springLength =   Mathf.Clamp(springLength, minLength, maxLength);
            springVelocity = (lastLength - springLength) / Time.fixedDeltaTime;
            springForce =    springPower * (restLength - springLength);
            damperForce =    dampening * springVelocity;

            suspensionForce = (springForce + damperForce) * transform.up;
            // forward velocity
            wheelVelocityLS = transform.InverseTransformDirection(rb.GetPointVelocity(hit.point));
            Fx = carController.accelerationInput * accelPower;
            Fy = -wheelVelocityLS.x * tireGripFactor * accelPower;


           
           
            
            
            // applying forces to wheels
                // suspension & acceleration
                ApplyWheelForces();
                // turning/sliding
                ApplyTurning();
            // max speed
            ApplySpeedClamp();
            // breaking
            ApplyBreaks();
            // friction
            ApplyFriction();
        }

        void ApplyWheelForces() {
            rb.AddForceAtPosition(suspensionForce + (Fx * transform.forward) + (Fy * transform.right), hit.point);
        }

        void ApplyTurning() {
            if (rb.velocity.magnitude > 5) {
                rb.AddForceAtPosition(transform.right * carController.turnInput * carController.TurningPower, hit.point);
                }
        }

        void ApplySpeedClamp() {
            if (rb.velocity.magnitude >  maxSpeed) {
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
            }
        }

        void ApplyBreaks() {
            if (carController.isBreaking) {
                Debug.Log("space down");
                rb.AddForceAtPosition( -rb.velocity * BreakingPower, hit.point);
            }
        }
        
        void ApplyFriction() {
            float friction = rb.mass * 9.8f  * .05f;
            rb.AddForceAtPosition( -rb.velocity.normalized * friction, hit.point);

        }

        
    }
}
