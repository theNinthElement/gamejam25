using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] RikschaController player;
    public void PlayerCollision(float force)
    {
        player.AddExternalPushForce(force);
    }

    public void PauseGame()
    {
        Time.timeScale = 0.0f;
    }
    public void ContinueGame()
    {
        Time.timeScale = 1.0f;
    }
    public void GameOver()
    {
        //TODO: Stop Game, Show Scoreboard, Player can add his Score
    }
    public void EndGame()
    {
        Application.Quit();
    }
}
