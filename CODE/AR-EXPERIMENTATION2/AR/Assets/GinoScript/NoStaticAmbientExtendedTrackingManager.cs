using Vuforia;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Vuforia;

/// 
/// Extended TrackingManager is used to stop the extended tracking when the tracked object is moved
/// Infact the extended tracking no work well with a no static scene. If the object is moved, delta from previouRotation[X-Z] to rotation[X-Z] is > deltaRotation[X-Z]) after 
///GOFRAME, I disactivate extended tracking with a penality of FRAMEPENALITY frame. Anyay I disactive extended tracking every deltaTime seconds.
public class NoStaticAmbientExtendedTrackingManager : MonoBehaviour
{
	private static int GOFRAME = 10;
	
	private static int FRAMEPENALITY = -300;

	private static float DELTAROTATIONX = 5f;
	private static float DELTAROTATIONY = 5f;
	private static float DELTAROTATIONZ = 5f;
	private static int DELTATIME = 15;

	private static bool isActiveBool;
	
	private float rotationX;
	private float rotationY;
	private float rotationZ;
	private float previouRotationX;
	private float previouRotationY;
	private float previouRotationZ;

	private int frameCount = 0;
	private int currentSecond;
	
	private MyTrackableEventHandler myTrackableEventHandler;
	private TrackableSettings trackableSettings;
	private MyUI ui;
	
	private bool flag;

	// Use this for initialization
	void Start ()
	{
		myTrackableEventHandler = GetComponent<MyTrackableEventHandler> ();
		trackableSettings = GameObject.FindWithTag ("SampleUI").GetComponent<TrackableSettings> ();

		ui = (MyUI)GameObject.FindWithTag ("UserInterface").GetComponent<MyUI> ();

		rotationX = transform.localEulerAngles.x;
		rotationY = transform.localEulerAngles.y;
		rotationZ = transform.localEulerAngles.z;
	}
	
	void Update ()
	{

		if (myTrackableEventHandler.isActive() && isActiveBool) {

			if (trackableSettings.IsExtendedTrackingEnabled () == false) {
				Debug.Log ("No extended tracking");
				ui.showTrackingSideOrOtherInformation ("No extended tracking");

			} else {
				ui.showTrackingSideOrOtherInformation ("Yes extended tracking");
				Debug.Log ("Yes extended tracking");
			}

			frameCount++;

			if (frameCount != GOFRAME) {
					
				previouRotationX = rotationX;
				previouRotationY = rotationY;
				previouRotationZ = rotationZ;

				rotationX = transform.localEulerAngles.x;
				rotationY = transform.localEulerAngles.y;
				rotationZ = transform.localEulerAngles.z;

			} else {
				
				if (trackableSettings.IsExtendedTrackingEnabled () == false) {
					trackableSettings.SwitchExtendedTracking (true);
					currentSecond = System.DateTime.UtcNow.Second;
				}
				
				
				if ((distance (rotationX, previouRotationX) > DELTAROTATIONX || 
					distance (rotationY, previouRotationY) > DELTAROTATIONY || 
					distance (rotationZ, previouRotationZ) > DELTAROTATIONZ) || 
					Mathf.Abs (currentSecond - System.DateTime.UtcNow.Second) > DELTATIME) {
					
					trackableSettings.SwitchExtendedTracking (false);

					myTrackableEventHandler.cleanTracker ();
					flag = true;

				}
					
				if (flag) {
					flag = false;
					frameCount = FRAMEPENALITY;
				} else
					frameCount = 0;
				
			}

		}

	}
	public float distance (float x, float y)
	{
		if(Mathf.Abs (x - y)>DELTAROTATIONX)
			Debug.Log ("" + Mathf.Abs (x - y));
		return Mathf.Abs (x - y);

	}

	public void activate(bool yesOrNo){
		if (isActiveBool = yesOrNo)
			ui.showNoStaticAmbientExtendedTrackingOn ();
		else
			ui.showNoStaticAmbientExtendedTrackingOff ();


	}

	public bool isActive(){
		return isActiveBool;
	}
}