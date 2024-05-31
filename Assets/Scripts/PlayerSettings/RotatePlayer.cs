using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayer : MonoBehaviour
{
    private Vector3 pos;
    private Camera main;
    void Start()
    {
        main = Camera.main;    
    }

    void Update()
    {
        Flip();
        pos = main.WorldToScreenPoint(transform.position);
    }

    private void Flip()
    {
        if (Input.mousePosition.x < pos.x)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else if (Input.mousePosition.x > pos.x)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
