using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipController : MonoBehaviour
    {
    public float xVelocity = 0;
    public float yVelocity = 0;

    public Camera cam;   

    public float maxRotation = 30f;

    public float mouseZ = 1000f;

    public float speed = 0;

    public float lookRange = 10;

    public GameObject pointer;

    // Update is called once per frame
    void FixedUpdate()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, mouseZ);
        Vector3 lookPos = cam.ScreenToWorldPoint(mousePos);
        lookPos.z = mouseZ;
        lookPos.x = Mathf.Clamp(lookPos.x, transform.position.x - lookRange, transform.position.x + lookRange);
        lookPos.y = Mathf.Clamp(lookPos.y, transform.position.y - lookRange, transform.position.y + lookRange);

        Debug.Log(lookPos);

        pointer.transform.position = lookPos;


        xVelocity = speed * horizontal;
        yVelocity = speed * vertical;

        transform.Rotate(new Vector3(yVelocity, 0, -xVelocity));

        transform.Translate(new Vector3(xVelocity, yVelocity, 0), Space.World);

        var pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp(pos.x, 0.07f, 0.93f);
        pos.y = Mathf.Clamp(pos.y, 0.07f, 0.93f);
        transform.position = Camera.main.ViewportToWorldPoint(pos);


        Vector3 currentRotation = transform.rotation.eulerAngles;
        this.transform.LookAt(lookPos);
        currentRotation.z = clampRotation(currentRotation.z, maxRotation);
        currentRotation.x = transform.rotation.eulerAngles.x;
        currentRotation.y = transform.rotation.eulerAngles.y;
        transform.localRotation = Quaternion.Euler (currentRotation);
    }

    float clampRotation(float x, float max){
        
        if (x < max || x > 360-max) return x;
        else if (x > 180) return 360 - max;
        else return max;
    }
}
