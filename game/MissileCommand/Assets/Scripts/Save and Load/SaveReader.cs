using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveReader : MonoBehaviour
{
    private StreamReader reader;
    private string line;
    private List<string> leaders;
    private List<int> scores;

    public void ParseSave(string path)
    {
        leaders = new List<string>();
        scores = new List<int>();

        reader = new StreamReader(path);

        line = reader.ReadLine();

        while (line != null)
        {

            int i = 0;
            while (char.IsNumber(line[i]) == false)
            {
                i++;
            }

            leaders.Add(line.Substring(0, i));
            scores.Add(Int32.Parse(line.Substring(i)));

            line = reader.ReadLine();

            Debug.Log("Score read");
        }

        reader.Close();
    }

    public List<string> GetLeaders()
    {
        return leaders;
    }

    public List<int> GetScores()
    {
        return scores;
    }
}
