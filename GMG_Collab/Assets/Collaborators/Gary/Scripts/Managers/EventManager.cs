
//-----------------------------------------------------------------------------
// EventManager.cs
//
// Copyright (C) Allegro Interactive. All rights reserved.
//-----------------------------------------------------------------------------


using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Garys_Work
{
	
	public class EventManager : MonoBehaviour 
	{
		public bool debugActive = false;

		public class UpdateScoreInfoEventArgs : EventArgs
		{
			public int hitCount;

			public UpdateScoreInfoEventArgs( int h )
			{
				hitCount = h; 
			}
		}

		public static void DebugLog(string methodName, string debugMsg )
		{
			#if UNITY_EDITOR
				Debug.Log(string.Format("<color=green>{0}</color>, {1}    {2}",Time.time, methodName, debugMsg));
			#endif
		}
		
		// REF: https://unity3d.com/learn/tutorials/modules/intermediate/scripting/events
		// REF: https://unity3d.com/learn/tutorials/topics/scripting/delegates

		public delegate void UpdateScore(UpdateScoreInfoEventArgs e);
		public static event UpdateScore OnUpdateScore;
		
		public void SignalUpdateScore( int hitCount) 
		{

			UpdateScoreInfoEventArgs eventInfo = new UpdateScoreInfoEventArgs(hitCount);
			
			if (OnUpdateScore != null) 
			{
				if ( debugActive ) DebugLog(System.Reflection.MethodBase.GetCurrentMethod().Name, "");
				OnUpdateScore( eventInfo );
			}
			else 
			{
				if ( debugActive ) DebugLog(System.Reflection.MethodBase.GetCurrentMethod().Name, "<color=red>No Listeners:</color>");
			}
		}

		public delegate void LevelStart();
		public static event LevelStart OnLevelStart; 
		
		public void SignalLevelStart () 
		{
			if (OnLevelStart != null) 
			{
				if ( debugActive ) DebugLog(System.Reflection.MethodBase.GetCurrentMethod().Name, "");
				OnLevelStart();
			}
		}

		public delegate void LevelStop();
		public static event LevelStop OnLevelStop;
		
		public void SignalLevelStop () 
		{
			if (OnLevelStop != null) 
			{
				if ( debugActive ) DebugLog(System.Reflection.MethodBase.GetCurrentMethod().Name, "");
				OnLevelStop();
			}
			else 
			{
				if ( debugActive ) DebugLog(System.Reflection.MethodBase.GetCurrentMethod().Name, "<color=red>No Listeners:</color>");
			}
		}

		public delegate void InputFeedback();
		public static event InputFeedback OnInputFeedback;
		
		public void SignalInputFeedback () 
		{
			if (OnInputFeedback != null) 
			{
				if ( debugActive ) DebugLog(System.Reflection.MethodBase.GetCurrentMethod().Name, "");
				OnInputFeedback();
			}
			else 
			{
				if ( debugActive ) DebugLog(System.Reflection.MethodBase.GetCurrentMethod().Name, "<color=red>No Listeners:</color>");
			}
		}


		// Use this for initialization
		public void Init() 
		{
			// do things here that need to happen after the object is instantiated. 
			
		}
		
		//The Start function is called after all Awake functions on all script instances have been called. 
		void Start() 
		{
		}
		
		// Use Awake to set up references between scripts, and use Start to pass any information back and forth.
		void Awake() 
		{
		}
	}
}