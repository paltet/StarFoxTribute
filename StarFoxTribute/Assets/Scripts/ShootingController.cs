using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Transform shootPointLeft;
    public Transform shootPointRight;

    public GameObject laserPrefab;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            Shoot();
        }
    }

    void Shoot(){
        Instantiate(laserPrefab, shootPointLeft.position, shootPointLeft.rotation);
        Instantiate(laserPrefab, shootPointRight.position, shootPointRight.rotation);
    }
}
