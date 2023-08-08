using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField] List<GameObject> tiles;
    [SerializeField] List<string> tileNames;

    private Dictionary<string, GameObject> tilePrefabs;

    private void Awake()
    {
        tilePrefabs = new Dictionary<string, GameObject>();

        for(int i = 0; i < tiles.Count; i++)
        {
            tilePrefabs.Add(tileNames[i], tiles[i]);
        }

        tiles.Clear();
        tileNames.Clear();
    }

    public GameObject GetTile(string s)
    {
        return tilePrefabs.GetValueOrDefault(s);
    }
}
