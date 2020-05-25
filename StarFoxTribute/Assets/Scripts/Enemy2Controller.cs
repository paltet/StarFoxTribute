﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Controller : MonoBehaviour
{
    public GameObject asteroid;
    public float enemySpeed = 1.0f;
    public float asteroidSpeed = 2.0f;
    public float movementLength = 5.0f;

    public float spawnRate = 4.0f;

    public float spawnStartTime = 0.0f;

    public float maxHealth = 20f;

    public float currentHealth;

    bool alive = true;

    Vector3 newPosition;
    Vector3 cartPos;
    GameObject spawned;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn",spawnStartTime,spawnRate);
        newPosition = Random.insideUnitCircle.normalized*movementLength;
        currentHealth = maxHealth;
        alive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth<=0f && alive){
            Death();
            alive = false;
        }
        Movement();
        if(spawned != null) {
            float step =  asteroidSpeed * Time.deltaTime; // calculate distance to move
            spawned.transform.position = Vector3.MoveTowards(spawned.transform.position, cartPos, step);
        }
    }

    void Movement()
    {
        float step =  enemySpeed * Time.deltaTime; // calculate distance to move
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, newPosition, step);

        // Check if the position of the newPosition and Enemy are approximately equal.
        if (Vector3.Distance(transform.localPosition, newPosition) < 0.001f)
        {
            // Calculate new random position
            newPosition = Random.insideUnitCircle.normalized*movementLength;
        }
    }

    void Spawn()
    {
        spawned = Instantiate(asteroid, transform.position, Quaternion.identity);
        cartPos = transform.parent.position;
        Destroy(spawned,4.0f);
    }

    void ResetPosition() {
        transform.localPosition = Vector3.zero;
        newPosition = Random.insideUnitCircle.normalized*movementLength;
    }

    void Reduce(){
        transform.localScale /= 1.05f;
    }

    void Death(){
        if (transform.childCount > 0) {
            gameObject.GetComponent<Collider>().isTrigger = false;
            CancelInvoke("Spawn");
            ParticleSystem exp = transform.GetChild(4).GetComponent<ParticleSystem>();
            exp.Play();
            float t = exp.duration;
            Destroy(exp, t);
            Destroy(gameObject, 0.7f*t);
            exp.transform.parent = null;
            InvokeRepeating("Reduce",0f,0.05f);
            Invoke("CancelInvoke",0.6f*t);
        }
    }

    void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "Terrain"){
            currentHealth -= 2f;
            ResetPosition();
        } else if (other.gameObject.tag == "MyLaser"){
            Destroy(other.gameObject);
            currentHealth -= 5f;
        }
    }
}