using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret1Controller : MonoBehaviour
{

    public float range;
    public GameObject laserPrefab;
    public Transform shootingPoint;

    // Update is called once per frame
    void Update()
    {
        Vector3 prediction = GetComponent<SpaceshipPredictor>().Prediction();

        if (Vector3.Distance(prediction, transform.position) <= range){
            transform.LookAt(prediction);
            Shoot();
        }
    }

    void Shoot(){
        Instantiate(laserPrefab, shootingPoint.position, shootingPoint.rotation);
        Debug.Log("shoot");
    }
}
