using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Paddle : MonoBehaviour
{
    public float speed = 12.0f;

    Vector2 GetCurrentPosition()
    {
        return transform.localPosition;
    }

    void UpdatePosition(Vector2 currentPosition)
    {
        transform.localPosition = currentPosition;
    }

    public void MovePaddleToPosition(Touch touch){
        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
        
        if(touchPosition.y < 7.8f && touchPosition.y > 0.8f){
            transform.DOMoveY(touchPosition.y, 0.2f, false);
        }
    }

    public void MoveUp(){
        Vector2 currentPosition = GetCurrentPosition();

        currentPosition.y += speed * Time.deltaTime;

        if(currentPosition.y < 7.9f){
            UpdatePosition(currentPosition);
        }
    }

    public void MoveDown(){
        Vector2 currentPosition = GetCurrentPosition();

        currentPosition.y -= speed * Time.deltaTime;

        if(currentPosition.y > 0.6f){
            UpdatePosition(currentPosition);
        }
    }

    public void ResetPaddlesPosition(){
        transform.localPosition = new Vector3(11.0f, 4.0f, 0.0f);
    }
}

