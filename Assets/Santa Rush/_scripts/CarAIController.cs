using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAIController : MonoBehaviour
{
    Rigidbody rb;
    public Vector3 centreofmass;

    public Transform path;
    public float minNodeDistance = 0.1f;
    public float minBrakeDistance = 5f;

    [Header("Driving Properties")]
    public float MaxSteerAngle = 45;
    public float maxTorque = 50f;
    public float maxBrakeTorque = 100f;
    public float maxSpeed = 100f;
    public float minSpeed = 10f;
    public bool isBraking = false;
    float newSteer = 0;

    Transform[] nodes;
    //[HideInInspector]
    public int currentNode = 1;

    [Header("Lights")]
    public Light brakeLightR;
    public Light brakeLightL;

    [Header("Wheel Colliders")]
    public WheelCollider WheelFL;
    public WheelCollider WheelFR;
    public WheelCollider WheelRL;
    public WheelCollider WheelRR;

    bool takeReverse = false;

    float t = 0;

    float y = 0;
    float resetTimer = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centreofmass;

        int count = path.childCount;

        nodes = new Transform[count];

        for(int i = 0; i < count; i++)
        {
            nodes[i] = path.GetChild(i);
        }

        y = transform.position.y;
    }

    private void Update()
    {
        resetTimer += Time.deltaTime;
        if(resetTimer > 20)
        {
            //rb.isKinematic = true;
            //rb.isKinematic = false;

            //Vector3 spawnPos = nodes[currentNode].position;
            //spawnPos.y = y;
            //transform.position = spawnPos;

            //int n = currentNode;
            //if(currentNode < nodes.Length-1)
            //{
            //    n = currentNode+1;
            //}
            //else
            //{
            //    n = 0;
            //}

            //Vector3 dir = nodes[n].position - nodes[currentNode].position;
            //dir.y = y;
            //transform.forward = dir;

            //currentNode = n;

            resetAll();
            resetTimer = 0;
        }
    }

    void resetAll()
    {
        isBraking = false;
        takeReverse = false;
        brakeLightL.enabled = false;
        brakeLightR.enabled = false;

        WheelFL.motorTorque = 0;
        WheelFR.motorTorque = 0;
        WheelRL.motorTorque = 0;
        WheelRR.motorTorque = 0;
        WheelRL.brakeTorque = 0;
        WheelRR.brakeTorque = 0;

        WheelFL.steerAngle = 0;
        WheelFR.steerAngle = 0;

        newSteer = 0;
    }

    private void FixedUpdate()
    {
        if(takeReverse == false && Physics.Raycast(transform.position + transform.forward * 2.5f, transform.forward, 6 ))
        {
            if(takeReverse == false)
            {
                isBraking = false;
                takeReverse = true;
                brakeLightR.enabled = true;
                brakeLightL.enabled = true;

                StartCoroutine(disableReverse());
            }
        }

        if(takeReverse)
        {
            Vector3 dir1 = transform.forward;

            int n = currentNode < path.childCount - 1 ? currentNode + 1 : 0;
            Vector3 dir2 = path.GetChild(currentNode).position - path.GetChild(n).position;

            float angle = Vector3.Angle(dir2, dir1);

            if(Mathf.Abs(180f - angle) < 5)
            {
                takeReverse = false;
                rb.velocity = rb.velocity * .1f;
            }
            else if(Physics.Raycast(transform.position - transform.forward * 2.5f, -transform.forward ,3))
            {
                takeReverse = false;
            }

            Vector3 relativePos = transform.InverseTransformPoint(nodes[currentNode].position);
            float revSteer = -(relativePos.x / relativePos.magnitude) * MaxSteerAngle;


            WheelFL.steerAngle = revSteer;
            WheelFR.steerAngle = revSteer;

            WheelFL.motorTorque = 0;
            WheelFR.motorTorque = 0;
            WheelRL.motorTorque = -maxTorque;
            WheelRR.motorTorque = -maxTorque;

            t += Time.fixedDeltaTime;
            if(t >= 5f)
            {
                takeReverse = false;
            }

        }
        else
        {
            ApplySteer();
            Drive();
            calculateNodeDistance();
            Brake();

            if (isBraking && !brakeLightL.enabled)
            {
                brakeLightL.enabled = true;
                brakeLightR.enabled = true;
            }
            else if (!isBraking && brakeLightL.enabled)
            {
                brakeLightL.enabled = false;
                brakeLightR.enabled = false;
            }

            t = 0;
        }
    }

    void ApplySteer()
    {
        Vector3 relativePos = transform.InverseTransformPoint(nodes[currentNode].position);
        newSteer = (relativePos.x / relativePos.magnitude) * MaxSteerAngle;
        WheelFL.steerAngle = newSteer;
        WheelFR.steerAngle = newSteer;
    }

    void Drive()
    {
        if (!isBraking && rb.velocity.magnitude < maxSpeed)
        {
            WheelRL.motorTorque = maxTorque;
            WheelRR.motorTorque = maxTorque;
        }
        else if (rb.velocity.magnitude < minSpeed)
        {
            WheelRL.motorTorque = maxTorque;
            WheelRR.motorTorque = maxTorque;
        }
        else
        {
            WheelRL.motorTorque = 0;
            WheelRR.motorTorque = 0;
        }
    }

    void calculateNodeDistance()
    {
        if(Vector3.Distance(transform.position, nodes[currentNode].position) < minBrakeDistance)
        {
            isBraking = true;

            if(Vector3.Distance(transform.position, nodes[currentNode].position) < minNodeDistance)
            {
                if(currentNode == path.childCount - 1)
                {
                    currentNode = 0;
                }
                else
                {
                    currentNode++;
                }
                isBraking = false;
            }
        }
    }

    void Brake()
    {

        if (isBraking && rb.velocity.magnitude > minSpeed)
        {
            WheelRL.brakeTorque = maxBrakeTorque;
            WheelRR.brakeTorque = maxBrakeTorque;
        }
        else
        {
            WheelRL.brakeTorque = 0;
            WheelRR.brakeTorque = 0;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "wall" || collision.gameObject.tag == "Car")
        {
            isBraking = false;
            takeReverse = true;
            brakeLightR.enabled = true;
            brakeLightL.enabled = true;

            StartCoroutine(disableReverse());
        }
    }

    IEnumerator disableReverse()
    {
        yield return new WaitForSeconds(5);
        takeReverse = false;
    }
}
