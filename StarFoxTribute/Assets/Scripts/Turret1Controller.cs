using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret1Controller : MonoBehaviour
{

    public float range;
    public GameObject laserPrefab;
    public Transform shootingPoint;

    public AudioClip gothit;

    float elapsed = 0f;
    bool alive = true;

    // Update is called once per frame
    void Update()
    {
        Vector3 prediction = GetComponent<SpaceshipPredictor>().Prediction();
        elapsed += Time.deltaTime;

        if (Vector3.Distance(prediction, transform.position) <= range){
            transform.LookAt(prediction);
            
            if (elapsed > 1f){
                if (alive) Shoot();
                elapsed = 0f;
            }

        }

        if (!alive) Reduce();
    }

    void Shoot(){
        Instantiate(laserPrefab, shootingPoint.position, shootingPoint.rotation);
        //Debug.Log("shoot");
    }

    void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "MyLaser"){
            transform.gameObject.GetComponent<AudioSource>().PlayOneShot(gothit);
            alive = false;
            ParticleSystem exp = transform.GetChild(2).GetComponent<ParticleSystem>();
            exp.Play();
            Destroy(gameObject, exp.duration);
        }
    }

    void Reduce(){
        transform.localScale /= 1.05f;
    }
}
