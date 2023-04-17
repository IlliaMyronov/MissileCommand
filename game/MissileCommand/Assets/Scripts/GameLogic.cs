using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    [SerializeField] private RocketSpawner spawnerScript;
    [SerializeField] private Camera cam;
    [SerializeField] private List<GameObject> buildingsList;
    [SerializeField] private Vector2 respawnTimeRange;
    [SerializeField] private float yRocketLaunch;
    [SerializeField] private Vector2 xRocketLaunchRange;

    private float timeSinceLastSpawn;
    private float respawnTime;

    private void Awake()
    {
        timeSinceLastSpawn = 0;
        respawnTime = Random.Range(respawnTimeRange.x, respawnTimeRange.y);
    }
    private void Update()
    {
        if(timeSinceLastSpawn < respawnTime)
        {
            timeSinceLastSpawn += Time.deltaTime;
        }
        else
        {
            // need to select a target for a rocket before 
            int building = Mathf.FloorToInt(Random.Range(0, buildingsList.Count));
            spawnerScript.GenerateRocket(buildingsList[building].transform.position, new Vector2(Random.Range(xRocketLaunchRange.x, xRocketLaunchRange.y), yRocketLaunch), true);
            respawnTime = Random.Range(respawnTimeRange.x, respawnTimeRange.y);
            timeSinceLastSpawn = 0;
        }

        if(Input.GetMouseButtonDown(0))
        {
            Vector2 clickPos = new Vector2(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y);
        }
    }
}
