using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EndPlaneScene : MonoBehaviour
{
    public GameObject camera;

    void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.CompareTag("Player"))
        {
            SceneController sc = camera.GetComponent<SceneController>();
            sc.EndScene();
        }
        else if(other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Asteroid")) {
            Destroy(other.gameObject);
        }
    }
}
