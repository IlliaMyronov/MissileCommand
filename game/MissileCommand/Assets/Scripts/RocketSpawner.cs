using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSpawner : MonoBehaviour
{
    [SerializeField] private GameObject rocketPrefab;
    [SerializeField] private int yStartPosition;
    [SerializeField] private int xLowerBoundary;
    [SerializeField] private int xHigherBoundary;
    [SerializeField] private int lowBoundary;
    private List<GameObject> rocketList;

    private void Awake()
    {
        rocketList = new List<GameObject>();
    }
    public void GenerateRocket()
    {
        GameObject rocket = Instantiate(rocketPrefab) as GameObject;
        rocket.transform.position = new Vector3(Random.Range(xLowerBoundary, xHigherBoundary), yStartPosition, 0);
        rocketList.Add(rocket);
    }

    private void FixedUpdate()
    {

        for(int i  = 0; i < rocketList.Count; i++)
        {
            if (rocketList[i].transform.position.y < lowBoundary)
            {
                Destroy(rocketList[i]);
                rocketList.RemoveAt(i);

            }
        }
    }
}
