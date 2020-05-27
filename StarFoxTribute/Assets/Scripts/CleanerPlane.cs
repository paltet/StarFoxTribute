using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanerPlane : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Asteroid") || other.gameObject.CompareTag("Turret") || other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }
    }
}
