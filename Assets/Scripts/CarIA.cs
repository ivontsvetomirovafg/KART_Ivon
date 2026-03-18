using System;
using UnityEngine;

public class CarIA : MonoBehaviour
{
    [SerializeField]
    private PathCars path;
    [SerializeField]
    private WheelCollider wheelFR, wheelFL, wheelBR, wheelBL;
    [SerializeField]
    private Transform transFR, transFL, transBR, transBL;
    [SerializeField]
    private float parMotor;
    [SerializeField]
    private float brakeForce;
    [SerializeField]
    private float steerAngle;
    [SerializeField]
    private int currentNode;
    [SerializeField]
    private float distanceToChangeNode;
    [SerializeField]
    private Transform[] sensors;
    [SerializeField]
    private float sensorsDistance;
    private MeshRenderer mesh;
    private BrakeZone brakeZone;
    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        wheelBL.motorTorque = parMotor;
        wheelBR.motorTorque = parMotor;
        CheckAngle(); //primero mirar en que dirección debe ir
        CheckSensors(); //después compruebe si no hay obstáculos en medio

        Vector3 wheelPos;
        Quaternion wheelRot;
        wheelBL.GetWorldPose(out wheelPos, out wheelRot);
        transBL.rotation = wheelRot;
        wheelBR.GetWorldPose(out wheelPos, out wheelRot);
        transBR.rotation = wheelRot;
        wheelFR.GetWorldPose(out wheelPos, out wheelRot);
        transFR.rotation = wheelRot;
        wheelFL.GetWorldPose(out wheelPos, out wheelRot);
        transFL.rotation = wheelRot;
    }
    private void CheckAngle()
    {
        Vector3 distance = path.nodes[currentNode].position - transform.position;
        Quaternion anguloGiro = Quaternion.FromToRotation(transform.forward * -1, distance.normalized);
        wheelFR.steerAngle = anguloGiro.eulerAngles.y;
        wheelFL.steerAngle = anguloGiro.eulerAngles.y;
        if (distance.magnitude <= distanceToChangeNode)
        {
            currentNode += 1;
            if (currentNode >= path.nodes.Count)
            {
                currentNode = 0;
            }
        }
    }
    private void CheckSensors()
    {
        RaycastHit hit;
        float avoidMultiply = 0;
        bool avoiding = false;
        for (int i = 0; i < sensors.Length; i++)
        {
            if (Physics.Raycast(sensors[i].position, sensors[i].forward, out hit, sensorsDistance))
            {
                switch(i)
                {
                    case 0: //frontal derecho
                        Debug.Log("Choco frontal derecho");
                        avoidMultiply = -1;
                        avoiding = true;
                    break;
                    case 1: //lateral derecho
                        if (avoiding == false)
                        {
                            avoiding = true;
                            Debug.Log("Choco lateral derecho");
                            avoidMultiply = -0.5f;
                        }

                    break;
                    case 2: //frontal izquierdo
                        Debug.Log("Choco frontal izquierdo");
                        avoidMultiply = 1;
                        avoiding = true;
                    break;
                    case 3: //lateral izquierdo
                        if (avoiding == false)
                        {
                            avoiding = true; 
                            Debug.Log("Choco lateral izquierdo");
                            avoidMultiply = 0.5f;
                        }
                        break;
                    case 4: //frontal

                        Quaternion rot = Quaternion.FromToRotation(transform.forward, hit.normal);
                        Vector3 eulerRot = rot.eulerAngles;
                        if(eulerRot.y>180)//girar derecha
                        {
                            avoidMultiply = 1;
                        }
                        else //girar izquierda
                        {
                            avoidMultiply = -1;
                        }
                        avoiding = true;

                        break;
                }   
            }
            Debug.DrawRay(sensors[i].position, sensors[i].forward * sensorsDistance, Color.red, 0.1f);
        }
        if (avoiding == true)
        {
            wheelFL.steerAngle = steerAngle * avoidMultiply;
            wheelFR.steerAngle = steerAngle * avoidMultiply;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
     
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "BrakeZone")
        { 
            brakeZone = other.GetComponent<BrakeZone>();
            if (brakeZone.maxSpeed < rb.linearVelocity.magnitude * 3.6f)
            {
                wheelFL.brakeTorque = brakeForce;
                wheelBL.brakeTorque = brakeForce;
                wheelFR.brakeTorque = brakeForce;
                wheelBR.brakeTorque = brakeForce;

                mesh.materials[2].SetColor("EmissionColor", Color.red);

            }
            else
            {
                wheelFL.brakeTorque = 0;
                wheelBL.brakeTorque = 0;
                wheelFR.brakeTorque = 0;
                wheelBR.brakeTorque = 0;

                mesh.materials[2].SetColor("EmissionColor", Color.black);
            }
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag=="BrakeZone")
        {
            wheelFL.brakeTorque = 0;
            wheelBL.brakeTorque = 0;
            wheelFR.brakeTorque = 0;
            wheelBR.brakeTorque = 0;

            mesh.materials[2].SetColor("EmissionColor", Color.black);
        }
    }
}
