using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Controller : MonoBehaviour
{
    public GameObject asteroid;
    public float speed = 1.0f;
    public float movementLength = 5.0f;

    public float spawnRate = 4.0f;

    public float maxHealth = 20f;

    public float currentHealth;

    Vector3 newPosition;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn",1.0f,spawnRate);
        newPosition = Random.insideUnitCircle.normalized*movementLength;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Debug.Log(currentHealth);
    }

    void Movement()
    {
        float step =  speed * Time.deltaTime; // calculate distance to move
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
        //Instantiate in dollycart
        GameObject spawned = Instantiate(asteroid, transform.parent.position, Quaternion.identity);
        Destroy(spawned,5.0f);
    }

    void ResetPosition() {
        transform.localPosition = Vector3.zero;
        newPosition = Random.insideUnitCircle.normalized*movementLength;
    }

    void Reduce(){
        transform.localScale /= 1.05f;
    }

    void Death(){
        gameObject.GetComponent<Collider>().isTrigger = false;
        CancelInvoke();
        ParticleSystem exp = transform.GetChild(4).GetComponent<ParticleSystem>();
        exp.Play();
        float t = exp.duration;
        Destroy(exp, t);
        Destroy(gameObject, 0.7f*t);
        exp.transform.parent = null;
        InvokeRepeating("Reduce",0f,0.05f);
        Invoke("CancelInvoke",0.6f*t);
    }

    void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "Terrain"){
            currentHealth -= 2f;
            ResetPosition();
        } else if (other.gameObject.tag == "MyLaser"){
            currentHealth -= 5f;
        }
        if (currentHealth<=0f){
            Death();
        }
        Debug.Log(currentHealth);
    }
}
