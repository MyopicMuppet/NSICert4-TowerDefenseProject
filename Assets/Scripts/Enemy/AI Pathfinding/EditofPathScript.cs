using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditofPathScript : MonoBehaviour
{
    // Set Variable for rayColor
    public Color rayColor = Color.white;
    // Create a list of transforms
    public List<Transform> path_objs = new List<Transform>();
    // Create a list for array
    Transform[] theArray;

    //draw Gizmos in the editor
    // Note (Manny): Use OnDrawGizmosSelected, it was harder to debug with all the lines everywhere.
    void OnDrawGizmosSelected()
    {
        Gizmos.color = rayColor;
        //Looking at children of main waypoint
        theArray = GetComponentsInChildren<Transform>();
        path_objs.Clear();

        // Create new transform types and use all in array as a transform by any children in the pathholder
        foreach (Transform path_obj in theArray)
        {
            //just use the children object
            if (path_obj != this.transform)
            {
                path_objs.Add(path_obj);
            }
        }

        // Count all the path objects in the list
        for (int i = 0; i < path_objs.Count; i++)
        {

            // Get position of latest game object
            Vector3 position = path_objs[i].position;
            if (i > 0)
            {
                Vector3 previous = path_objs[i - 1].position;
                //draw lines to make the waypoints visible
                Gizmos.DrawLine(previous, position);
                Gizmos.DrawWireSphere(position, 0.3f);
            }
        }
    }
}