using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rigidBody;

    public float minimumSpeed = 15f;

    private Vector3 lastVelocity;

    void Start(){
        AddForceToBallsRigidbody();
    }

    void AddForceToBallsRigidbody(){
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.AddForce(new Vector2(9.8f * 40f, 9.8f * 40f));
    }

    void Update(){
        UpdateVelocity();
    }
    
    void UpdateVelocity(){
        lastVelocity = rigidBody.velocity;
    }

    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "Boundary"){
            GameObject.Find("ControlGame").GetComponent<MenuInGame>().PlayBounceSoundEffect();
            RotateBallWithReflectingIncomingVelocity(other);
        }
        else if(other.gameObject.tag == "OutBoundary"){

            if(other.gameObject.name == "RightBoundary"){
                GameObject.Find("ControlGame").GetComponent<MenuInGame>().PlayerTwoScoreUp();
            }
            else if(other.gameObject.name == "LeftBoundary"){
                GameObject.Find("ControlGame").GetComponent<MenuInGame>().PlayerOneScoreUp();
            }
            ResetBall();

        }
        else if( other.gameObject.tag == "Paddle"){
            GameObject.Find("ControlGame").GetComponent<MenuInGame>().PlayBounceSoundEffect();
            RotateBallWithCalculatedAngleByDistanceFromPaddlesCenter(other);
        }    
    }

    void ResetBall(){
        rigidBody.velocity = new Vector2(0.0f, 0.0f);
        transform.localPosition = new Vector2(2.238f, 4.609f);
        if(!GameObject.Find("ControlGame").GetComponent<MenuInGame>().IsGameOver()){
            rigidBody.AddForce(new Vector2(9.8f * 40f, 9.8f * 40f));
        }
        else{
            GameObject.Find("Player1").GetComponent<Paddle>().IsGameOver();
        }
    }

    void RotateBallWithCalculatedAngleByDistanceFromPaddlesCenter(Collision2D paddle){

        float distance = DistanceBetweenCollidePointAndPaddlesCenter(paddle);

        float normalizedDistance = DivideDistanceToHalfOfPaddlesWidth(distance, paddle);

        float angle = MultiplyNormalizedDistanceWithFourtyFiveDegree(normalizedDistance);

        angle = CheckIfAngleValid(angle);

        float directionX = CalculateNextXDireciton(paddle);

        float ballVelocityX = directionX * minimumSpeed * Mathf.Cos(angle);
        float ballVelocityY = minimumSpeed * Mathf.Sin(angle);

        rigidBody.velocity = new Vector2(ballVelocityX, ballVelocityY);
    }

    float DistanceBetweenCollidePointAndPaddlesCenter(Collision2D paddle){
        return transform.localPosition.y - paddle.gameObject.transform.position.y;
    }

    float DivideDistanceToHalfOfPaddlesWidth(float distance, Collision2D paddle){
        return distance / (paddle.gameObject.GetComponent<BoxCollider2D>().size.y / 2);
    }

    float MultiplyNormalizedDistanceWithFourtyFiveDegree(float normalizedDistance){
       return normalizedDistance * (Mathf.PI/4); // pi/4 is 45 degree in radians.
    }

    float CheckIfAngleValid(float angle){
        //0.785398 is 45 degree in radians
        if(angle > 0.785398f){
            angle = 0.785398f;
        }
        else if(angle < -0.785398f){
            angle = -0.785398f;
        }
        return angle;
    }

    float CalculateNextXDireciton(Collision2D paddle){
        float direction = 1f;
        if(paddle.gameObject.name == "Player1"){
            direction = -1f;
        }
        else if(paddle.gameObject.name == "Player2"){
            direction = 1f;
        }
        return direction;
    }

    void RotateBallWithReflectingIncomingVelocity(Collision2D hitObject){
        float speed = CalculateSpeedByLastVelocitysMagnitude();

        Vector3 normalizedVelocity = NormalizeLastVelocity();
        
        Vector3 direction = ReflectIncomingAngle(normalizedVelocity, hitObject);
        
        rigidBody.velocity = direction * Mathf.Max(speed, minimumSpeed);
    }

    float CalculateSpeedByLastVelocitysMagnitude(){
        return lastVelocity.magnitude;
    }

    Vector3 NormalizeLastVelocity(){
        Vector3 normalizedVelocity = lastVelocity.normalized;

        return normalizedVelocity;
    }

    Vector3 ReflectIncomingAngle(Vector3 normalizedVelocity, Collision2D hitObject){
        return Vector3.Reflect(normalizedVelocity, hitObject.contacts[0].normal);
    }

    public void PlayAgain(){
        GameObject.Find("Player1").GetComponent<Paddle>().PlayAgain();
        rigidBody.AddForce(new Vector2(9.8f * 40f, 9.8f * 40f));        
    }
}
