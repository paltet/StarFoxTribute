using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    public float speed = 10f;
    public Rigidbody rigidbody;
    public float destroyTimer;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = transform.forward * speed;
    }

    void Update() {
        Destroy(gameObject, destroyTimer);
    }
}
