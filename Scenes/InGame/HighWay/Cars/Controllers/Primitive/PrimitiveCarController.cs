using Unity.Mathematics;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Rendering;
using UnityEngine;

public class PrimitiveCarController : MonoBehaviour{
    
    [Header("Attributes")]
    public int maxAcceleration; 
    public float peakAccelTime;
    public int targetVelocity;
    public Vector3 targetDirection;
    [Header("Trackers")]
    public float currentVelocity;
    public float currentAcceleration;
    [Header("Components")]
    Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        peakAccelTime = 1/peakAccelTime;
        targetDirection = transform.forward;
    }

    

    void FixedUpdate()
    {
        currentVelocity = rb.velocity.magnitude;
        float velocityGap = targetVelocity - currentVelocity;
        // use sigmoid shape function to determine acceleration 1/ 1+e^-x
        currentAcceleration = -4 + (8/ (1+ math.pow(math.E,peakAccelTime*-velocityGap)));
        Vector3 forceToApply = targetDirection * (currentAcceleration);


        rb.AddForce(forceToApply / Time.deltaTime,ForceMode.Impulse);
    }
}