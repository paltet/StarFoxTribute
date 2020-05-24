using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret2Controller : MonoBehaviour
{

    public float range = 50;
    public float explosionTime = 5;
    public float laserCount = 8;
    public GameObject ship;
    public GameObject laserPrefab;

    bool triggered = false;
    float timer;
    float trigger;

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, ship.transform.position) < range && !triggered){
            Trigger();
            timer = explosionTime /2;
        }

        if (triggered){
            explosionTime -= Time.deltaTime;


            if (explosionTime < timer)  GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);

            if (explosionTime < 0) Explode();
        }
    }

    void Trigger(){
        this.transform.GetChild(0).transform.Translate(new Vector3(0, -0.5f, 0), Space.Self);
        this.transform.GetChild(1). transform.Translate(new Vector3(0, 0.5f, 0), Space.Self);
        triggered = true;
    }

    void Explode(){

        float angle = 360 / laserCount;
        for(float i = 0; i < laserCount; i++){
            
            Instantiate(laserPrefab, transform.position, transform.rotation);
            transform.Rotate(new Vector3(0, angle, 0), Space.Self);

        }
        Destroy(gameObject);
    }
}
