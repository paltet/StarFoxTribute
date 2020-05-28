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
    public GameObject bandit;
    public GameObject playerCart;
    public GameObject ship;

    public float banditProb = 0.1f;

    public float maxDist = 950f;
    public float minD = 60f;
    public float maxD = 70f;
    public float width = 10f;
    public float height = 10f;
    public float spawnRate = 7f;
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

    void SpawnBandit() {
        Vector3 newPos = Random.insideUnitCircle.normalized;
        newPos.x*=width;
        newPos.y*=height;
        GameObject spawned = Instantiate(bandit, transform.position+newPos, Quaternion.identity);
        spawned.transform.GetComponent<MaintainDistance>().playerCart = playerCart;
        spawned.transform.GetChild(0).GetComponent<SpaceshipPredictor>().ship = ship;
        spawned.transform.GetComponent<CinemachineDollyCart>().m_Path = transform.GetComponent<CinemachineDollyCart>().m_Path;
        spawned.transform.GetComponent<CinemachineDollyCart>().m_Position = transform.GetComponent<CinemachineDollyCart>().m_Position;        
        spawned.transform.GetComponent<CinemachineDollyCart>().m_Speed = transform.GetComponent<CinemachineDollyCart>().m_Speed;
        spawned.transform.GetComponent<MaintainDistance>().isAutoSpawned = true;
        spawned.transform.GetComponent<MaintainDistance>().minDist = minD;
        spawned.transform.GetComponent<MaintainDistance>().maxDist = maxD;
        spawned.transform.GetComponent<BanditSpawner>().awakePos = transform.GetComponent<CinemachineDollyCart>().m_Position;
    }

    void Spawn() {
        float prob = Random.Range(0.0f, 1.0f);
        if (prob <= banditProb) {
            SpawnBandit();
        } else if (prob <= (1f-banditProb)/4f) {
            SpawnAsteroid1();
        } else if (prob <= 2f*(1f-banditProb)/4f) {
            SpawnAsteroid2();
        } else if (prob <= 3f*(1f-banditProb)/4f) {
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
