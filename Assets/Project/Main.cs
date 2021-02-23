using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class Main : MonoBehaviour
{

    public MeshRenderer meshRenderer;
    public RawImage rawImage;
    private AndroidJavaObject nativeObject;
    private int width, height;
    private Texture2D texture2D;

    // Use this for initialization
    void Start()
    {
        nativeObject = new AndroidJavaObject("com.example.videoplugin.VideoPlugin");
        width = 1600;
        height = 900;
        Debug.Log("VideoPlugin:" + width + ", " + height);
    }

    // Update is called once per frame
    void Update()
    {
        if (texture2D != null && nativeObject.Call<bool>("isUpdateFrame"))
        {
            Debug.Log("VideoPlugin:Update");
            nativeObject.Call("updateTexture");
            GL.InvalidateState();
        }
    }

    public void Click()
    {
        Debug.Log("VideoPlugin:Start");
        if (texture2D == null)
        {
            texture2D = new Texture2D(width, height, TextureFormat.RGB24, false, false);
            //texture2D.wrapMode = TextureWrapMode.Clamp;
            //texture2D.filterMode = FilterMode.Bilinear;
            //texture2D.Apply();
            nativeObject.Call("start", (int)texture2D.GetNativeTexturePtr(), width, height);
            meshRenderer.material.mainTexture = texture2D;
            rawImage.texture = texture2D;
        }
    }

}
