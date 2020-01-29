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
        public GameObject BrickPrefab;
        public GameObject BrickParent;
        public Vector3 BrickFieldAnchor;

        public Color[] RowColor; //= {Color.yellow, Color.red, Color.magenta, Color.blue, Color.green};
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
            CreateBrickField(BrickParent, BrickPrefab, BrickFieldAnchor);
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
            GameObject newBrick;
            Vector3 nextPos;
            float brickWidth = 0.8f;
            float brickHeight = -0.2f;
            
            int rowCnt = 4;
            int colCnt = 5;
            int brickId = 1;

            for( int j = 0; j <= rowCnt; j++)
            {
                for( int i = 0; i <= colCnt; i++)
                {
                    nextPos = new Vector3(brickWidth * i, brickHeight * j, 0)  + firstBrickPos;
                    newBrick = Instantiate(prefab, nextPos, Quaternion.identity);
                    //newEnemy.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0.0f, 359.0f));
                    newBrick.transform.SetParent(parent.transform);
                    newBrick.GetComponent<BrickHandler>().BrickId = brickId++;
                    newBrick.transform.name = prefab.transform.name;
                    
                    newBrick. GetComponent<SpriteRenderer>().material.SetColor("_Color", new Color(
                                                                                            RowColor[j].r,
                                                                                            RowColor[j].g,
                                                                                            RowColor[j].b, 
                                                                                            1.0f));
                }
            }
           
          
        }

    }
}
