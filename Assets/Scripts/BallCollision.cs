using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollision : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "Boundary"){
            GameObject.Find("ControlGame").GetComponent<GameSoundManagement>().PlayBounceSoundEffect();
            GameObject.Find("Ball").GetComponent<Ball>().RotateBallWithReflectingIncomingVelocity(other);
        }
        else if(other.gameObject.tag == "OutBoundary"){

            if(other.gameObject.name == "RightBoundary"){
                GameObject.Find("ControlGame").GetComponent<ScoreManagement>().PlayerTwoScoreUp();
            }
            else if(other.gameObject.name == "LeftBoundary"){
                GameObject.Find("ControlGame").GetComponent<ScoreManagement>().PlayerOneScoreUp();
            }
            GameObject.Find("ControlGame").GetComponent<ScoreManagement>().IsGameOver();
            ResetBall();

        }
        else if( other.gameObject.tag == "Paddle"){
            GameObject.Find("ControlGame").GetComponent<GameSoundManagement>().PlayBounceSoundEffect();
            GameObject.Find("Ball").GetComponent<Ball>().RotateBallWithCalculatedAngleByDistanceFromPaddlesCenter(other);
        }    
    }
    
    void ResetBall(){
        Vector2 initialVelocity = new Vector2(0.0f, 0.0f);
        GameObject.Find("Ball").GetComponent<BallRigidbody>().setRigidbodyVelocity(initialVelocity);

        transform.localPosition = new Vector2(2.238f, 4.609f);
        if(GameObject.Find("ControlGame").GetComponent<GameStateManager>().IsPlaying()){
            GameObject.Find("Ball").GetComponent<BallRigidbody>().AddForceToBallsRigidbody();
        }
    }
}
