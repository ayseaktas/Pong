using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManagement : MonoBehaviour
{
    public TMP_Text playerOneScoreText;
    public TMP_Text playerTwoScoreText;

    private int playerOneScore = 0;
    private int playerTwoScore = 0;

    void Start()
    {
        playerOneScoreText.text = playerOneScore.ToString();
        playerTwoScoreText.text = playerTwoScore.ToString();
    }

    public void PlayerOneScoreUp(){
        playerOneScore++;

        playerOneScoreText.text = playerOneScore.ToString();

    }
    public void PlayerTwoScoreUp(){
        playerTwoScore++;

        playerTwoScoreText.text = playerTwoScore.ToString();
    }

    public bool IsGameOver(){
        if(playerOneScore == 10 || playerTwoScore == 10){

            if(playerOneScore == 10){
                GameObject.Find("ControlGame").GetComponent<WinElementsManagement>().ReplaceWinUIElementsToXPosition(-60.0f);
            }
            else if(playerTwoScore == 10){
                GameObject.Find("ControlGame").GetComponent<WinElementsManagement>().ReplaceWinUIElementsToXPosition(-380.0f);
            }

            GameObject.Find("ControlGame").GetComponent<WinElementsManagement>().ShowWinUIElements();
            
            return true;
        }
        return false;
    }

    public void PlayAgain(){
        GameObject.Find("ControlGame").GetComponent<WinElementsManagement>().HideWinUIElements();
        
        ResetScore();
    }

    private void ResetScore(){
        playerOneScore = 0;
        playerTwoScore = 0;

        playerOneScoreText.text = playerOneScore.ToString();
        playerTwoScoreText.text = playerTwoScore.ToString();
    }
}
