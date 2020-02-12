using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racket : MonoBehaviour
{

    //speed
    public float speed = 400;

    void FixedUpdate()
    {
        //horizontal input
        float h = Input.GetAxisRaw("Horizontal");

        //Velocity
        GetComponent<Rigidbody2D>().velocity = Vector2.right * h * speed/100;
    }
}
