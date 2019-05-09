using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TeacherPatrol : MonoBehaviour
{

    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;
    private GameObject player;
    private bool wasChasing;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = false;
        player = GameObject.FindGameObjectWithTag("Player");
        wasChasing = false;
        GotoNextPoint();
    }


    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }


    void Update()
    {
        // Choose the next destination point when the agent gets
        // close to the current one.

        Vision vision = gameObject.GetComponentInChildren(typeof(Vision)) as Vision;

        

            if (vision.isChasing())
            {
            if (vision != null)
            {
                ChaseUpdate();
            }
                
        }
        
    

        else
        {
            PatrolUpdate();
        }

        void ChaseUpdate()
        {
            if (!wasChasing)
            {
                wasChasing = true;
                agent.acceleration = 10f;
                agent.speed = 3.5f;

            }

            agent.destination = player.transform.position;

        }

        void PatrolUpdate()
        {
            if (wasChasing)
            {
                wasChasing = false;
                agent.acceleration = 8f;
                agent.speed = 3.5f;
                agent.destination = points[destPoint].position;
            }
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
                GotoNextPoint();
            Debug.Log("Teacher position:" + transform.position.ToString());
            Debug.Log("Teacher forward Patrol:" + transform.forward.ToString());
        }
       
    }
}
