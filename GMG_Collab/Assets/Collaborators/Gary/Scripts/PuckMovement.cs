#region File Description
//-----------------------------------------------------------------------------
// PuckMovement.cs
//
// Copyright (C) Allegro Interactive. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Garys_Work
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PuckMovement : MonoBehaviour
    {	
        
    #region fields
        bool isActive = true;
        bool moveActive;
        
        Rigidbody2D ballRigidBody;

        Vector2 inDirection;

    #endregion
        
    #region properties
        public float MovementSpeed;
    #endregion
        
        void OnEnable() 
        {
            // make event subscriptions
        
            EventManager.OnLevelStart += LevelStartEvent;
        }

        void OnDisable()
        {
            // remove event subscriptions
        
            EventManager.OnLevelStart -= LevelStartEvent;
        }

        void LevelStartEvent() 
        {
            moveActive = true;  
            isActive = true; 
            
            //ballRigidBody.velocity = new Vector2(MovementSpeed/2, MovementSpeed);
            
            inDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        }

        void LevelStopEvent() 
        {
            isActive = false;
        }

        // Start is called before the first frame update
        void Start()
        {
            moveActive = false;  
            isActive = false; 
            
            ballRigidBody = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            if ( moveActive && isActive )
            {
            }
        }

        void FixedUpdate() 
        {
            transform.Translate(inDirection * MovementSpeed/10, Space.World);
        }

        public void StartMovement() 
        {
            moveActive = true;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
           //EventManager.DebugLog("OnTriggerEnter2D()", other.transform.name);
        }

        void OnCollisionEnter2D( Collision2D collision)
        {
            //Vector3 newDirection =  Vector3.Reflect(transform.up,  collision.contacts[0].normal);  
            
            var contactPoint = collision.contacts[0].point;
            Vector2 ballLocation = transform.position;
            var inNormal = (ballLocation - contactPoint).normalized;
            inDirection = Vector2.Reflect(inDirection, inNormal);

            if ( collision.transform.tag == "Brick")
            {
                Destroy(collision.gameObject);
            }  
        }
    }
}
