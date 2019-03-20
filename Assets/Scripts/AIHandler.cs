using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHandler : MonoBehaviour
{
    public int pathIndex;
    public enum Path
    {
        Path1, Path2
    }
    public Path myPath;
    public Transform[] wayPoints;
    // Use this for initialization
    void Start()
    {
        pathIndex = Random.Range(0, 2);
        myPath = (Path)pathIndex;
        if(myPath == Path.Path1)
        {
            wayPoints = GameObject.FindGameObjectWithTag("WPH").GetComponent<PathHandler>().wayPointPath1;
        }
        else
        {
            wayPoints = GameObject.FindGameObjectWithTag("WPH").GetComponent<PathHandler>().wayPointPath2;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
