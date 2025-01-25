using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
    private Vector3 scorePosition;
    private List<Highscore> highscoreList;
    // Start is called before the first frame update
    void Start()
    {
        highscoreList = new List<Highscore>();
        //Testentry
        highscoreList.Add(new Highscore("A", 100));
        highscoreList.Add(new Highscore("B", 200));
        highscoreList.Add(new Highscore("C", 400));
        highscoreList.Add(new Highscore("D", -100));

        foreach (Highscore highscore in highscoreList)
        {
            scoreboardEntry.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = highscore.name;
            scoreboardEntry.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = highscore.score.ToString();
            GameObject sE = Renderer.Instantiate(scoreboardEntry);
            sE.transform.SetParent(transform, false);
        }
    }

    public void WriteHighscore(string entry)
    {
        Highscore highscore = new Highscore(entry, GameStats.instance.GetScore());
        highscoreList.Add(highscore);
    }
}
