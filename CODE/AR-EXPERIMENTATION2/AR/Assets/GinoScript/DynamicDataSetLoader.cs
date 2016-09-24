
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Vuforia;

///This class is used to handle the dataset activation at runtime. Attach the script to an empty gameobject with THE SAME NAME
///OF THE TARGET IN THE VUFORIA DATASET. boolRotationProblem and rotationProblem are used to clean stop the tracking when
///this is very unstable; so, rotationProblem is the best tracking position and deltaRotation[X-Y-Z] are the max delta.
/// <see cref="NoStaticAmbientExtendedTrackingManager"/>  is added dynamically to handle extended tracking in a no static ambient.
/// Note: it is important assign to every empty target to track the script ObjectTargetBehaviour...for my experience if the height of the
/// target is 1 the augmentation is always show.
public class DynamicDataSetLoader : MonoBehaviour
{	
	//my prefix for dynamic target generated 
	private static string DYNAMICOBJECTTARGET ="DynamicObjectTarget-";

	private static List<MyTrackableEventHandler> targetPositionList = new List<MyTrackableEventHandler> ();

	// specify these in Unity Inspector
	public string dataSetName = "";  //  Assets/StreamingAssets/QCAR/DataSetName

	private ObjectTracker objectTracker;

	private DataSet dataSet;
	
	public Transform rotationProblem = null;

	private Magnet magnet;

	private ResultManager resultManager;

	private MyTrackableEventHandler myTrackableEventHandler;

	public float deltaRotationX;
	public float deltaRotationY;
	public float deltaRotationZ;

	public bool boolRotationProblem = false;

	public bool boolExtendedTrackingNoStaticAmbient = false;

	//to load the dataset i use a flag because objectTracker sometimes
	//return null. So I force to load all the dataset
	private bool flagActivate;

	private bool flagStopTracker;

	private MyUI ui;

	// Use this for initialization
	void Start ()
	{
		magnet = (Magnet)GameObject.FindWithTag ("Magnet").GetComponent<Magnet> ();

		resultManager = (ResultManager)GameObject.FindWithTag ("ResultManager").GetComponent<ResultManager> ();

		resultManager.targetPosition = targetPositionList;

		ui = (MyUI)GameObject.FindWithTag ("UserInterface").GetComponent<MyUI> ();
					
	}


	void Update(){

		if (flagActivate) {
			
			objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker> ();
			
			if (objectTracker==null) {
				return;
			} else 
				flagActivate = false;

			dataSet = objectTracker.CreateDataSet ();
			
			if (dataSet.Load (dataSetName)) {
				
				objectTracker.Stop ();  // stop tracker so that we can add new dataset
				
				if (!objectTracker.ActivateDataSet (dataSet)) {
					// Note: ImageTracker cannot have more than 100 total targets activated
					Debug.Log ("<color=yellow>Failed to Activate DataSet: " + dataSetName + "</color>");
				}
				
				if (!objectTracker.Start ()) {
					Debug.Log ("<color=yellow>Tracker Failed to Start.</color>");
				}
				
				int counter = 0;
				
				IEnumerable<TrackableBehaviour> tbs = TrackerManager.Instance.GetStateManager ().GetTrackableBehaviours ();
				
				foreach (TrackableBehaviour tb in tbs) {
					
					//the name of the game object must be the same of the target in the database
					if (this.name == tb.TrackableName) {
						
						// change generic name to include trackable name
						tb.gameObject.name = DYNAMICOBJECTTARGET + tb.TrackableName;
						
						// add additional script components for trackable
						myTrackableEventHandler = tb.gameObject.AddComponent<MyTrackableEventHandler> ();
						
						targetPositionList.Add (myTrackableEventHandler);
						
						tb.gameObject.AddComponent<TurnOffBehaviour> ();

						if(boolExtendedTrackingNoStaticAmbient)
							tb.gameObject.AddComponent<NoStaticAmbientExtendedTrackingManager> ();
						
						if (boolRotationProblem) {
							
							LookComponent lookComponent = tb.gameObject.AddComponent<LookComponent> ();
							
							lookComponent.rotationX = rotationProblem.localRotation.x;
							lookComponent.rotationY = rotationProblem.localRotation.y;
							lookComponent.rotationZ = rotationProblem.localRotation.z;
							
							lookComponent.deltaRotationX = deltaRotationX;
							lookComponent.deltaRotationY = deltaRotationY;
							lookComponent.deltaRotationZ = deltaRotationZ;	
						}
						
						break;		
						
					}


				}
			} else {
				Debug.LogError ("<color=yellow>Failed to load dataset: '" + dataSetName + "'</color>");
			}

			if(flagStopTracker)
				stopTracker ();

		}

	}

	public void LoadDataSet(bool stopTracker){
		flagActivate = true;
		flagStopTracker = stopTracker;
	}

	public void stopTracker ()
	{	
		objectTracker.DeactivateDataSet (dataSet);
		//myTrackableEventHandler.stopTracker ();
	}

	public void startTracker ()
	{	
		objectTracker.ActivateDataSet (dataSet);
		//myTrackableEventHandler.startTracker ();
	}



}