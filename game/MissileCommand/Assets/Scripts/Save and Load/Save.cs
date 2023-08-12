using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Save : MonoBehaviour
{
    [SerializeField] private int maxNumOfLeaders;
    [SerializeField] private GameObject userNameEntree;
    [SerializeField] private GameObject userScore;
    [SerializeField] private GameObject buttonPressResponse;
    [SerializeField] private SaveReader saveReader;

    private string path;
    private List<string> leaderNames;
    private List<int> highScores;
    private StreamWriter writer;

    private void Awake()
    {
        highScores = new List<int>();
        leaderNames = new List<string>();

        path = Application.dataPath + "/Saves/Scores.txt";

        if(!File.Exists(path))
        {
            File.Create(path);
        }

        saveReader.ParseSave(path);
        leaderNames = saveReader.GetLeaders();
        highScores = saveReader.GetScores();
    }

    private void PerformSave()
    {
        File.WriteAllText(path, String.Empty);

        writer = new StreamWriter(path);

        string saveLine = string.Empty;

        for (int i = 0; i < highScores.Count; i++)
        {
            saveLine = leaderNames[i] + highScores[i];
            writer.WriteLine(saveLine);
        }

        writer.Close();
    }

    private string AddScore(int score, string playerName)
    {
        // if there are no scores just add a score
        if(highScores.Count == 0)
        {
            highScores.Add(score);
            leaderNames.Add(playerName);

            this.PerformSave();
            return "Your score has been saved";
        }

        // if scores are full and your score is too small don't add

        if(highScores.Count > maxNumOfLeaders && highScores[highScores.Count - 1] > score)
        {
            return "Unfortunatly your score is insufficient to enter the leaderboard";
        }

        // check if this score already exists

        if(highScores.IndexOf(score) == leaderNames.IndexOf(playerName) && highScores.IndexOf(score) != -1)
        {
            return "This score with your name seems to exist already";

        }

        int counter = 0;

        while (highScores[counter] > score) 
        { 
            counter++;
        }

        highScores.Insert(counter, score);
        leaderNames.Insert(counter, playerName);

        if(highScores.Count > maxNumOfLeaders)
        {

            highScores.RemoveAt(highScores.Count - 1);
            leaderNames.RemoveAt(leaderNames.Count - 1);

        }

        this.PerformSave();
        return "Your score has been saved";
    }

    public void SaveButtonClicked()
    {
        Debug.Log(Int32.Parse(userScore.GetComponent<Text>().text));
        Debug.Log(userNameEntree.GetComponent<TMP_InputField>().text);
        buttonPressResponse.GetComponent<Text>().text = this.AddScore(Int32.Parse(userScore.GetComponent<Text>().text), userNameEntree.GetComponent<TMP_InputField>().text);
    }
}
