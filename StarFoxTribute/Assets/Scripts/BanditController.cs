﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditController : MonoBehaviour
{
    public GameObject laserPrefab;
    public Transform shootingPoint;

    public float vulnerabilityTime = 3.0f;
    public float shotTime = 3.0f;

    public float range = 5.0f;
    public float distance = 3.0f;

    float elapsed = 0f;
    float elapsedShoot = 0f;
    float elapsedRafaga = 0f; 

    bool vulnerable = false;
    bool alive = true;

    SphereCollider detector;
    BoxCollider body;
    public AudioClip gothit;
    public AudioClip shot;

    void Start(){

        detector = GetComponent<SphereCollider>();
        body = GetComponent<BoxCollider>();

        detector.enabled = true;
        body.enabled = false;
    }

    void Update()
    {
        Vector3 prediction = GetComponent<SpaceshipPredictor>().Prediction();
        transform.LookAt(prediction);

        elapsed += Time.deltaTime;
        if (vulnerable && elapsed > vulnerabilityTime) Recover();
        if (!alive) transform.localScale /= 1.1f;

        elapsedShoot += Time.deltaTime;
        if (elapsedShoot > shotTime){
            StartCoroutine(Shoot(0.2f));

            elapsedShoot = 0.0f;
        }
    }

    void changePosition(){

        elapsed = 0;

        Vector3 newPos = LoadNewPosition(transform.localPosition, range, distance);
        transform.localPosition = newPos;

        vulnerable = true;
        detector.enabled = false;
        body.enabled = true;

        transform.GetChild(3).GetComponent<ParticleSystem>().Play();
    }

    Vector3 LoadNewPosition(Vector3 oldPosition, float range, float distance){

        bool found = false;

        while (!found){

            Vector3 newPosition = new Vector3(Random.Range(-range, range), Random.Range(-range, range), 0);
            
            Vector3 clampedPosition = newPosition;

            clampedPosition.x = Mathf.Clamp(clampedPosition.x, -range, +range);
            clampedPosition.y = Mathf.Clamp(clampedPosition.y, -range, +range);


            if (Vector3.Distance(clampedPosition, oldPosition) > distance) return newPosition;
        }

        return oldPosition;
    }

    void OnTriggerEnter(Collider c){

        if (c.gameObject.tag == "MyLaser"){
            if (vulnerable){
                Destroy();
                Destroy(c.gameObject);
                TimeFreeze.INSTANCE.FreezeTime(6);
            } else {
                changePosition();
            }
        }
    }

    void Recover(){
        vulnerable = false;
        detector.enabled = true;
        body.enabled = false;
    }

    void Destroy(){
        transform.gameObject.GetComponent<AudioSource>().pitch = 1;
        transform.gameObject.GetComponent<AudioSource>().PlayOneShot(gothit);
        alive = false;
        ParticleSystem ps = transform.GetChild(0).GetComponent<ParticleSystem>();
        ps.Play();
        Destroy(transform.parent.gameObject, ps.main.duration);
    }

    IEnumerator Shoot(float delay){
        transform.gameObject.GetComponent<AudioSource>().pitch = Random.Range(0.8f,1.2f);
        transform.gameObject.GetComponent<AudioSource>().PlayOneShot(shot,1.0f);
        Instantiate(laserPrefab, shootingPoint.position, shootingPoint.rotation);
        yield return new WaitForSeconds(delay);
        Instantiate(laserPrefab, shootingPoint.position, shootingPoint.rotation);
        yield return new WaitForSeconds(delay);
        Instantiate(laserPrefab, shootingPoint.position, shootingPoint.rotation);
    }

    void OnEnable(){
        transform.GetChild(3).GetComponent<ParticleSystem>().Play();
    }
}
