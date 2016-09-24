﻿/*===============================================================================
Copyright (c) 2015-2016 PTC Inc. All Rights Reserved.
Copyright (c) 2015 Qualcomm Connected Experiences, Inc. All Rights Reserved.
Vuforia is a trademark of PTC Inc., registered in the United States and other 
countries.
===============================================================================*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Vuforia;

public class ViewTrigger : MonoBehaviour
{

    #region PUBLIC_MEMBER_VARIABLES
    public float activationTime = 1.5f;
    public Material focusedMaterial;
    public Material nonFocusedMaterial;

    public bool Focused { get; set; }
    #endregion // PUBLIC_MEMBER_VARIABLES


    #region PRIVATE_MEMBER_VARIABLES
    private float mFocusedTime = 0;
    private bool mTriggered = false;

	private Magnet magnet;
	private AugmentationRenderer augmentationRenderer;

	private int nextCounter , currentCounter;


    #endregion // PRIVATE_MEMBER_VARIABLES


    #region MONOBEHAVIOUR_METHODS
    void Start()
    {
        mTriggered = false;
        mFocusedTime = 0;
        Focused = false;
        GetComponent<Renderer>().material = nonFocusedMaterial;
		magnet = (Magnet)GameObject.FindWithTag ("Magnet").GetComponent<Magnet> ();
		augmentationRenderer = FindObjectOfType<AugmentationRenderer> ();
    }

    void Update()
    {

		if (mTriggered) 
			return;

        UpdateMaterials(Focused);

		bool startAction = false;
		if (Input.GetMouseButtonUp (0)) 
		{
			startAction = true;
		}

        if (Focused)
        {
            // Update the "focused state" time
            mFocusedTime += Time.deltaTime;
			if ((mFocusedTime > activationTime) || startAction)
            {
                mTriggered = false;

                mFocusedTime = 0;

				switch (focusedMaterial.name) {

				case "HandsButtonFocusedMaterial":
					augmentationRenderer.handlerRenderOffOrOnfHands (2);
					break;
				case "AugmentationButtonFocusedMaterial":
					augmentationRenderer.handlerRenderOffOrOnAll (2);
					break;
				case "NextButtonFocusedMaterial":
					magnet.nextScene ();
					break;

				}
                
            }
        }
        else
        {
            // Reset the "focused state" time
            mFocusedTime = 0;
        }
    }
    #endregion // MONOBEHAVIOUR_METHODS


    #region PRIVATE_METHODS
    private void UpdateMaterials(bool focused)
    {
        Renderer meshRenderer = GetComponent<Renderer>();
        if (focused)
        {
            if (meshRenderer.material != focusedMaterial)
                meshRenderer.material = focusedMaterial;
        }
        else
        {
            if (meshRenderer.material != nonFocusedMaterial)
                meshRenderer.material = nonFocusedMaterial;
        }
        
        float t = focused ? Mathf.Clamp01(mFocusedTime / activationTime) : 0;
        foreach (var rnd in GetComponentsInChildren<Renderer>())
        {
            if (rnd.material.shader.name.Equals("Custom/SurfaceScan"))
            {
                rnd.material.SetFloat("_ScanRatio", t);
            }
        }
    }

    private IEnumerator ResetAfter(float seconds)
    {
        Debug.Log("Resetting View trigger after: " + seconds);

        yield return new WaitForSeconds(seconds);

        Debug.Log("Resetting View trigger: " + this.gameObject.name);

        // Reset variables
        mTriggered = false;
        mFocusedTime = 0;
        Focused = false;
        UpdateMaterials(false);
    }


    #endregion // PRIVATE_METHODS
}

