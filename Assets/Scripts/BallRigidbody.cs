using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRigidbody : MonoBehaviour
{
    private Rigidbody2D rigidBody;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    public void AddForceToBallsRigidbody(){
        rigidBody.AddForce(new Vector2(9.8f * 40f, 9.8f * 40f));
    }

    public Vector2 getRigidbodyVelocity(){
        return rigidBody.velocity;
    }

    public void setRigidbodyVelocity(Vector2 velocity){
        rigidBody.velocity = velocity;
    }


}
