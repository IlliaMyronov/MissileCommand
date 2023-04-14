using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    [SerializeField] private RocketSpawner spawnerScript;
    [SerializeField] private List<GameObject> buildingsList;
    [SerializeField] private float respawnTime;
    private float timeSinceLastSpawn;

    private void Awake()
    {
        timeSinceLastSpawn = 0;
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
            spawnerScript.GenerateRocket(buildingsList[building].transform.GetChild(0).transform.position);
            timeSinceLastSpawn = 0;
        }
    }
}
