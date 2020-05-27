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
    public AudioClip exploding;
    public AudioClip gothit;

    bool triggered = false;
    float timer;
    float trigger;

    bool alive = true;

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

            if (explosionTime < 0 && alive) Explode();
        }
        if (!alive) transform.localScale /= 1.05f;
    }

    void Trigger(){
        this.transform.GetChild(0).transform.Translate(new Vector3(0, -0.5f, 0), Space.Self);
        this.transform.GetChild(1).transform.Translate(new Vector3(0, 0.5f, 0), Space.Self);
        triggered = true;
        //transform.gameObject.GetComponent<AudioSource>().PlayOneShot(loading);
    }

    void Explode(){
        alive = false;

        float angle = 360 / laserCount;
        for(float i = 0; i < laserCount; i++){
            
            Instantiate(laserPrefab, transform.position, transform.rotation);
            transform.Rotate(new Vector3(0, angle, 0), Space.Self);

        }
        transform.gameObject.GetComponent<AudioSource>().PlayOneShot(exploding);
        ParticleSystem exp = transform.GetChild(2).GetComponent<ParticleSystem>();
        exp.Play();
        Destroy(gameObject, Mathf.Max(exp.duration,exploding.length));
    }

    void OnTriggerEnter(Collider other){
        string tag = other.gameObject.tag;
        if (tag == "Player") {            
            Killed();
        }
        else if (tag == "MyLaser") {
            Destroy(other.gameObject);
            Killed();
            TimeFreeze.INSTANCE.FreezeTime(6);
        }
    }
    
    void Killed(){
        transform.gameObject.GetComponent<AudioSource>().pitch = 1;
        transform.gameObject.GetComponent<AudioSource>().PlayOneShot(gothit);
        alive = false;
        ParticleSystem exp = transform.GetChild(2).GetComponent<ParticleSystem>();
        exp.Play();
        Destroy(gameObject, exp.duration);
    }
}
