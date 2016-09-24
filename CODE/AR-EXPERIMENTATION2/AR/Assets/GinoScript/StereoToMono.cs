using UnityEngine;
using System.Collections;
using Vuforia;

/// Stereo to mono. This class is used to switch between stereo and mono
public class StereoToMono : MonoBehaviour
{

	private static string MONO = "mono";
	private static string STEREO = "stereo";
	public GameObject CanvasButton;
	public GameObject StereoDivisionCanvas;
	public GameObject Reticle;

	private bool mono;

	private MyUI ui;
	
	/// <summary>
	/// Three differenty modality are available
	/// 1: MODE_DEFAULT
	/// 2: MODE_OPTIMIZE_QUALITY
	/// 3: MODE_OPTIMIZE_SPEED
	/// </summary>
	public int cameraMode = 1;

	// Use this for initialization
	void Start ()
	{
		ui = (MyUI)GameObject.FindWithTag ("UserInterface").GetComponent<MyUI> ();

		//CanvasButton.SetActive (false);
	}

	public void switchVisualization (bool switching)
	{
		Handheld.Vibrate ();

		if (switching) {
			monoMode ();
		} else {
			stereoMode ();
		}


	}

	void monoMode ()
	{

		CanvasButton.SetActive (true);

		StereoDivisionCanvas.SetActive (false);
		Reticle.SetActive (false);

		MixedRealityController.Instance.SetMode(MixedRealityController.Mode.HANDHELD_AR);

		mono = true;
	}

	void stereoMode ()
	{

		CanvasButton.SetActive (false);

		StereoDivisionCanvas.SetActive (true);
		Reticle.SetActive (true);

		MixedRealityController.Instance.SetMode(MixedRealityController.Mode.VIEWER_AR);


		mono = false;
	}
		
	/// <summary>
	/// Stereos the or mono. Used for result analysis
	/// </summary>
	/// <returns>The or mono.</returns>
	public string stereoOrMono ()
	{
		if (mono)
			return MONO;
		else 
			return STEREO;

	}

	public bool isMono ()
	{
		if (mono)
			return true;
		else
			return false;
	}

}
