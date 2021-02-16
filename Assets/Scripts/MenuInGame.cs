using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuInGame : MonoBehaviour
{
    public TMP_Text playerOneScoreText;
    public TMP_Text playerTwoScoreText;

    public GameObject winElements;

    private int playerOneScore = 0;
    private int playerTwoScore = 0;

    AudioSource audioSource;
    public AudioClip bounceSoundEffect;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
                ReplaceWinUIElementsToXPosition(-60.0f);
            }
            else if(playerTwoScore == 10){
                ReplaceWinUIElementsToXPosition(-380.0f);
            }

            ShowWinUIElements();
            
            return true;
        }
        return false;
    }

    void ShowWinUIElements(){
        winElements.SetActive(true);
    }

    void HideWinUIElements(){
        winElements.SetActive(false);
    }

    void ReplaceWinUIElementsToXPosition(float value){
        Vector2 newPosition = new Vector2(value, -50.0f);
        winElements.GetComponent<RectTransform>().anchoredPosition  = newPosition;
    }

    public void PlayBounceSoundEffect(){
        audioSource.PlayOneShot(bounceSoundEffect);
    }

    public void PlayAgain(){
        HideWinUIElements();

        GameObject.Find("Ball").GetComponent<Ball>().PlayAgain();
        
        ResetScore();
    }

    void ResetScore(){
        playerOneScore = 0;
        playerTwoScore = 0;

        playerOneScoreText.text = playerOneScore.ToString();
        playerTwoScoreText.text = playerTwoScore.ToString();
    }
}
