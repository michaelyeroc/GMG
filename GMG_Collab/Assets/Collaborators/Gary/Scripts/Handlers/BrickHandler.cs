#region File Description
//-----------------------------------------------------------------------------
// BrickHandler.cs
//
// Copyright (C) Allegro Interactive. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Garys_Work
{
    public class BrickHandler : MonoBehaviour
    {	
        
    #region fields
        bool isActive = true;
       
    #endregion
        
    #region properties
        public int BrickId;
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

    }
}
