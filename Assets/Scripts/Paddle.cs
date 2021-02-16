using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Paddle : MonoBehaviour
{
    public float speed = 12.0f;

    public LayerMask floorMask;

    Vector2 previousUnitPosition = Vector2.zero;

    Vector2 direction = Vector2.zero;

    private int touchSensetivityVertical = 2;

    private bool move, moveUp, moveDown;

    private GameStates gameState;

    enum GameStates
    {
        playing,
        gameOver
    }

    void Start()
    {
        gameState = GameStates.playing;
    }

    void FixedUpdate()
    {
       CheckInput();
    }

    Vector2 GetCurrentPosition()
    {
        return transform.localPosition;
    }

    void UpdatePosition(Vector2 currentPosition)
    {
        transform.localPosition = currentPosition;
    }

    void CheckInput()
    {
        if(gameState == GameStates.playing){

            #if UNITY_ANDROID 
                if(Input.touchCount > 0){
                    Touch touch = Input.GetTouch(0);

                    if(touch.phase == TouchPhase.Began){
                        previousUnitPosition = new Vector2(touch.position.x, touch.position.y);
                        
                        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                        if(touchPosition.y < 7.8f && touchPosition.y > 0.8f){
                            transform.DOMoveY(touchPosition.y, 0.2f, false);
                        }

                    }
                    else if(touch.phase == TouchPhase.Moved){
                        Vector2 touchDeltaPosition = touch.deltaPosition;

                        direction = touchDeltaPosition.normalized;

                        if(Mathf.Abs(touch.position.y - previousUnitPosition.y) >= touchSensetivityVertical && touch.deltaPosition.x > -10 && touch.deltaPosition.x < 10){
                            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                            if(touchPosition.y < 7.8f && touchPosition.y > 0.8f){
                                transform.DOMoveY(touchPosition.y, 0.2f, false);
                            }
                        }
                    }
                }
            #else

                if(Input.GetKey(KeyCode.UpArrow))
                {
                    MoveUp();
                }
                
                else if(Input.GetKey(KeyCode.DownArrow))
                {
                    MoveDown();
                }

            #endif

        }
    }

    void MoveUp(){
        Vector2 currentPosition = GetCurrentPosition();

        currentPosition.y += speed * Time.deltaTime;
        currentPosition = ReturnFixedPositionIfPaddleCollidingWithTopBoundaryAt(currentPosition);

        UpdatePosition(currentPosition);
    }

    void MoveDown(){
        Vector2 currentPosition = GetCurrentPosition();

        currentPosition.y -= speed * Time.deltaTime;
        currentPosition =  ReturnFixedPositionIfPaddleCollidingWithBottomBoundaryAt(currentPosition);

        UpdatePosition(currentPosition);
    }

    Vector2 ReturnFixedPositionIfPaddleCollidingWithTopBoundaryAt(Vector2 currentPosition)
    {
        //For find out if collide happened, we use "raycasting" method. 
        //Basically we take out 3 rays(top left, top right, top middle) from paddles top 
        //and we check the distance with boundary and paddle, if its short enough for collision we fix the paddles position
        Vector2 topRightOfPaddle = new Vector2(currentPosition.x - 1.1f, currentPosition.y + 1f+ 0.2f);
        Vector2 topLeftOfPaddle = new Vector2(currentPosition.x - 1.55f, currentPosition.y + 1f + 0.2f);
        Vector2 topMiddleOfPaddle = new Vector2(currentPosition.x - 1.3f, currentPosition.y + 1f+ 0.2f);

        //Then create rays from points which we just assigned above.
        RaycastHit2D hitRight = Physics2D.Raycast(topRightOfPaddle, Vector2.up, speed * Time.deltaTime, floorMask);
        RaycastHit2D hitLeft = Physics2D.Raycast(topLeftOfPaddle, Vector2.up, speed * Time.deltaTime, floorMask);
        RaycastHit2D hitMiddle = Physics2D.Raycast(topMiddleOfPaddle, Vector2.up, speed * Time.deltaTime, floorMask);

        if(hitRight.collider != null || hitLeft.collider != null || hitMiddle.collider != null)
        {
            //If rays hit any collider close enough
            //then we find out which collider that we hit
            RaycastHit2D hitRay = hitRight;

            if(hitRay.rigidbody != null){
                
            }
            else{
                if(hitRay.rigidbody != null){
                    return currentPosition;
                }
                else if(hitLeft)
                    hitRay = hitLeft;

                else if(hitRight)
                    hitRay = hitLeft;

                else if(hitMiddle)
                    hitRay = hitMiddle;

                //And fix the paddles position by collider that we hit
                currentPosition.y = hitRay.collider.bounds.center.y - hitRay.collider.bounds.size.y / 2f - 1f - 0.2f;
            }
        }
        return currentPosition;
    }

    Vector2 ReturnFixedPositionIfPaddleCollidingWithBottomBoundaryAt(Vector2 currentPosition)
    {
        Vector2 bottomLeftOfPaddle = new Vector2(currentPosition.x - 1.55f, currentPosition.y - 1f + 0.192f);
        Vector2 bottomRightOfPaddle = new Vector2(currentPosition.x - 1.1f, currentPosition.y - 1f + 0.192f);
        Vector2 bottomMiddleOfPaddle = new Vector2(currentPosition.x - 1.3f, currentPosition.y - 1f + 0.192f);

        RaycastHit2D hitLeft = Physics2D.Raycast(bottomLeftOfPaddle, Vector2.down, speed * Time.deltaTime, floorMask);
        RaycastHit2D hitRight = Physics2D.Raycast(bottomRightOfPaddle, Vector2.down, speed * Time.deltaTime, floorMask);
        RaycastHit2D hitMiddle = Physics2D.Raycast(bottomMiddleOfPaddle, Vector2.down, speed * Time.deltaTime, floorMask);

        if(hitLeft.collider != null || hitRight.collider != null || hitMiddle.collider != null)
        {
            RaycastHit2D hitRay = hitLeft;

            if(hitRay.rigidbody != null){

            }
            else{
                if(hitRay.rigidbody != null){
                    return currentPosition;
                }
                else if(hitLeft){
                    hitRay = hitLeft;
                }
                else if(hitRight){
                    hitRay = hitRight;
                }
                else if(hitMiddle){
                    hitRay =  hitMiddle;
                }
                currentPosition.y = hitRay.collider.bounds.center.y + hitRay.collider.bounds.size.y / 2f + 1f - 0.19f;
            }
        }
        return currentPosition;
    }  

    public void IsGameOver(){
        gameState = GameStates.gameOver;
    }  

    public void PlayAgain(){
        gameState = GameStates.playing;
        transform.localPosition = new Vector3(11.0f, 4.0f, 0.0f);
    }
}
