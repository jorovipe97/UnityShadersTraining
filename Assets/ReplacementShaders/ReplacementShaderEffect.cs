using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ReplacementShaderEffect : MonoBehaviour {

    public Shader ReplacementShader;
    public string Tag = "RenderType";

    public Color OverDrawColor;

    private void OnValidate()
    {
        Shader.SetGlobalColor("_OverDrawColor", OverDrawColor);
    }

    private void OnEnable()
    {
        Debug.Log("Script enabled");
        if (ReplacementShader != null)
        {
            GetComponent<Camera>().SetReplacementShader(ReplacementShader, Tag);
        }
    }

    private void OnDisable()
    {
        Debug.Log("Script disabled");
        GetComponent<Camera>().ResetReplacementShader();
    }
}
