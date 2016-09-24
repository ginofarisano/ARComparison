using UnityEngine;
using System.Collections;
using Vuforia;
using UnityEngine.UI;

///This class is used to handle UI message
///every context has a personal UI message
/// 
public class MyUI : MonoBehaviour
{

	/// <summary>
	/// Label for every no tracking scene
	/// </summary>
	public string[] NOTRACKINGMESSAGE = new string[] {};

	/// <summary>
	/// Label for every scene
	/// </summary>
	public string[] LABEL = new string[] {};
	private Magnet magnet;
	private Text uiLabel;
	private Text uiLabelNoTracking;
	private Text uiLabelTrackingSide;
	private Text uiLabelNoStaticAmbientExtendedTracking;
	private Text positionLabel;
	private Text uiLabelSplash;

	private GameObject rendererComponents;
	private GameObject rendererComponentsNoTracking;
	private GameObject rendererComponentsTrackingSideOrOtherInformation;
	private GameObject rendererComponentsTextSplash;

	//red image. This modality is danger
	UnityEngine.UI.Image panelNoStaticAmbientExtendedTracking;


	/*********inserte here personal message for no visible side**********/

	//private static int context8 = 8;
	//private static string contextSide8 = "DynamicObjectTarget-SchedaMadreInputComponentSide1";
	//private static string personalMessage8 = "Ruota la scheda madre fino a raggiungere la posizione indicata dalla freccia";
	
	/*********inserte here personal message for no visible side**********/

	void Start ()
	{

		magnet = (Magnet)GameObject.FindWithTag ("Magnet").GetComponent<Magnet> ();
			
		uiLabel = GameObject.FindWithTag ("Text").GetComponent<Text> ();
		uiLabelNoTracking = GameObject.FindWithTag ("TextNoTracking").GetComponent<Text> ();
		uiLabelTrackingSide = GameObject.FindWithTag ("TextTrackingSideOrOtherInformation").GetComponent<Text> ();
		uiLabelNoTracking.text = NOTRACKINGMESSAGE[magnet.GetCount()];

		uiLabelSplash = GameObject.FindWithTag ("TextSplash").GetComponent<Text> ();

		rendererComponents = GameObject.FindWithTag ("CanvasInstructions");
		rendererComponentsTrackingSideOrOtherInformation = GameObject.FindWithTag ("CanvasTrackingSideOrOtherInformation");
		rendererComponentsNoTracking = GameObject.FindWithTag ("CanvasNoTracking");

		rendererComponents.SetActive (false);
		rendererComponentsNoTracking.SetActive (true);
		rendererComponentsTrackingSideOrOtherInformation.SetActive (false);

		rendererComponentsTextSplash = GameObject.FindWithTag ("CanvasSplash");
		rendererComponentsTextSplash.SetActive (false);

		panelNoStaticAmbientExtendedTracking = GameObject.FindGameObjectWithTag ("PanelNoStaticAmbientExtendedTracking").GetComponent<UnityEngine.UI.Image> ();


	}

	public void hideNoTrackingMessage ()
	{
		rendererComponentsNoTracking.SetActive (false);
	}

	public void showNoTrackingMessage ()
	{
		uiLabelNoTracking.text = NOTRACKINGMESSAGE[magnet.GetCount()];
		rendererComponentsNoTracking.SetActive (true);
	}

	public void showUI (string nameContex)
	{
			
		hideNoTrackingMessage ();

		/*********add an in if to catch personal message**********/
		//if ((currentCounter = magnet.GetCount () )== context8 && contextSide8==nameContex) {
		//uiLabel.text = personalMessage8;
		//rendererComponents.SetActive (true);	
		//}
		/*********add an in if to catch personal message**********/
		//else
		{
			uiLabel.text = LABEL [magnet.GetCount ()];
			rendererComponents.SetActive (true);
		}

	}

	public void hideUI ()
	{
		
		rendererComponents.SetActive (false);
		showNoTrackingMessage ();

	}

	public void showTrackingSideOrOtherInformation ()
	{
		rendererComponentsTrackingSideOrOtherInformation.SetActive (false);
	}
	
	public void showTrackingSideOrOtherInformation (string text)
	{
		uiLabelTrackingSide.text = text;
		rendererComponentsTrackingSideOrOtherInformation.SetActive (true);
	}

	public void showNoStaticAmbientExtendedTrackingOff ()
	{
		panelNoStaticAmbientExtendedTracking.color = UnityEngine.Color.green;
	}
	
	public void showNoStaticAmbientExtendedTrackingOn ()
	{
		panelNoStaticAmbientExtendedTracking.color = UnityEngine.Color.red;
	}

	public IEnumerator splash (string text)
	{
		uiLabelSplash.text = text;
		rendererComponentsTextSplash.SetActive (true);
		yield return new WaitForSeconds(3f);
		rendererComponentsTextSplash.SetActive (false);

	}
	
}