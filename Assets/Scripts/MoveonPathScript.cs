using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveonPathScript : MonoBehaviour
{

    public EditofPathScript PathToFollow;
    public EditofPathScript[] pathToFollowList;

    // Establishment of variables
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

    // Use this for initialization
    void Start()
    {

         path = pathToFollowList[Random.Range(0, pathToFollowList.Length)];
        //additional randomised waypoints that I couldn't get working
        PathToFollow = GameObject.Find(pathName).GetComponent<EditofPathScript>();
        last_position = transform.position;

        //NavMesh agent tutorial setting up the agent with NavMeshAgent
        agent = NPC.GetComponent<UnityEngine.AI.NavMeshAgent>();

    }

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
            CurrentWayPointID = 0;
        }

    }


}
