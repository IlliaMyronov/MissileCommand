using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    [SerializeField] private WorldGen worldGen;

    private List<List<GameObject>> map;
    private List<List<GameObject>> plants;
    private int numOfPlants;
    private int maxNumOfPlants;
    private Dictionary<Vector3Int, GameObject> fertileTiles;

    private void Awake()
    {
        numOfPlants = 0;
        fertileTiles = new Dictionary<Vector3Int, GameObject>();
    }

    private void Start()
    {
        map = worldGen.GenerateWorld();
        plants = worldGen.GeneratePlants();
        this.FillOutFertileTiles();
        maxNumOfPlants = fertileTiles.Count;
    }

    private void FillOutFertileTiles()
    {

        for(int i = 0; i < map.Count; i++)
        {

            for(int j = 0; j < map[i].Count; j++)
            {

                if (map[i][j].GetComponent<TileInfo>().IsFertile())
                    fertileTiles.Add(new Vector3Int((int)map[i][j].transform.position.x, (int)map[i][j].transform.position.y, 0), map[i][j]);
            }
        }
        Debug.Log(fertileTiles.Count);
    }

    public int GetNumOfPlants()
    {
        return numOfPlants; 
    }

    public int GetMaxNumOfPlants()
    {
        return maxNumOfPlants;
    }

    public Dictionary<Vector3Int, GameObject> GetFertileTiles()
    {
        return fertileTiles;
    }

    public void AddPlant(Vector3Int coordinates, GameObject plant)
    {
        numOfPlants++;

        plants[coordinates.y][coordinates.x] = plant;

        fertileTiles.Remove(coordinates);
    }
}
