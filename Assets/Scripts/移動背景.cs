using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 移動背景 : MonoBehaviour
{
    // 調整移動速度
    public float scrollSpeed = 0.5f;

    // 用來儲存材質的Material
    private Material backgroundMaterial;

    // 用來儲存材質的Offset
    private Vector2 mainTextureOffset;

    // Start is called before the first frame update
    void Start()
    {
        // 獲取物件上的材質
        backgroundMaterial = GetComponent<Renderer>().material;
        mainTextureOffset = backgroundMaterial.mainTextureOffset; // 初始化偏移量
    }

    // Update is called once per frame
    void Update()
    {
        // 持續在Y軸上移動材質的Offset
        mainTextureOffset.y += scrollSpeed * Time.deltaTime;

        // 更新材質的Offset
        backgroundMaterial.mainTextureOffset = mainTextureOffset;
    }
}