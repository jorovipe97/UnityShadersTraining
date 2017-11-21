using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ReplacementShaderEffect : MonoBehaviour {

    public Shader ReplacementShader;

    private void OnEnable()
    {
        Debug.Log("Script enabled");
        if (ReplacementShader != null)
        {
            GetComponent<Camera>().SetReplacementShader(ReplacementShader, "RenderType");
        }
    }

    private void OnDisable()
    {
        Debug.Log("Script disabled");
        GetComponent<Camera>().ResetReplacementShader();
    }
}
