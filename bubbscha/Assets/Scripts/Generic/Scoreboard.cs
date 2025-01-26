using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using static Scoreboard;
using static UnityEngine.EventSystems.EventTrigger;


public class Scoreboard : MonoBehaviour
{
    public struct Highscore
    {
        public string name;
        public int score;
        public Highscore(string name, int score)
        {
            this.name = name;
            this.score = score;
        }
    }
    [SerializeField] GameObject scoreboardEntry;
    public static List<Highscore> highscoreList;
    public static int scoreboardSize = 7;
    void Start()
    {

        highscoreList = new List<Highscore>();
        highscoreList = HighScoreXML.instance.ReadScores();
        highscoreList = highscoreList.OrderByDescending(h => h.score).ToList();
        if (highscoreList.Count > scoreboardSize)
        {
            highscoreList.RemoveRange(scoreboardSize, highscoreList.Count - scoreboardSize);
        }
            for (int i = 0; i < highscoreList.Count && i < scoreboardSize; i++)
        {
            InstanceHighscore(highscoreList[i]);
        }
    }

    public void WriteHighscore(string entry)
    {
        Highscore highscore = new Highscore(entry, GameStats.instance.GetScore());
        highscoreList.Add(highscore);
        highscoreList = highscoreList.OrderByDescending(h => h.score).ToList();
        InstanceHighscore(highscore);
        HighScoreXML.instance.WriteScores(highscoreList);
    }

    private void InstanceHighscore(Highscore entry)
    {
        scoreboardEntry.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = entry.name;
        scoreboardEntry.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = entry.score.ToString();
        GameObject sE = Renderer.Instantiate(scoreboardEntry);
        sE.transform.SetParent(transform, false);
        sE.transform.SetSiblingIndex(highscoreList.FindIndex(e => e.name == entry.name));
    }
}
