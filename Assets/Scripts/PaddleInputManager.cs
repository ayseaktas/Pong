using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleInputManager : MonoBehaviour
{

    void FixedUpdate()
    {
        if(GameObject.Find("ControlGame").GetComponent<GameStateManager>().IsPlaying()){
            CheckInput();
        }
    }

    void CheckInput()
    {
        #if UNITY_ANDROID 
            if(Input.touchCount > 0){
                Touch touch = Input.GetTouch(0);

                if(touch.phase == TouchPhase.Began){                        
                    GameObject.Find("Player1").GetComponent<Paddle>().MovePaddleToPosition(touch);
                }

                else if(touch.phase == TouchPhase.Moved){
                    GameObject.Find("Player1").GetComponent<Paddle>().MovePaddleToPosition(touch);
                }
            }

        #else
            if(Input.GetKey(KeyCode.UpArrow))
            {
                GameObject.Find("Player1").GetComponent<Paddle>().MoveUp();
            }
            
            else if(Input.GetKey(KeyCode.DownArrow))
            {
                GameObject.Find("Player1").GetComponent<Paddle>().MoveDown();
            }

        #endif
    }
}
