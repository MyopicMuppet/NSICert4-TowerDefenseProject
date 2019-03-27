using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{

    public GameObject[] towers;
    //Keep track of towers we spawn
    public GameObject[] holograms;
    [Header("Raycasts")]
    public float rayDistance = 1000f;
    public LayerMask hitLayers;
    public QueryTriggerInteraction triggerInteraction;

    //Current prefab selected
    private int currentIndex;

    void DrawRay(Ray ray)
    {
        Gizmos.DrawLine(ray.origin, ray.origin + ray.direction * 1000f);
    }


    void onDrawGizmos()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Ray playerRay = new Ray(transform.position, transform.forward);

        Gizmos.color = Color.white;
        DrawRay(mouseRay);
        Gizmos.color = Color.red;
        DrawRay(playerRay);
    }

    // Update is called once per frame
    void Update()
    {
        //Disable all Holgrams at the start of the frame
        DisableAllHolograms();

        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        //Perform Raycast
        if (Physics.Raycast(mouseRay, out hit))
        {
            //Try getting a placeable script
            Placeable p = hit.transform.GetComponent<Placeable>();
            // if it is a placeable and its available (no tower spwawned)
            if (p && p.isAvailable)
            {
                //Get position of placeable
                Vector3 placeablePoint = p.transform.position;
                //Get hologram of current tower
                GameObject hologram = holograms[currentIndex];
                hologram.SetActive(true);
                //set position of hologram
                hologram.transform.position = p.GetPivotPoint();

                if (Input.GetMouseButtonDown(0))
                {
                    // Get the prefab
                    GameObject towerPrefab = towers[currentIndex];
                    //spawn the tower
                    GameObject tower = Instantiate(towerPrefab);
                    //position to placeable
                    tower.transform.position = p.GetPivotPoint();
                    // The tile is no longer available
                    p.isAvailable = false;
                }
            }


        }
    }
    /// <summary>
    /// Diables the GameObjects of all referenced Holograms
    /// </summary>
    void DisableAllHolograms()
    {
        foreach (var holo in holograms)
        {
            holo.SetActive(false);
        }
    }
    public void SelectTower(int index)
    {
        //Is index in range of prefabs
        if (index >= 0 && index < towers.Length)
        {
            // Set current index
            currentIndex = index;
        }
    }
}
