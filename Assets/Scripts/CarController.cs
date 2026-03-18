using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    [SerializeField]
    private WheelCollider wheelFR, wheelFL, wheelBR, wheelBL;
    [SerializeField]
    private Transform transFR, transFL, transBR, transBL;
    [SerializeField]
    private float parMotor;
    [SerializeField]
    private float maxRot;
    [SerializeField]
    public float maxSpeed;
    [SerializeField]
    private float brakeForce;
    private PlayerInput playerInput;
    private float aceleration;
    private float steer;
    private float brake;
    [SerializeField]
    private MeshRenderer meshRenderer;
    public Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {
        aceleration = playerInput.actions["Acelerate"].ReadValue<float>();
        steer = playerInput.actions["Steer"].ReadValue<float>();
        brake = playerInput.actions["Brake"].ReadValue<float>();
        if (brake > 0)
        {
            meshRenderer.material.SetColor("_EmissionColor", Color.red);
        }
        else
        {
            meshRenderer.material.SetColor("_EmissionColor", Color.black);
        }
    }
    private void FixedUpdate()
    {
        //aceleración
        if (maxSpeed > (rb.linearVelocity.magnitude*3.6f))
        {
            wheelBL.motorTorque = parMotor * aceleration;
            wheelBR.motorTorque = parMotor * aceleration;
        }
        else
        {
            wheelBL.motorTorque = 0;
            wheelBR.motorTorque = 0;
        }

        //giro
        wheelFL.steerAngle = maxRot * steer;
        wheelFR.steerAngle = maxRot * steer;
        //frenos
        wheelBL.brakeTorque = brakeForce * brake;
        wheelBR.brakeTorque = brakeForce * brake;
        wheelFL.brakeTorque = brakeForce * brake;
        wheelFR.brakeTorque = brakeForce * brake;


        Vector3 wheelPos;
        Quaternion wheelRot;
        wheelBR.GetWorldPose(out wheelPos, out wheelRot);
        transBR.transform.rotation = wheelRot;
        wheelBL.GetWorldPose(out wheelPos, out wheelRot);
        transBL.transform.rotation = wheelRot;
        wheelFL.GetWorldPose(out wheelPos, out wheelRot);
        transFL.transform.rotation = wheelRot;
        wheelFR.GetWorldPose(out wheelPos, out wheelRot);
        transFR.transform.rotation = wheelRot;
    }
}
