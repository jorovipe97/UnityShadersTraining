using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRayVision : MonoBehaviour {

    public Camera cam;
    public Shader OverDrawShader;

    private bool toggle = true;

	// Use this for initialization
	void Start () {
        StartCoroutine("ToggleXRayView");
	}
	
	// Update is called once per frame
	void Update () {
		/*if (Input.GetKeyDown(KeyCode.I))
        {
            if (toggle)
            {
                ActivteXRay();
                toggle = false;
            }
            else
            {
                DeActivateXRay();
                toggle = true;
            }
            
        }*/
	}

    public void ActivteXRay()
    {
        // If tag is "" the subshader of all objects in scene will be replaced
        // for the first subshader specified in OverDrawShader.
        cam.SetReplacementShader(OverDrawShader, "");
    }

    public void DeActivateXRay()
    {
        cam.ResetReplacementShader();
    }
    
    IEnumerator ToggleXRayView()
    {
        // Note: A coroutine does not need be inside a loop
        while (true)
        {

            if (toggle)
                ActivteXRay();
            else
                DeActivateXRay();

            toggle = !toggle;
            yield return new WaitForSeconds(1);

        }
        
    }
}
