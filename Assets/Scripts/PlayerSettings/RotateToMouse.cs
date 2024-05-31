using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToMouse : MonoBehaviour
{
    public float offset;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        Vector3 difference = mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotateZ + offset);
        
        Vector3 LocalScale = Vector3.one;
        
        if (rotateZ > 90 || rotateZ < -90)
        {
            LocalScale.y = -1f;
        }
        else
        {
            LocalScale.y = 1f;
        }
        
        transform.localScale = LocalScale;
    }
}
