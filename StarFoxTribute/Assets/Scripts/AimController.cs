using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour
{
    public Camera main;


    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.localPosition.z);
        Vector3 lookPos = main.ScreenToWorldPoint(mousePos);

        Debug.Log(lookPos);
        transform.position = lookPos;
    }
}
