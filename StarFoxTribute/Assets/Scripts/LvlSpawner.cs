using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class LvlSpawner : MonoBehaviour
{
    public GameObject asteroid1;
    public GameObject asteroid2;
    public GameObject turret1;
    public GameObject turret2;
    public GameObject ship;

    public float maxDist = 950f;
    public float width = 10f;
    public float height = 10f;
    public float spawnRate = 7f;
    // Start is called before the first frame update
    void SpawnAsteroid1() {
        Vector3 newPos = Random.insideUnitCircle.normalized;
        newPos.x*=width;
        newPos.y*=height;
        Instantiate(asteroid1, transform.position+newPos, Quaternion.identity);
    }
    void SpawnAsteroid2() {
        Vector3 newPos = Random.insideUnitCircle.normalized;
        newPos.x*=width;
        newPos.y*=(height/5f);
        Instantiate(asteroid2, transform.position+newPos, Quaternion.identity);
    }

    void SpawnTurret1() {
        Vector3 newPos = Random.insideUnitCircle.normalized;
        newPos.x*=width;
        newPos.y*=height;
        GameObject spawned = Instantiate(turret1, transform.position+newPos, Quaternion.identity);
        spawned.transform.GetComponent<SpaceshipPredictor>().ship = ship;
    }

    void SpawnTurret2() {
        Vector3 newPos = Random.insideUnitCircle.normalized;
        newPos.x*=width;
        newPos.y*=0;
        GameObject spawned = Instantiate(turret2, transform.position+newPos, Quaternion.identity);
        spawned.transform.GetComponent<Turret2Controller>().ship = ship;
    }

    void Spawn() {
        float prob = Random.Range(0.0f, 1.0f);
        if (prob <= 0.25f) {
            SpawnAsteroid1();
        } else if (prob <= 0.5f) {
            SpawnAsteroid2();
        } else if (prob <= 0.75f) {
            SpawnTurret1();
        } else {
            SpawnTurret2();
        }
    }

    void Start()
    {
        InvokeRepeating("Spawn", 1.0f, spawnRate);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.GetComponent<CinemachineDollyCart>().m_Position >= maxDist) {
            CancelInvoke();
        }        
    }
}
