/*==============================================================================
Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Qualcomm Connected Experiences, Inc.
==============================================================================*/
using UnityEngine;
using System.Collections.Generic;

namespace Vuforia
{
	/// <summary>
	/// A custom handler that implements the ITrackableEventHandler interface.
	/// </summary>
	public class MyTrackableEventHandler : MonoBehaviour,
	ITrackableEventHandler
	{
		
		#region PRIVATE_MEMBER_VARIABLES

		/**** 
		 *    this are the prefix to handle the augmentation content and the position augmentation.
		 *    Remember: the Augmentation is one for all the different target position in the context. 
		 *    The Position is one for every target position
		****/
		private static string AUGMENTATION = "Augmentation";
		private static string POSITION = "Position";
		private static string HAND = "Hand";

		private static string STARTWITH = "SchedaMadre";

		private static string TRACKINGSIDE = "Tracking side:\n";

		/*****/

		//the augmentation contect are attached to a targer only. 
		private static bool[] isLookGlobal;
		private bool isLookLocal;
		private TrackableBehaviour mTrackableBehaviour;
		private MyUI ui;
		private Magnet magnet;
		private int nextCounter, currentCounter;
		private GameObject currentAugmentationObject;
		private Transform currentAugmentationObjectParent;
		private Transform currentAugmentationPosition;

		private string contextName;
		private string contextNameToShow;

		private static bool flagRenderHand,flagRender;

		#endregion // PRIVATE_MEMBER_VARIABLES
		
		
		#region UNTIY_MONOBEHAVIOUR_METHODS

		public string getContextName(){
			return contextNameToShow;
		}

		void Start ()
		{
			
			mTrackableBehaviour = GetComponent<TrackableBehaviour> ();
			if (mTrackableBehaviour) {
				mTrackableBehaviour.RegisterTrackableEventHandler (this);
			}

			contextName = mTrackableBehaviour.name.Split ('-') [1];

			contextNameToShow = contextName.Replace (STARTWITH, "");

			magnet = (Magnet)GameObject.FindWithTag ("Magnet").GetComponent<Magnet> ();
			
			currentCounter = nextCounter = magnet.GetCount ();
			
			ui = (MyUI)GameObject.FindWithTag ("UserInterface").GetComponent<MyUI> ();

			isLookGlobal = new bool[ui.LABEL.Length];

			isLookLocal = new bool ();
		

		}
		
		void Update ()
		{
			
			nextCounter = magnet.GetCount ();
			
			if (nextCounter != currentCounter) {

				forceOnTrackingLost (GameObject.FindWithTag (AUGMENTATION + currentCounter));

				isLookGlobal [currentCounter] = false;

				cleanTracker ();

				currentCounter = nextCounter;
				flagRenderHand = flagRender = false;

			}


		}

		#endregion // UNTIY_MONOBEHAVIOUR_METHODS
		
		
		
		#region PUBLIC_METHODS
	

		public  void cleanTracker ()
		{
			mTrackableBehaviour.UnregisterTrackableEventHandler (this);
			mTrackableBehaviour.RegisterTrackableEventHandler (this);
		}

		public void stopTracker ()
		{
		
			mTrackableBehaviour.UnregisterTrackableEventHandler (this);
		}

		public void startTracker ()
		{
			mTrackableBehaviour.RegisterTrackableEventHandler (this);
		}

		///I reset the other targets so that they can be tracked
		public void resetOtherTarget ()
		{
			//InvalidOperationException: Collection was modified; enumeration operation may not execute - to adjust
			MyTrackableEventHandler  [] allMyTrackableEventHandler = FindObjectsOfType (typeof(MyTrackableEventHandler)) as MyTrackableEventHandler[];

			List<MyTrackableEventHandler> allMyTrackableEventHandlerList = new List<MyTrackableEventHandler> (allMyTrackableEventHandler);

			foreach (MyTrackableEventHandler myTrackableEventHandler in allMyTrackableEventHandlerList) 
				if (myTrackableEventHandler != this)
					myTrackableEventHandler.cleanTracker ();

		}
		
		/// <summary>
		/// Implementation of the ITrackableEventHandler function called when the
		/// tracking state changes.
		/// </summary>
		public void OnTrackableStateChanged (
			TrackableBehaviour.Status previousStatus,
			TrackableBehaviour.Status newStatus)
		{	

			if (newStatus == TrackableBehaviour.Status.DETECTED ||
				newStatus == TrackableBehaviour.Status.TRACKED ||
				newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED) {    

				//if the target is recognized other target can not acquire the augmentation content
				if (!isLookGlobal [magnet.GetCount ()]) {


					isLookGlobal [magnet.GetCount ()] = true;
					isLookLocal = true;

					cleanTarget ();

					assignAsParentWithContextPosition ();

					OnTrackingFound ();

					ui.showTrackingSideOrOtherInformation (TRACKINGSIDE+contextNameToShow);

					ui.showUI (this.name);
				}
				
			} else {
				if (isLookLocal) {
					
					isLookGlobal [magnet.GetCount ()] = false;
					isLookLocal = false;

					OnTrackingLost ();

					cleanTarget ();

					ui.showTrackingSideOrOtherInformation ();

					ui.hideUI ();

					resetOtherTarget ();
				}
				
			}
		}

		//the augmentation content are no longer children of the target
		private void cleanTarget ()
		{
			if (currentAugmentationObject != null)
				currentAugmentationObject.transform.parent = currentAugmentationObjectParent.transform;
				
		}

		//return the augmentation objects for the context
		private GameObject returnAugmentationObject ()
		{
		
			GameObject augmentationObject = GameObject.FindWithTag (AUGMENTATION + magnet.GetCount ());

			//the first father of the augmentation
			currentAugmentationObjectParent = augmentationObject.transform.parent;

			return augmentationObject;

		}

		//return the augmentation objects position for the context. Remember: every side have a particular position in the scene
		private Transform returnAugmentationPosition ()
		{
			Transform [] contextPositions = (GameObject.FindWithTag (POSITION + magnet.GetCount ())).GetComponentsInChildren<Transform> ();

			foreach (Transform contextPosition in contextPositions) {
				
				if (contextPosition.name.Equals (POSITION + contextName)) {
					return contextPosition;
				}
			}

			return null;


		}
		//the augmentation content are now children of the trached targer
		private void assignAsParentWithContextPosition ()
		{

			currentAugmentationObject = returnAugmentationObject ();

			currentAugmentationPosition = returnAugmentationPosition ();

			if (currentAugmentationObject != null && currentAugmentationPosition != null) {
				
				//the father of the augmentation is now the tracked object
				currentAugmentationObject.transform.parent = this.gameObject.transform;
				
				//root and the children may have a position, rotation and scale depending the side tracked. If the
				//position is not specified is assigned the default position (specified in AUGMENTATION)
				
				currentAugmentationObject.transform.localPosition = currentAugmentationPosition.transform.localPosition;
				currentAugmentationObject.transform.localRotation = currentAugmentationPosition.transform.localRotation;
				currentAugmentationObject.transform.localScale = currentAugmentationPosition.transform.localScale;
				
				
				Transform[] objectComponents = currentAugmentationObject.GetComponentsInChildren<Transform> (true);
				
				Transform[] positionComponents = currentAugmentationPosition.GetComponentsInChildren<Transform> (true);
				
				foreach (Transform component in objectComponents) {
					
					foreach (Transform component2 in positionComponents) {
						
						if (component.name.Equals (component2.name)) {
							component.transform.localPosition = component2.transform.localPosition;
							component.transform.localRotation = component2.transform.localRotation;
							component.transform.localScale = component2.transform.localScale;
						}
					}
					
					
					
				}
			}



		}

		public void OnTrackingFound ()
		{

			Renderer[] rendererComponents = GetComponentsInChildren<Renderer> (true);

			// Enable rendering:
			foreach (Renderer component in rendererComponents) {

				if(flagRenderHand){
					if(component.name.Contains(HAND))
						component.enabled = false;
					else
						component.enabled = true;
				} else if(flagRender){
					component.enabled = false;
				} else
					component.enabled = true;
				
			}
			
			Debug.Log ("Trackable " + mTrackableBehaviour.TrackableName + " found");
		}
		
		public void OnTrackingLost ()
		{
			Renderer[] rendererComponents = GetComponentsInChildren<Renderer> (true);

			// Disable rendering:
			foreach (Renderer component in rendererComponents) {
				component.enabled = false;
			}
			
			Debug.Log ("Trackable " + mTrackableBehaviour.TrackableName + " lost");
		}

		public bool isActive ()
		{
			return isLookLocal;
		}

		public bool isGlobalActive ()
		{
			return isLookGlobal [magnet.GetCount ()];
		}

		/// <summary>
		/// Force the tracking lost...somotimes the render is active after OnTrackingLost
		/// </summary>
		public void forceOnTrackingLost (GameObject parentObject)
		{
			Renderer[] rendererComponents = parentObject.GetComponentsInChildren<Renderer> (true);

			// Disable rendering:
			foreach (Renderer component in rendererComponents) {
				component.enabled = false;
			}
			
			Debug.Log ("Trackable " + mTrackableBehaviour.TrackableName + " lost");
		}

		public void setFlagRenderHand(bool flag){
			flagRenderHand = flag;
		}
		
		public void setFlagRender(bool flag){
			flagRender = flag;
		}

		public bool getFlagRenderHand(){
			return flagRenderHand;
		}

		public bool getFlagRender(){
			return flagRender;
		}

		#endregion // PUBLIC_METHODS

	
	}
	
	
	
	
	
	
}