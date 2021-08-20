using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundRepeat : MonoBehaviour
{
    public const float width = 50f;
    public const int backgroundNum = 2;
    Transform mainCamera = null;

    Vector3 initPos;

    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        initPos = transform.position;
    }

    
    void Update()
    {
        float totalWidth = width * backgroundNum;
        float distZ = mainCamera.position.z - initPos.z;

        int n = Mathf.RoundToInt(distZ / totalWidth);
        var pos = initPos;
        pos.z += n * totalWidth;
        transform.position = pos;
    }
}
