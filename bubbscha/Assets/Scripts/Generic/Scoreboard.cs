using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Scoreboard : MonoBehaviour
{
    public struct Highscore
    {
        string name;
        int score;
        public Highscore(string name, int score)
        {
            this.name = name;
            this.score = score;
        }
    }
    private List<Highscore> highscoreList;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void WriteHighscore(string entry)
    {
        Highscore highscore = new Highscore(entry, 0);
        highscoreList.Add(highscore);
    }
}
