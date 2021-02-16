using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PaddleAI : MonoBehaviour
{
    private Vector2 ballsPosition;

    void Update(){
        FollowTheBall();
    }

    void UpdatePaddlesCurrentPosition(Vector2 paddlesCurrentPosition){
        transform.localPosition = paddlesCurrentPosition;
    }

    Vector2 GetPaddlesCurrentPosition(){
        return transform.localPosition;
    }

    Vector2 GetBallsCurrentPosition(){
        return GameObject.Find("Ball").transform.position;
    }

    void FollowTheBall(){
        Vector2 paddlesCurrentPosition = GetPaddlesCurrentPosition();

        Vector2 ballsCurrentPosition = GetBallsCurrentPosition();
        
        if(ballsCurrentPosition.y < 7.8f && ballsCurrentPosition.y > 0.8f){
            transform.DOMoveY(ballsCurrentPosition.y, 1.0f, false);
        }
    }

}
