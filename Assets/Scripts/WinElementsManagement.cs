using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinElementsManagement : MonoBehaviour
{

    public GameObject winElements;

    public void ShowWinUIElements(){
        winElements.SetActive(true);
    }

    public void HideWinUIElements(){
        winElements.SetActive(false);
    }

    public void ReplaceWinUIElementsToXPosition(float value){
        Vector2 newPosition = new Vector2(value, -50.0f);
        winElements.GetComponent<RectTransform>().anchoredPosition  = newPosition;
    }

}
