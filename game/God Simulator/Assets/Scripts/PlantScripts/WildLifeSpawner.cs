using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Build.Content;
using UnityEngine;

public class WildLifeSpawner : MonoBehaviour
{
    // sets how often trees will be attempted to spawn
    [SerializeField] private int plantSpawnInterval;

    // sets what part of the world is maximum covered with wild plants, may vary from 0 (0%) to 1 (100%)
    [SerializeField] private float maxPlantDencity;

    // how rare spawn of plants is. 1 sets it to always, 2 to 50% of the times, 3 to 33%.
    [SerializeField] private int spawnRarity;

    // reference to world manager for taking values about growth
    [SerializeField] private WorldManager worldManager;

    private int spawnCounter;
    private Dictionary<Vector3Int, GameObject> fertileTiles;

    private void Awake()
    {
        spawnCounter = 0;
    }

    private void FixedUpdate()
    {
        if(spawnCounter == spawnRarity)
        {
            spawnCounter = 0;

            if(Random.Range(1, spawnRarity + 1) == 1)
            {

                float areaCovered = ((float)worldManager.GetNumOfPlants()) / worldManager.GetMaxNumOfPlants();

                if(areaCovered < maxPlantDencity)
                {

                    if((areaCovered - maxPlantDencity) * 10 < 1)
                    {
                        this.SpawnPlant(1);
                        return;
                    }

                    this.SpawnPlant((int)Random.Range(1, Mathf.Floor((areaCovered - maxPlantDencity) * 10)) + 1);
                }
            }
        }

        spawnCounter++;
    }

    private void SpawnPlant(int numOfPlants)
    {
        fertileTiles = worldManager.GetFertileTiles();
        // spawn a plant and increment number of plants in the world
        for (int i = 0; i < numOfPlants; i++)
        {
            GameObject tile = fertileTiles.ElementAt(Random.Range(0, fertileTiles.Count)).Value;
            
            
            if(Random.Range(1, tile.GetComponent<TileInfo>().GetGrowthDifficulty() + 1) == 1)
            {
                GameObject plant = Instantiate(tile.GetComponent<TileInfo>().GetRandomPlant()) as GameObject;
                
                plant.transform.position = tile.transform.position;

                worldManager.AddPlant(new Vector3Int((int)plant.transform.position.x, (int)plant.transform.position.y, 0), plant);
            }

            else
            {
                i--;
            }
        }
        
    }
}
