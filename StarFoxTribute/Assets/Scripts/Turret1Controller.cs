using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret1Controller : MonoBehaviour
{

    public float range;
    public GameObject laserPrefab;
    public Transform shootingPoint;

    public AudioClip gothit;
    public AudioClip shot;

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
        transform.gameObject.GetComponent<AudioSource>().pitch = Random.Range(0.8f,1.2f);
        transform.gameObject.GetComponent<AudioSource>().PlayOneShot(shot);
        Instantiate(laserPrefab, shootingPoint.position, shootingPoint.rotation);
        //Debug.Log("shoot");
    }

    void OnTriggerEnter(Collider other){
        string tag = other.gameObject.tag;
        if (tag == "Player") {            
            Explode();
        }
        else if (tag == "MyLaser") {
            Destroy(other.gameObject);
            Explode();
            TimeFreeze.INSTANCE.FreezeTime(6);
        }
    }

    void Explode(){
        transform.gameObject.GetComponent<AudioSource>().pitch = 1;
        transform.gameObject.GetComponent<AudioSource>().PlayOneShot(gothit);
        alive = false;
        ParticleSystem exp = transform.GetChild(2).GetComponent<ParticleSystem>();
        exp.Play();
        Destroy(gameObject, exp.duration);
    }

    void Reduce(){
        transform.localScale /= 1.05f;
    }
}
