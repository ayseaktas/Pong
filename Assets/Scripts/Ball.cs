using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    public float minimumSpeed = 15f;

    private Vector3 lastVelocity;

    void Start(){
        GameObject.Find("Ball").GetComponent<BallRigidbody>().AddForceToBallsRigidbody();
    }

    void Update(){
        lastVelocity = GameObject.Find("Ball").GetComponent<BallRigidbody>().getRigidbodyVelocity();
    }

    public void RotateBallWithCalculatedAngleByDistanceFromPaddlesCenter(Collision2D paddle){

        float distance = DistanceBetweenCollidePointAndPaddlesCenter(paddle);

        float normalizedDistance = DivideDistanceToHalfOfPaddlesWidth(distance, paddle);

        float angle = MultiplyNormalizedDistanceWithFourtyFiveDegree(normalizedDistance);

        angle = CheckIfAngleValid(angle);

        float directionX = CalculateNextXDireciton(paddle);

        float ballVelocityX = directionX * minimumSpeed * Mathf.Cos(angle);
        float ballVelocityY = minimumSpeed * Mathf.Sin(angle);

        Vector2 newVelocity = new Vector2(ballVelocityX, ballVelocityY);
        GameObject.Find("Ball").GetComponent<BallRigidbody>().setRigidbodyVelocity(newVelocity);
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

    public void RotateBallWithReflectingIncomingVelocity(Collision2D hitObject){
        float speed = CalculateSpeedByLastVelocitysMagnitude();

        Vector3 normalizedVelocity = NormalizeLastVelocity();
        
        Vector2 direction = ReflectIncomingAngle(normalizedVelocity, hitObject);

        Vector2 newVelocity = direction * Mathf.Max(speed, minimumSpeed);
        
        GameObject.Find("Ball").GetComponent<BallRigidbody>().setRigidbodyVelocity(newVelocity);
    }

    float CalculateSpeedByLastVelocitysMagnitude(){
        return lastVelocity.magnitude;
    }

    Vector2 NormalizeLastVelocity(){
        Vector2 normalizedVelocity = lastVelocity.normalized;

        return normalizedVelocity;
    }

    Vector3 ReflectIncomingAngle(Vector3 normalizedVelocity, Collision2D hitObject){
        return Vector3.Reflect(normalizedVelocity, hitObject.contacts[0].normal);
    }
}
