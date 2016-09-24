using UnityEngine;
using System.Collections;
using Vuforia;
using System.IO;
using System;
using System.Collections.Generic;

/// <summary>
/// Result manager. This class is used for the result evaluation. See variable in csv file
/// </summary>
public class ResultManager : MonoBehaviour {

	private static string RESULT = "/result";

	public List<MyTrackableEventHandler> targetPosition;


	private string FILEHEADER = "Time;Context;TrackedObject;AxisPosition;AxisRotation;StereoOrMono;UserAcceleration"+"\n";
	private Magnet magnet;
	private StereoToMono stereoToMono;
	private ServerARManager serverARManager;

	private string fileName;

	private bool flagStopWrite;

	//variable in csv file
	private string time;
	private string context;
	private string trackedObject;
	private string axisPosition;
	private string axixRotation;
	private string stereoOrMono;
	private string userAcceleration;
	//variable in csv file

	private StreamWriter fileWriter;

	private int nextCounter, currentCounter;
	private DateTime previousDateTime;


	// Use this for initialization
	void Start () {

		magnet = (Magnet) GameObject.FindWithTag ("Magnet").GetComponent<Magnet> ();
		stereoToMono = (StereoToMono) GameObject.FindWithTag ("StereoToMonoManager").GetComponent<StereoToMono> ();
		serverARManager = (ServerARManager)GameObject.FindWithTag ("ServerARManager").GetComponent<ServerARManager> ();
		fileName = Application.persistentDataPath + RESULT + System.DateTime.UtcNow.ToString ("MMMM-dd-HH-mm")+".csv";
		fileWriter = File.CreateText(fileName); 
		fileWriter.WriteLine(FILEHEADER);

		currentCounter = nextCounter = magnet.GetCount ();
		previousDateTime = DateTime.Now;
	}

	// Update is called once per frame
	void Update () {

		if (!flagStopWrite && serverARManager.isActive()) {

			nextCounter = magnet.GetCount ();

			if (nextCounter != currentCounter) {
				double seconds = (DateTime.Now - previousDateTime).TotalSeconds;
				time = "" + seconds;

				previousDateTime = DateTime.Now;
				currentCounter = nextCounter;
			} else
				time = "";

			context = ""+magnet.GetCount ();

			trackedObject="//";
			axisPosition = "//";
			axixRotation= "//";

			foreach (MyTrackableEventHandler tp in targetPosition) {

				if(tp.isActive()){
					trackedObject = tp.getContextName();

					axisPosition = tp.transform.localPosition.ToString();
					axixRotation = tp.transform.localEulerAngles.ToString();

					break;
				}

			}

			stereoOrMono=stereoToMono.stereoOrMono ();

			userAcceleration = Input.acceleration.ToString ();

			fileWriter.WriteLine(time+";"+context+";"+trackedObject+";"+axisPosition+";"+axixRotation+";"+stereoOrMono+";"+userAcceleration+";");

		}

	}

	public void closeFile(){
		double seconds = (DateTime.Now - previousDateTime).TotalSeconds;
		fileWriter.WriteLine(seconds+";"+context+";"+trackedObject+";"+axisPosition+";"+axixRotation+";"+stereoOrMono+";"+userAcceleration+";");
		fileWriter.Close();
		flagStopWrite = true;
	}


}
