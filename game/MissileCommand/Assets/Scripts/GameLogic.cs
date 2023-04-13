using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    [SerializeField] private RocketSpawner spawnerScript;
    private float timeSinceLastSpawn = 0;

    private void Update()
    {
        if(timeSinceLastSpawn < 2)
        {
            timeSinceLastSpawn += Time.deltaTime;
        }
        else
        {
            spawnerScript.GenerateRocket();
            timeSinceLastSpawn = 0;
        }
    }
}
