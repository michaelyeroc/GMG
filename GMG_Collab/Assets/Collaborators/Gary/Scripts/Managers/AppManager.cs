
#region File Description
//-----------------------------------------------------------------------------
// AppManager.cs
//
// Copyright (C) Allegro Interactive. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region using
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

using System;
#endregion

namespace Garys_Work
{

	public class AppManager : MonoBehaviour {
		
		#region enums
		#endregion
		
		#region fields
		bool isActive;

		#endregion
		
		#region properties
		public GameObject activeEventManager;
		public GameObject activeAudioManager;
		public GameObject activeScoreManager;
		#endregion
		
		#region events
		void OnEnable() 
		{
			// make event subscriptions
			EventManager.OnLevelStart += LevelStartEvent;
			EventManager.OnLevelStop +=LevelStopEvent;
		}

		void OnDisable()
		{
			// remove event subscriptions
			EventManager.OnLevelStart -= LevelStartEvent;
			EventManager.OnLevelStop -=LevelStopEvent;
		}

		void LevelStartEvent() 
		{
			isActive = true;
		}

		void LevelStopEvent() 
		{
			isActive = false;
		}

		void SceneLoadCompleteEvent() 
		{
			isActive = true;
		}
		#endregion
		
		#region Initialize
		//The Start function is called after all Awake functions on all script instances have been called. 
		void Start() 
		{
			isActive = false;
			//activeAudioManager.GetComponent<AudioManager>().MakeUserVolumeAdj(GameControl.GetUserVolumeAdj());
		}
		
		// Use Awake to set up references between scripts, and use Start to pass any information back and forth.
		void Awake() 
		{
		}
		#endregion


		#region methods
		// Update is called once per frame
		void Update()
		{
			
		}

		//DEVNOTE: started from OnClick() event
		public void StartGame() 
		{	
			activeEventManager.GetComponent<EventManager>().SignalLevelStart();

			EventManager.DebugLog("Start()", "unable to find 'EventManager' reporting object: " + transform.name);

		}
		#endregion
	}
}
