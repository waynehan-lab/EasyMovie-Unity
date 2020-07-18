using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class Main : MonoBehaviour {

    public MeshRenderer meshRenderer;
    private AndroidJavaObject nativeObject;
    private int width, height;
    private Texture2D texture2D;

    void Awake()
    {
    }

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
        bool updateFrame = nativeObject.Call<bool>("isUpdateFrame");
        if (updateFrame)
        {
            Debug.Log("VideoPlugin:Update");
            meshRenderer.material.mainTexture = texture2D;
            nativeObject.Call("updateTexture");
            GL.InvalidateState();
        }
    }

    public void Click()
    {
        Debug.Log("VideoPlugin:Start");
        texture2D = new Texture2D(width, height, TextureFormat.RGB565, false, false);
        texture2D.wrapMode = TextureWrapMode.Clamp;
        texture2D.filterMode = FilterMode.Bilinear;
        nativeObject.Call("start", (int)texture2D.GetNativeTexturePtr(), width, height);
    }

}
