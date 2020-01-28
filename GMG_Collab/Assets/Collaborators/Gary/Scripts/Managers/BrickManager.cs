#region File Description
//-----------------------------------------------------------------------------
// BrickManager.cs
//
// Copyright (C) Allegro Interactive. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Garys_Work
{
    public class BrickManager : MonoBehaviour
    {	
        
    #region fields
        bool isActive = true;
       
    #endregion
        
    #region properties
        public GameObject brickPrefab;
        public GameObject brickParent;
        public Vector3 brickFieldAnchor;
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
            isActive = true;
        }

        void LevelStopEvent() 
        {
            isActive = false;
        }

        // Start is called before the first frame update
        void Start()
        {
            isActive = false; 
            CreateBrickField(brickParent, brickPrefab, brickFieldAnchor);
        }

        // Update is called once per frame
        void Update()
        {
            if ( isActive )
            {
            }

        }

        void FixedUpdate() 
        {
        }

        void CreateBrickField(GameObject parent, GameObject prefab, Vector3 firstBrickPos)
        {
            GameObject newEnemy;
            Vector3 nextPos;
            float brickWidth = 0.8f;
            float brickHeight = -0.2f;
            
            int rowCnt = 3;
            int colCnt = 5;
            for( int j = 0; j <= rowCnt; j++)
            {
                for( int i = 0; i <= colCnt; i++)
                {
                    nextPos = new Vector3(brickWidth * i, brickHeight * j, 0)  + firstBrickPos;
                    newEnemy = Instantiate(prefab, nextPos, Quaternion.identity);
                    //newEnemy.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0.0f, 359.0f));
                    newEnemy.transform.SetParent(parent.transform);
                }
            }
           
          
        }

    }
}
