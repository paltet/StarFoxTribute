using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditController : MonoBehaviour
{
    //public GameObject ship;
    public float changePositionEvery = 3.0f;
    public float range = 5.0f;
    public float distance = 3.0f;

    float elapsed = 0f;

    // Update is called once per frame
    void Update()
    {
        Vector3 prediction = GetComponent<SpaceshipPredictor>().Prediction();
        transform.LookAt(prediction);

        elapsed += Time.deltaTime;
        if (elapsed > changePositionEvery) changePosition();
    }

    void changePosition(){

        elapsed = 0;

        Vector3 newPos = LoadNewPosition(transform.localPosition, range, distance);
        transform.localPosition = newPos;

        /*
        Vector3 local = transform.localPosition;

        local.x = Mathf.Clamp(local.x, -range, +range);
        local.y = Mathf.Clamp(local.y, -range, +range);

        transform.localPosition = local;
        */
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

        
}
