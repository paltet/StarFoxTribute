using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret1Controller : MonoBehaviour
{

    public float range;
    public GameObject laserPrefab;
    public Transform shootingPoint;

    float elapsed = 0f;

    // Update is called once per frame
    void Update()
    {
        Vector3 prediction = GetComponent<SpaceshipPredictor>().Prediction();
        elapsed += Time.deltaTime;

        if (Vector3.Distance(prediction, transform.position) <= range){
            transform.LookAt(prediction);
            
            if (elapsed > 1f){
                Shoot();
                elapsed = 0f;
            }

        }
    }

    void Shoot(){
        Instantiate(laserPrefab, shootingPoint.position, shootingPoint.rotation);
        //Debug.Log("shoot");
    }

    void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "MyLaser"){
            Destroy(gameObject);
        }
    }
}
