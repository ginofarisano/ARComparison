using UnityEngine;
using System.Collections;

///This class is used to handle the animation in the scene.
///Iteratively  every contextTargetArray's element is animated in a particular sceneNumber, in creaseOrdecrease
///direction, in a particular axis, and with a delta change. The flag after is used to stop the i-1 animation at the last frame.
///particularMessage is used to display a message (enphasize) a particular animation 
/// Note:
///for every public component define the same length
public class MyAnimationObject : MonoBehaviour
{
	public GameObject[] myObject;
	public GameObject[] particularPlace;
	public string[] contextTargetArray;
	public string[] axis;
	public float[] startPosition;
	public float[] endPosition;
	public float[] delta;
	public bool[] rotate;
	//the animation is locked - it no return at the beginning
	public bool[] after;
	private bool isTheFirstStart = true;
	private bool isTheFirstStop = true;
	public int[] creaseOrdecrease;
	public int sceneNumber;

	//use it for handle particular message in particular place
	public string[] particularMessage;
	private Magnet magnet;

	

	// Use this for initialization
	void Start ()
	{
		magnet = (Magnet)GameObject.FindWithTag ("Magnet").GetComponent<Magnet> ();
	}

	void Update ()
	{	

		foreach (string context in contextTargetArray) {
			if (magnet.GetCount () == sceneNumber && isTheFirstStart) {

				isTheFirstStart = false;
				StartCoroutine ("startAnimation");

			}
			
			if (magnet.GetCount () != sceneNumber) {
				StopCoroutine ("startAnimation");
				//deactive myself
				isTheFirstStart = true;
				enabled = false;
			}
		}

	}

	IEnumerator startAnimation ()
	{
		Vector3 [] position = new Vector3[myObject.Length];

		float [] currentPositionMyAxes = new float[myObject.Length];	

		int i = 0;

		bool flagBreak = false;

		foreach (GameObject gameObject in myObject) {

			if (!rotate [i]) {
				position [i] = gameObject.GetComponent<Transform> ().localPosition;
				
				currentPositionMyAxes [i] = startPosition [i];
				
				i++;

			} else {
				position [i] = gameObject.GetComponent<Transform> ().localEulerAngles;
				
				currentPositionMyAxes [i] = startPosition [i];
				
				i++;
			}

		}

		i = 0;
			
		while (true) {

			if (particularPlace.Length > 0) {
				if(particularPlace [i]!=null)
					particularPlace [i].GetComponent<TextMesh> ().text = particularMessage [i];

			}

			while (true) {
			
				currentPositionMyAxes [i] += delta [i] * (creaseOrdecrease [i]);

				if (creaseOrdecrease [i] == 1) {
					
					if (currentPositionMyAxes [i] > endPosition [i]) {
						
						currentPositionMyAxes [i] = startPosition [i];

						flagBreak = true;

					} 
						

				} else {
					
					if (currentPositionMyAxes [i] < endPosition [i]) {

						currentPositionMyAxes [i] = startPosition [i];

						flagBreak = true;
					}
					
				}
			
				if (axis [i] == "x" && !rotate [i]) {
					position [i] = new Vector3 (currentPositionMyAxes [i], myObject [i].transform.localPosition.y, myObject [i].transform.localPosition.z);
				} else if (axis [i] == "y" && !rotate [i]) {
					position [i] = new Vector3 (myObject [i].transform.localPosition.x, currentPositionMyAxes [i], myObject [i].transform.localPosition.z);

				} else if (axis [i] == "z" && !rotate [i]) {

					position [i] = new Vector3 (myObject [i].transform.localPosition.x, myObject [i].transform.localPosition.y, currentPositionMyAxes [i]);

				} else if (axis [i] == "x" && rotate [i]) {
					position [i] = new Vector3 (currentPositionMyAxes [i], myObject [i].transform.localEulerAngles.y, myObject [i].transform.localEulerAngles.z);

				} else if (axis [i] == "y" && rotate [i]) {
					position [i] = new Vector3 (myObject [i].transform.localEulerAngles.x, currentPositionMyAxes [i], myObject [i].transform.localEulerAngles.z);

				} else if (axis [i] == "z" && rotate [i]) {
					position [i] = new Vector3 (myObject [i].transform.localEulerAngles.x, myObject [i].transform.localEulerAngles.y, currentPositionMyAxes [i]);
				}

				if (!rotate [i] && !flagBreak) {
					myObject [i].transform.localPosition = position [i];
				} else if (rotate [i] && !flagBreak) {
					myObject [i].transform.localEulerAngles = position [i];

				}

				if (!rotate [i] && flagBreak && !after [i]) {
					myObject [i].transform.localPosition = position [i];
				} else if (rotate [i] && flagBreak && !after [i]) {
					myObject [i].transform.localEulerAngles = position [i];
					
				}

				if (flagBreak) {
					flagBreak = false;
					break;
				}

				yield return null;

			}


			if (++i == myObject.Length)
				i = 0;

		}




	}

}