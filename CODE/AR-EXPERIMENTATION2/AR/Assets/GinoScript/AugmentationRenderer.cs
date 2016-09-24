using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Vuforia;

public class AugmentationRenderer : MonoBehaviour {

	private static string SIMANI = "Mani visibili";
	private static string NOMANI = "Mani non visibili";

	private static string SIAUMENTAZIONE = "Aumentazione visibile";
	private static string NOAUMENTAZIONE = "Aumentazione non visibile";

	private static string AUGMENTATION = "Augmentation";
	private static string HAND = "Hand";

	private Magnet magnet;

	public Texture2D hands;
	public Texture2D noHands;
	public GameObject rawImageObject;

	private RawImage rawImage; 

	private int nextCounter , currentCounter;

	private MyTrackableEventHandler myTrackableEventHandler;
	private MyUI ui;

	
	void Start(){
		rawImage = rawImageObject.GetComponent<RawImage> ();
		magnet = (Magnet)GameObject.FindWithTag ("Magnet").GetComponent<Magnet> ();
		ui = (MyUI)GameObject.FindWithTag ("UserInterface").GetComponent<MyUI> ();

		nextCounter = currentCounter = magnet.GetCount ();
	}

	void Update ()
	{

		if (myTrackableEventHandler == null) {
			myTrackableEventHandler = FindObjectOfType <MyTrackableEventHandler>();
		} else {

				nextCounter = magnet.GetCount ();
			
				if (nextCounter != currentCounter) {
					
					rawImage.texture = noHands;
					

					currentCounter = nextCounter;
				}

			}


		}

	/// <summary>
	/// Renders the off or onf hands. callPlace is the object (normalUI, virtualButton and smartphoneClient) the call the method
	/// </summary>
	/// <param name="callPlace">Call place.</param>
	public void handlerRenderOffOrOnfHands(int callPlace) {

		if (!myTrackableEventHandler.getFlagRenderHand ()) {
				RenderOffOrOnTheHands (false);
				myTrackableEventHandler.setFlagRenderHand (true);

				rawImage.texture = hands;
				ui.StartCoroutine ("splash", NOMANI);

		} else {

			myTrackableEventHandler.setFlagRenderHand (false);
			rawImage.texture = noHands;
			ui.StartCoroutine ("splash", SIMANI);

			if (myTrackableEventHandler.isGlobalActive ())
				RenderOffOrOnTheHands (true);
		

		}
	
	}

	/// <summary>
	/// Renders the off or on all. callPlace is the object (normalUI, virtualButton and smartphoneClient) the call the method
	/// </summary>
	/// <param name="callPlace">Call place.</param>
	public void handlerRenderOffOrOnAll(int callPlace) {

		if (!myTrackableEventHandler.getFlagRender ()) {
				RenderOffOrOnAll (false);
				myTrackableEventHandler.setFlagRender (true);
				ui.StartCoroutine ("splash", NOAUMENTAZIONE);
		} else {
					
			myTrackableEventHandler.setFlagRender(false);
			ui.StartCoroutine ("splash", SIAUMENTAZIONE);

			if (myTrackableEventHandler.isGlobalActive ())
				RenderOffOrOnAll (true);
			
				

		}

	}

	//return the augmentation objects for the context
	private bool RenderOffOrOnTheHands (bool flag)
	{

		bool thereAreModified = false;

		GameObject augmentationObject = GameObject.FindWithTag (AUGMENTATION + magnet.GetCount ());
		
		Renderer[] rendererComponents = augmentationObject.GetComponentsInChildren<Renderer> (true);

		Renderer[] childRendererComponents;


		foreach (Renderer component in rendererComponents) {

			if(component.name.Contains(HAND)){

				thereAreModified = true;

				component.enabled = flag;

			}

		}

		return thereAreModified;

	}

		//return the augmentation objects for the context
	private void RenderOffOrOnAll (bool flag)
	{

		GameObject augmentationObject = GameObject.FindWithTag (AUGMENTATION + magnet.GetCount ());

		Renderer[] rendererComponents = augmentationObject.GetComponentsInChildren<Renderer> (true);

		Renderer[] childRendererComponents;

		foreach (Renderer component in rendererComponents) {
				component.enabled = flag;
		}

	}


}
