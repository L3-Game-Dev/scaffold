// HighscoreStorer
// Handles storing & retrieving highscores
// Created by Dima Bethune 22/06

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class HighscoreStorer : MonoBehaviour
{
    public static string file = "random_first_names.csv"; // Testing file 1 (100 values)
    //public static string file = "blank.csv"; // Testing file 2 (blank data)
    //public static string file = "highscores.csv";
    public static string path = Application.dataPath + "/HIGHSCORES/" + file;
    public static string data;

    // Whether new highscores are currently being stored
    public static bool storingHighscores = true;

    public static void ReadFile()
    {
        data = "";

        if (File.Exists(path))
        {
            FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite);
            StreamReader read = new StreamReader(fileStream);
            data = read.ReadToEnd();
            fileStream.Close();
        }
        else Debug.Log("No file found at " + path);
    }

    public static void SaveHighscore(string name, string time)
    {
        if (storingHighscores)
        {
            StreamWriter writer = new StreamWriter(path, true);

            writer.Write("\n" + time + "," + name);

            writer.Flush();
            writer.Close();
        }
    }

    public static int GetHighscoreCount()
    {
        ReadFile();

        string[] lines = data.Split("\n");

        return lines.Length;
    }

    public static Tuple<string, string> GetHighscores(int i)
    {
        ReadFile();

        string[] lines = data.Split("\n");
        Array.Sort(lines);

        string[] parts = lines[i].Split(",");

        if (parts.Length > 1)
            return new Tuple<string, string>(parts[1], parts[0]);
        else
            return null;
    }
}
