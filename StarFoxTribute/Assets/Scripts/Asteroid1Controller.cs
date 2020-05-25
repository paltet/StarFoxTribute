using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid1Controller : MonoBehaviour
{
    public float rotationRate;

    Vector3 rotationVector;

    void Start() {
        rotationVector = Random.insideUnitSphere.normalized;
    }

    void Update() {
        transform.Rotate(rotationVector, rotationRate * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other){
        string tag = other.gameObject.tag;
        if (tag == "MyLaser" || tag == "Player") {            
            Explode();
        }
    }

    void Explode() {
        if (transform.childCount > 0) {
            ParticleSystem exp = transform.GetChild(0).GetComponent<ParticleSystem>();
            exp.Play();
            exp.transform.parent = null; //so particle system doesnt disapear but we can destroy asteroid
            Destroy(gameObject);
            Destroy(exp,exp.duration);
        }
    }
}
