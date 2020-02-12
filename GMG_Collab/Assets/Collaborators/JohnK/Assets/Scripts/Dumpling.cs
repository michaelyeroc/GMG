using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dumpling : MonoBehaviour
{
    //speed
    public float speed = 100.0f;


    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.up * speed / 100;
    }

    
}
