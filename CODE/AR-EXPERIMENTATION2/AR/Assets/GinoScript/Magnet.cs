using UnityEngine;
using System.Collections;
using System;
using Vuforia;

///This class is used to increment counter in the scene. It is called Magnet because it born only for Cardboard.
///Now it is usable with smartphone and PC (and mac) for debug.
public class Magnet : MonoBehaviour {




	private static string SCENE = "Scena: ";
	private static int counter = 1;

	private static int SHORTTIME = 0;
	private static int LONGTIME = 200;

	private MyUI ui;

	private ResultManager resultManager;

	public bool useSceneOrderNoDirection = true;

	private bool useSceneOrderNoDirectionInizialized;


	private MyTrackableEventHandler myTrackableEventHandler;


	public int [] sceneOrder = new int[]{};

	public int [] sceneOrderNoDirection = new int[]{};


	//scene's duration in second. The scene "Opera nella posizione indicata dalla freccia" should be brief
	//note that the elementin the array below are associated at the sceneOrder's element
	public int [] durationScene = new int[]{};

	private DateTime previousDateTime;

	private bool flagIsCallNextScene = true;

	// Use this for initialization
	void Start () {
		
		ui = (MyUI) GameObject.FindWithTag ("UserInterface").GetComponent<MyUI> ();
		resultManager = (ResultManager)GameObject.FindWithTag ("ResultManager").GetComponent<ResultManager> ();

		if (useSceneOrderNoDirection)
			sceneOrder = sceneOrderNoDirection;
		
		previousDateTime = DateTime.Now;

	}
	
	void Update () {

		if (myTrackableEventHandler == null) {
			myTrackableEventHandler = FindObjectOfType <MyTrackableEventHandler>();
		} else if (!useSceneOrderNoDirection && flagIsCallNextScene){
			if (myTrackableEventHandler.isGlobalActive ()) {
				if ((DateTime.Now - previousDateTime).TotalSeconds >= durationScene [sceneOrder [counter]]) {
					nextScene ();
					flagIsCallNextScene = false;
				}
			}
				
		}

	}

	/// <summary>
	/// Gets the count. The order scene is define is sceneOrder array
	/// </summary>
	/// <returns>The count.</returns>
	public int GetCount(){
		
		//inizialization problem
		if (useSceneOrderNoDirection && !useSceneOrderNoDirectionInizialized) {
			sceneOrder = sceneOrderNoDirection;
			useSceneOrderNoDirectionInizialized = true;
		}


		return sceneOrder[counter];
	}

	public void nextScene(){
		
		if (counter == sceneOrder.Length-1) {
			resultManager.closeFile ();

			ui.StartCoroutine ("splash", "FINE");

			StartCoroutine ("LoadNextSceneAfter", 2);

			return;
		} else {

			if ((DateTime.Now - previousDateTime).TotalSeconds > SHORTTIME || !useSceneOrderNoDirection) {
				
				counter++;

				ui.showNoTrackingMessage ();

				ui.StartCoroutine ("splash", SCENE + counter);

				previousDateTime = DateTime.Now;
				flagIsCallNextScene = true;
			}

		}
	}

	#region PRIVATE_METHODS
	private IEnumerator LoadNextSceneAfter(float seconds)
	{
		yield return new WaitForSeconds(seconds);

		#if (UNITY_5_2 || UNITY_5_1 || UNITY_5_0)
		Application.LoadLevel(Application.loadedLevel+1);
		#else
		UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex+1);
		#endif
	}
	#endregion //PRIVATE_METHODS

}
