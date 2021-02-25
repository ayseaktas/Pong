using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public enum GameStates
    {
        playing,
        gameOver
    }

    public GameStates gameState;

    void Start()
    {
        gameState = GameStates.playing;
    }

    public void GameOver(){
        gameState = GameStates.gameOver;
    }

    public void PlayAgain(){
        gameState = GameStates.playing;
        GameObject.Find("Ball").GetComponent<BallRigidbody>().AddForceToBallsRigidbody();
        GameObject.Find("Player1").GetComponent<Paddle>().ResetPaddlesPosition();
        GameObject.Find("ControlGame").GetComponent<WinElementsManagement>().HideWinUIElements();
        GameObject.Find("ControlGame").GetComponent<ScoreManagement>().ResetScore();

    }

    public bool IsPlaying(){
        if(gameState == GameStates.playing){
            return true;
        }
        return false;
    }

}
