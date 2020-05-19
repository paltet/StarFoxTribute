using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Vector3 offset;
    public GameObject Ship;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - Ship.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {

        /*
        float newXPosition = Ship.transform.position.x + offset.x;
        float newZPosition = Ship.transform.position.z + offset.z;
        float newYPosition = Ship.transform.position.y + offset.y;
 
        transform.position = new Vector3(newXPosition, newYPosition, newZPosition);
        */
    }
}
