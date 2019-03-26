using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPan : MonoBehaviour
{
    public Camera attachedCamera;
    // Percentage offset where movement starts (25%)
    public float movementThreshold = .25f;
    public float movementSpeed = 20f;
    public float zoomSensitivity = 10f;
    public Vector3 size = new Vector3(20f, 1f, 20f);

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, size);
    }

    /// <summary>
    /// Filters incoming position to keep it within bounds
    /// </summary>
    /// <param name="incomingPos">Position that needs filtering</param>
    /// <returns></returns>
    Vector3 GetAdjustedPos(Vector3 incomingPos)
    {
        // Storing in smaller name
        Vector3 pos = transform.position;
        //Getting half size
        Vector3 halfSize = size * 0.5f;

        // X
        if (incomingPos.x > pos.x + halfSize.x) incomingPos.x = pos.x + halfSize.x;
        if (incomingPos.x < pos.x - halfSize.x) incomingPos.x = pos.x - halfSize.x;

        // Y
        if (incomingPos.y > pos.y + halfSize.y) incomingPos.y = pos.y + halfSize.y;
        if (incomingPos.y < pos.y - halfSize.y) incomingPos.y = pos.y - halfSize.y;

        // Z
        if (incomingPos.z > pos.z + halfSize.z) incomingPos.z = pos.z + halfSize.z;
        if (incomingPos.z < pos.z - halfSize.z) incomingPos.z = pos.z - halfSize.z;

        return incomingPos;
    }

    //Defining
    void Movement()
    {
        //Create transform for smaller name
        Transform camTransform = attachedCamera.transform;

        //Get mouse to viewport coordinates
        Vector2 mousePoint = attachedCamera.ScreenToViewportPoint(Input.mousePosition);
        //Calculate offset from centre of screen
        Vector2 offset = mousePoint - new Vector2(.5f, .5f);
        //Get input only if offset reaches certain threshold
        Vector3 input = Vector3.zero; //The direction to move the camera
        if (offset.magnitude > movementThreshold) input = new Vector3(offset.x, 0, offset.y) * movementSpeed;

        //Get scroll from axis and multiply by zoomSensitivity
        float inputScroll = Input.GetAxisRaw("Mouse ScrollWheel");
        Vector3 scroll = camTransform.forward * inputScroll * zoomSensitivity;

        // Apply movement
        Vector3 movement = input + scroll;
        camTransform.position += movement * Time.deltaTime;

        //Filter position with bounds
        camTransform.position = GetAdjustedPos(camTransform.position);


    }


    // Update is called once per frame
    void Update()
    {
        //Calling
        Movement();
    }


}

