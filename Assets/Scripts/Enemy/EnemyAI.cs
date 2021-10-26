using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public Transform target; //target to follow
    public float followSpeed = 300f; // speed to follow target
    public float nextWaypointDistance = 3f; //distance before move towards target

    private bool withinRange = false;

    Path path; //current path following
    int currentWaypoint = 0; //stores current waypoint along path 
    bool reachedEndOfPath = false; //bool for if have reached end of path

    Seeker seeker; //referance script responsible for making paths
    Rigidbody2D rb; //rigidbody 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        seeker = GetComponent<Seeker>();

        target = GameObject.Find("Player").transform;

        InvokeRepeating("UpdatePath", 0f, 0.5f); //repeat path "update path", no delay, repeating every 0.5 seconds

    }

    void OnTriggerEnter2D (Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            withinRange = true;
        }
    }

        void OnTriggerExit2D (Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            withinRange = false;
        }
    }

    void UpdatePath() //to continiously update path
    {
        if(seeker.IsDone())
        seeker.StartPath(rb.position, target.position, OnPathComplete); //seeker function creates path (starting position, position to move to, method called once path completed)  
    }

    void OnPathComplete(Path p) //method called once path is completed. Takes the newly generated generated path as parameter (called p for each instance)
    {
        if (!p.error) //if path doesn't generate any errors
        {
            path = p; //sets newly generated path to current path
            currentWaypoint = 0; //sets waypoint to 0 as path has completed
        }
    }
    void FixedUpdate()
    {
        if (path == null) //if we don't have a path
            return;
        

        if (currentWaypoint >= path.vectorPath.Count) //if we have reached the end of the waypoints in the path
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized; //gets direction towards next waypoint along path and stores as Vector2 
        Vector2 force = direction * followSpeed * Time.fixedDeltaTime; //creates Vector2 for applying movement to enemy


        if (withinRange)
        {
            rb.AddForce(force); //apply movement to enemy
        }

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]); //calculating distance towards next waypoint 

        if (distance < nextWaypointDistance) //if we have reached the current waypoint
        {
            currentWaypoint++; //increase currentWaypoint (move to next waypoint) 
        }

        
    }
}
