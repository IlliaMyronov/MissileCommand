using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfo : MonoBehaviour
{
    [SerializeField] private int growthDifficulty;
    [SerializeField] private int moveDifficulty;
    [SerializeField] private List<GameObject> growableObjects;

    public int GetMoveDifficulty()
    {
        return moveDifficulty;
    }

    public bool IsFertile()
    {
        return (growableObjects.Count == 0) ? false : true;
    }

    public int GetGrowthDifficulty()
    {
        return growthDifficulty;
    }

    public GameObject GetRandomPlant()
    {
        return growableObjects[(int)Random.Range(0, growableObjects.Count)];
    }
}
