using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid2Controller : MonoBehaviour
{
    public float rotationRate;
    public float length = 10f;
    Vector3 startPos;
    Vector3 rotationVector;

    void Start() {
        rotationVector = Random.insideUnitSphere.normalized;
        startPos = transform.position;
    }

    void Update() {
        transform.Rotate(rotationVector, rotationRate * Time.deltaTime);
        transform.position = startPos + new Vector3(0,length*Mathf.Sin(Time.timeSinceLevelLoad),0);
    }

    void OnTriggerEnter(Collider other){
        string tag = other.gameObject.tag;
        if (tag == "MyLaser" || tag == "Player") {            
            Explode();
        }
    }

    void Explode() {
        ParticleSystem exp = transform.GetChild(0).GetComponent<ParticleSystem>();
        exp.Play();
        exp.transform.parent = null; //so particle system doesnt disapear but we can destroy asteroid
        Destroy(gameObject);
        Destroy(exp,exp.duration);
    }

}
