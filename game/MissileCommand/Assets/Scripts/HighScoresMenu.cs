using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoresMenu : MonoBehaviour
{
    [SerializeField] private GameObject leaderPrefab;
    [SerializeField] private Vector2 leaderboardPos;
    [SerializeField] private int spaceBetweenLeaders;
    [SerializeField] private int maxLeaders;
    [SerializeField] private SaveReader saveReader;

    private List<int> scores;
    private List<string> leaders;
    private string path;

    private void Awake()
    {
        path = Application.dataPath + "/Saves/Scores.txt";
    }
    private void Start()
    {
        saveReader.ParseSave(path);

        scores = saveReader.GetScores();
        leaders = saveReader.GetLeaders();

        for (int i = 0; i < scores.Count; i++)
        {

            GameObject newLeader = Instantiate(leaderPrefab) as GameObject;

            newLeader.transform.SetParent(this.transform);
            newLeader.transform.position = new Vector3(leaderboardPos.x, leaderboardPos.y - (spaceBetweenLeaders * i), 0);

            newLeader.transform.GetChild(0).GetComponent<Text>().text = "00" + (i + 1);
            newLeader.transform.GetChild(1).GetComponent<Text>().text = leaders[i];
            newLeader.transform.GetChild(2).GetComponent<Text>().text = scores[i].ToString();
        }
    }
}
