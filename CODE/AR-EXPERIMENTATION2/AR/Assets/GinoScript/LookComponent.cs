using UnityEngine;
using System.Collections;
using Vuforia;
/// 
/// Look component is used to stop the tracking when
///this is very unstable; so, rotationProblem is the best tracking position and deltaRotation[X-Y-Z] are the max delta.
///
public class LookComponent : MonoBehaviour
{
	
	public float rotationX;
	public float rotationY;
	public float rotationZ;
	public float deltaRotationX;
	public float deltaRotationY;
	public float deltaRotationZ;

	private MyTrackableEventHandler myTrackableEventHandler;
	// Use this for initialization
	void Start ()
	{
		myTrackableEventHandler = new MyTrackableEventHandler();
	}


	// Update is called once per frame
	void Update ()
	{	
		if (myTrackableEventHandler.isActive()) {
			if (distance (rotationX, transform.localEulerAngles.x) > deltaRotationX || distance (rotationY, transform.localEulerAngles.y) > deltaRotationY || distance (rotationZ, transform.localEulerAngles.z) > deltaRotationZ) {
									
				myTrackableEventHandler.cleanTracker();

			}
			
		}
	}

	private float distance (float x, float y)
	{
			
		return Mathf.Abs (x - y);

	}
}
