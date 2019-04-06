using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class MoveonPathScript : MonoBehaviour
{
    #region Variables
    //waypoint variables
    [Header("Pathfinding")]
    public EditofPathScript PathToFollow;
    public EditofPathScript[] pathToFollowList;
    public NavMeshAgent agent;
    public int CurrentWayPointID = 0;
    public float speed;
    public float rotationSpeed = 5.0f;
    public string pathName;

    //Enemy attack and damage variables
    [Header("Attack & Damage")]
    public int damage = 10;
    public float attackRate = 5f;
    public float attackRange = 5f;
    public GateHealth currentGate;
    public CastleHealth currentCastle;

    private EditofPathScript path;
    private Vector3 last_position;
    private Vector3 current_position;
    private float reachDistance = 1.0f;
    private float attackTimer = 0f;
    private Transform myTransform;
    #endregion

    // Note (Manny): You don't need a region for every function, i.e, 'Start' doesn't need a '#region Start'
    #region Unity Events
    void OnDrawGizmosSelected()
    {
        // Draw the attack sphere around Tower
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private void Awake()
    {
        myTransform = transform;
    }

    // Use this for initialization
    void Start()
    {
        CurrentWayPointID = 0;

        path = pathToFollowList[Random.Range(0, pathToFollowList.Length)];
        //additional randomised waypoints that I couldn't get working
        //PathToFollow = GameObject.Find(pathName).GetComponent<EditofPathScript>();
        last_position = transform.position;
        //NavMesh agent tutorial setting up the agent with NavMeshAgent
        agent = GetComponent<NavMeshAgent>(); // Note (Manny): "NPC.GetComponent" will only get it from the prefab! Not the Instance!
    }
    // Update is called once per frame
    void Update()
    {
        // Note (Manny): Cache these variables or else your project will get much much slower!
        // Also, it's easier to read!
        List<Transform> currentPath = path.path_objs;
        Transform waypoint = currentPath[CurrentWayPointID];
        // Find waypoint ID
        float distance = Vector3.Distance(waypoint.position, transform.position);
        // Move to waypoint at speed
        transform.position = Vector3.MoveTowards(transform.position, waypoint.position, Time.deltaTime * speed);
        //return path following
        // Rotate agent to face the waypoint
        var rotation = Quaternion.LookRotation(waypoint.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        //tutorial example given of how to attach waypoints to the navmesh agent
        agent.SetDestination(waypoint.position);
        // send agent to next waypoint
        if (distance <= reachDistance)
        {
            CurrentWayPointID++;
        }
        // End movement on waypoint and attack gates
        if (CurrentWayPointID >= currentPath.Count)
        {
            CurrentWayPointID = currentPath.Count - 1;

            SmashGates();
        }

        /*if (currentGate = null)
        {
            DestroyCastle();
        }*/
    }
    #endregion

    #region Internal
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
    #endregion

    #region External
    // Aims at a given enemy every frame
    public virtual void Aim(GateHealth g)
    {
        print("I am aiming at '" + g.name + "'");
    }
    // Attacks at a given enemy only when 'attacking'
    public virtual void Attack(GateHealth g)
    {
        print("I am attacking '" + g.name + "'");
        g.TakeDamage(damage);

        // Note (Manny): The way you're using Inheritance & Polymorphism here is wrong. Come and see me for more details.
    }

    /*public virtual void AimC(CastleHealth c)
    {
        print("I am aiming at '" + c.name + "'");
    }

    public virtual void AttackC(CastleHealth c)
    {
        print("I am attacking  '" + c.name + "'");
        c.TakeDamage(damage);
    }*/
    // Protected - Accessible to Cannon / Other Tower classes
    // Virtual - Able to change what this function does in derived classes
    public void SmashGates()
    {
        // Detect enemies before performing attack logic
        DetectGates();
        // Count up the timer
        attackTimer += Time.deltaTime; // Note (Manny): This script needs to run every frame or else this doesn't make sense.
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

    /*public void DetectCastle()
    {
        // Reset current enemy
        currentCastle = null;
        // Perform OverlapSphere and get the hits
        Collider[] hits = Physics.OverlapSphere(transform.position, attackRange);
        // Loop through everything we hit
        foreach (var hit in hits)
        {
            // If the thing we hit is an enemy
            CastleHealth castle = hit.GetComponent<CastleHealth>();
            if (castle)
            {
                // Set current enemy to that one
                currentCastle = castle;
            }
        }
    }

    public void DestroyCastle()
    {

        DetectCastle();
        attackTimer += Time.deltaTime;
        AimC(currentCastle);
        // Is attack timer ready?
        if (attackTimer >= attackRate)

        {
            // Attack the enemy!
            Attack(currentGate);
            // Reset timer
            attackTimer = 0f;
        }*/
    #endregion
}
