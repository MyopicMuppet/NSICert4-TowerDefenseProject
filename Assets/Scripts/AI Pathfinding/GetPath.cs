using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPath : MonoBehaviour
{

    public GameObject[] allPaths;

    // Use this for initialization
    void Start()
    {

        // Create a random number of the waypoints
        int num = 1; //Random.Range(0, allPaths.Length);
        transform.position = allPaths[num].transform.position;
        MoveonPathScript yourPath = GetComponent<MoveonPathScript>();
        yourPath.pathName = allPaths[num].name;
        
    }

    


}
