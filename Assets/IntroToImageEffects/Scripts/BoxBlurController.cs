using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* TODO: Tutorials
 * For more knowledgment about image effects
 * https://docs.unity3d.com/550/Documentation/Manual/WritingImageEffects.html
 * https://unity3d.com/es/learn/tutorials/s/graphics
 */

[ExecuteInEditMode]
public class BoxBlurController : MonoBehaviour {

    public Material blurMaterial;
    [Range(0, 12)]
    public int ciclos;
    [Range(0, 4)]
    public int DownRes;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        // About image scaling https://goo.gl/bi2fPV
        int width = source.width >> DownRes;
        int height = source.height >> DownRes;

        RenderTexture rt = RenderTexture.GetTemporary(width, height);
        Graphics.Blit(source, rt);
        for (int i = 0; i < ciclos; i++)
        {
            RenderTexture rt2 = RenderTexture.GetTemporary(width, height);
            Graphics.Blit(rt, rt2, blurMaterial);
            RenderTexture.ReleaseTemporary(rt);
            rt = rt2;
        }

        Graphics.Blit(rt, destination);
        RenderTexture.ReleaseTemporary(rt);
    }
}
