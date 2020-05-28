using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Controller : MonoBehaviour
{
    public GameObject asteroid;
    public GameObject asteroid2;
    public GameObject turret;
    public float turretProbability = 0.1f;
    public float enemySpeed = 1.0f;
    public float asteroidSpeed = 2.0f;
    public float movementLength = 5.0f;

    public float spawnRate = 4.0f;

    public float spawnStartTime = 0.0f;

    public float maxHealth = 20f;

    public float currentHealth;
    public AudioClip gothit;

    bool alive = true;

    Vector3 newPosition;
    Vector3 cartPos;
    GameObject spawned;

    // Start is called before the first frame update
    void OnEnable()
    {
        ParticleSystem exp = transform.Find("DustExplosion").GetComponent<ParticleSystem>();
        exp.Play();
    }
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
        float prob = Random.Range(0f,1f);
        if (prob <= (1f-turretProbability)) {
            if (prob <= (1f-turretProbability)/2f)
                spawned = Instantiate(asteroid, transform.position, Quaternion.identity);
            else
                spawned = Instantiate(asteroid2, transform.position, Quaternion.identity);
            cartPos = transform.parent.position;
            Destroy(spawned,4.0f);
        }
        else {
            spawned = Instantiate(turret, transform.position, Quaternion.identity);
            spawned.transform.GetComponent<Turret2Controller>().range /= 2.5f;
            spawned.transform.GetComponent<Turret2Controller>().explosionTime /= 2.5f;
            spawned.transform.GetComponent<Turret2Controller>().ship = transform.parent.GetComponent<MaintainDistance>().playerCart;
            cartPos = transform.parent.position;
        }
    }

    void ResetPosition() {
        transform.localPosition = Vector3.zero;
        newPosition = Random.insideUnitCircle.normalized*movementLength;
    }

    void Reduce(){
        transform.localScale /= 1.05f;
    }

    void Hit(){
        transform.gameObject.GetComponent<AudioSource>().PlayOneShot(gothit);
        if (transform.childCount > 4) {
            ParticleSystem exp = transform.Find("FlashHit").gameObject.GetComponent<ParticleSystem>();
            exp.Play();
        }
        TimeFreeze.INSTANCE.FreezeTime(2);
    }

    void Death(){
        if (transform.childCount > 0) {
            gameObject.GetComponent<Collider>().isTrigger = false;
            CancelInvoke("Spawn");
            ParticleSystem exp = transform.GetChild(4).GetComponent<ParticleSystem>();
            exp.Play();
            float t = exp.main.duration;
            Destroy(exp.gameObject, t);
            Destroy(gameObject, 0.7f*t);
            exp.transform.parent = null;
            InvokeRepeating("Reduce",0f,0.05f);
            Invoke("CancelInvoke",0.6f*t);
            TimeFreeze.INSTANCE.FreezeTime(6);
        }
    }

    void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "Terrain"){
            currentHealth -= 2f;
            ResetPosition();
        } else if (other.gameObject.tag == "MyLaser"){
            Destroy(other.gameObject);
            currentHealth -= 5f;
            if(currentHealth > 0f) {
                Hit();
            }
        }
    }
}
