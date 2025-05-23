    using UnityEngine;
    using Unity.MLAgents;
    using Unity.MLAgents.Sensors;
    using Unity.MLAgents.Actuators;
    using System.Collections.Generic;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
public class CubeAgentRays : Agent
{
    public float Force = 15f;
    private Rigidbody rb = null;
    public Transform reset = null;


    public override void Initialize()
    {

        rb = this.GetComponent<Rigidbody>();
        // Freeze the X position and rotation
        rb.constraints = RigidbodyConstraints.FreezePositionX
                | RigidbodyConstraints.FreezeRotationX
                | RigidbodyConstraints.FreezeRotationY
                | RigidbodyConstraints.FreezeRotationZ;

        ResetPlayer();
    }
    /*
    void Update()
    {
        RequestDecision(); // This calls OnActionReceived
    }
    */

    private void FixedUpdate()
    {
        RequestDecision();
    }


    public override void OnActionReceived(ActionBuffers actions)
    {
        Debug.Log("RequestDecision called");
        int action = actions.DiscreteActions[0];
        if (action == 1)
        {
            Thrust();
        }
    }
   
    public override void Heuristic(in ActionBuffers actionsOut)
    {

        Debug.Log("Heuristic called");
        var discreteActions = actionsOut.DiscreteActions;
        discreteActions[0] = 0;

        if (Input.GetKey(KeyCode.UpArrow))
            discreteActions[0] = 1;
    }

    public override void OnEpisodeBegin()
    {
        ResetPlayer();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") == true)
        {
            AddReward(-0.5f);
            Debug.Log("obstacle hit");
            Destroy(collision.gameObject);
            EndEpisode();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WallReward") == true)
        {
            AddReward(0.1f);
        }

        if (other.gameObject.CompareTag("Ceiling") == true)
        {
            Debug.Log("ceiling hit");
            AddReward(-1.0f);
            EndEpisode();
        }
    }

    private void Thrust()
    {
        Debug.Log("Thrust called");
        rb.AddForce(Vector3.up * Force, ForceMode.Acceleration);
    }

    private void ResetPlayer()
    {
        this.transform.position = new Vector3(reset.position.x, reset.position.y, reset.position.z);
    }

}
