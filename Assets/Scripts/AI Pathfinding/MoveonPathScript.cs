using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class MoveonPathScript : MonoBehaviour
{
    #region Variables
    //waypoint variables
    public EditofPathScript PathToFollow;
    public EditofPathScript[] pathToFollowList;
    public GameObject NPC;
    public UnityEngine.AI.NavMeshAgent agent;
    public int CurrentWayPointID = 0;
    public float speed;
    private float reachDistance = 1.0f;
    public float rotationSpeed = 5.0f;
    public string pathName;
    private EditofPathScript path;
    Vector3 last_position;
    Vector3 current_position;

    //Enemy attack and damage variables
    public int damage = 10;
    public float attackRate = 5f;
    public float attackRange = 5f;

    public GateHealth currentGate;
    private float attackTimer = 0f;
    #endregion
    void OnDrawGizmosSelected()
    {
        // Draw the attack sphere around Tower
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    #region Start
    // Use this for initialization
    void Start()
    {

         path = pathToFollowList[Random.Range(0, pathToFollowList.Length)];
        //additional randomised waypoints that I couldn't get working
        //PathToFollow = GameObject.Find(pathName).GetComponent<EditofPathScript>();
        last_position = transform.position;

        //NavMesh agent tutorial setting up the agent with NavMeshAgent
        agent = NPC.GetComponent<UnityEngine.AI.NavMeshAgent>();

    }
    #endregion
    #region Update
    // Update is called once per frame
    void Update()
    {


        // Find waypoint ID
        float distance = Vector3.Distance(path.path_objs[CurrentWayPointID].position, transform.position);
        // Move to waypoint at speed
        transform.position = Vector3.MoveTowards(transform.position, path.path_objs[CurrentWayPointID].position, Time.deltaTime * speed);
        //return path following

        // Rotate agent to face the waypoint
        var rotation = Quaternion.LookRotation(path.path_objs[CurrentWayPointID].position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);

        //tutorial example given of how to attach waypoints to the navmesh agent
        agent.SetDestination(path.path_objs[CurrentWayPointID].transform.position);
        // send agent to next waypoint

        if (distance <= reachDistance)
        {
            CurrentWayPointID++;
        }
       


        // Loop waypoints once the end is reached
       if (CurrentWayPointID >= path.path_objs.Count)
        {
            
            SmashGates();
            CurrentWayPointID = path.path_objs.Count -1;
        }

    }

 
    #endregion

    // Aims at a given enemy every frame
    public virtual void Aim(GateHealth g)
    {
        print("I am aiming at '" + g.name + "'");
    }
    // Attacks at a given enemy only when 'attacking'
    public virtual void Attack(GateHealth g)
    {
        print("I am attacking '" + g.name + "'");

    }

    void DetectGates()
    {
        // Reset current enemy
        currentGate = null;
        // Perform OverlapSphere and get the hits
        Collider[] hits = Physics.OverlapSphere(transform.position, attackRange);
        // Loop through everything we hit
        foreach (var hit in hits)
        {
            // If the thing we hit is an enemy
            GateHealth gate = hit.GetComponent<GateHealth>();
            if (gate)
            {
                // Set current enemy to that one
                currentGate = gate;
            }
        }
    }

    // Protected - Accessible to Cannon / Other Tower classes
    // Virtual - Able to change what this function does in derived classes
    public void SmashGates()
    {
        // Detect enemies before performing attack logic
        DetectGates();
        // Count up the timer
        attackTimer += Time.deltaTime;
        // if there's an enemy
        if (currentGate)
        {
            // Aim at the enemy
            Aim(currentGate);
            // Is attack timer ready?
            if (attackTimer >= attackRate)
                
            {
                // Attack the enemy!
                Attack(currentGate);
                // Reset timer
                attackTimer = 0f;
            }
        }
    }

}
