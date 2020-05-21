using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour
{
    public Camera main;
    public Transform ship;
    public float range = 0.1f;

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.localPosition.z);
        Vector3 lookPos = main.ScreenToWorldPoint(mousePos);

        var shi = Camera.main.WorldToViewportPoint(ship.position);
        var pos = Camera.main.WorldToViewportPoint(lookPos);
        pos.x = Mathf.Clamp(pos.x, shi.x - range, shi.x + range);
        pos.y = Mathf.Clamp(pos.y, shi.y - range, shi.y + range);
        transform.position = Camera.main.ViewportToWorldPoint(pos);

        //transform.position = lookPos;
    }
}
