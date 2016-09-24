using UnityEngine;
using System.Collections;
/// 
/// Deactivator animation manager. This class is used to deactivate the animation at runtime. It is all automatic
///
public class DeactivatorAnimationManager : MonoBehaviour {


	private MyAnimationObject[] myAnimationObjects;
	private Magnet magnet;


	// Use this for initialization
	void Start () {
		myAnimationObjects = FindObjectsOfType(typeof(MyAnimationObject)) as MyAnimationObject[];
		magnet = (Magnet)GameObject.FindWithTag ("Magnet").GetComponent<Magnet> ();

	}
	
	// Update is called once per frame
	void Update () {
		foreach (MyAnimationObject myAnimationObject in myAnimationObjects) {

			if(myAnimationObject.sceneNumber == magnet.GetCount())
				myAnimationObject.enabled = true;
		}
	}



}
