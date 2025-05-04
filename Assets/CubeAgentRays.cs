    using UnityEngine;
    using Unity.MLAgents;
    using Unity.MLAgents.Sensors;
    using Unity.MLAgents.Actuators;
    using System.Collections.Generic;
public class CubeAgentRays : Agent
{
    public float Force = 15f;
    private Rigidbody rb = null;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX;
    }

    private void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.UpArrow) == true)
        {
            Thrust();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Obstacle") == true)
        {
            Destroy(collision.gameObject);
        }
    }

    private void Thrust()
    {
        rb.AddForce(Vector3.up * Force, ForceMode.Acceleration);
    }
}
