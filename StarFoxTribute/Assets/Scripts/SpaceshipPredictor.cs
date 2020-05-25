using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipPredictor : MonoBehaviour
{
    public Transform shootingPoint;
    public GameObject shooter;
    public GameObject ship;
    public GameObject laserPrefab;

    public Vector3 Prediction(){

        Vector3 shipPos = ship.transform.position;
        Vector3 shooterPos = shootingPoint.position;

        Vector3 shipVelocity = Camera.main.velocity;
        //Debug.Log(Camera.main.velocity);
        Vector3 shooterVelocity = transform.GetComponent<Rigidbody>() ? shooter.GetComponent<Rigidbody>().velocity : Vector3.zero;

        float shotSpeed = laserPrefab.GetComponent<LaserController>().speed;

        return computeInterception(shipPos, shooterPos, shipVelocity, shooterVelocity, shotSpeed);
    }

    Vector3 computeInterception(Vector3 targetPos, Vector3 shooterPos, Vector3 targetVelocity, Vector3 shooterVelocity, float shotSpeed){
        
        Vector3 relativePos = targetPos - shooterPos;
        Vector3 relativeVelocity = targetVelocity - shooterVelocity;

        float time = FirstOrderIntercept(shotSpeed, relativePos, relativeVelocity);

        return targetPos + time * relativeVelocity;
    }

    float FirstOrderIntercept(float shotSpeed, Vector3 relativePos, Vector3 relativeVelocity){

        float sqrdVelocity = relativeVelocity.sqrMagnitude;

        if (sqrdVelocity < 0.001f) return 0f;

        float a = sqrdVelocity - shotSpeed*shotSpeed;

        if (Mathf.Abs(a) < 0.001f){
            float t = -relativePos.sqrMagnitude/(
                2f*Vector3.Dot(relativeVelocity, relativePos)
            );
            return Mathf.Max(t, 0f);
        }

        float b = 2f*Vector3.Dot(relativeVelocity, relativePos);
        float sqrdPos = relativePos.sqrMagnitude;
        float det = b*b - 4f*a*sqrdPos;

        if (det > 0f){
            float t1 = (-b + Mathf.Sqrt(det))/(2f*a);
            float t2 = (-b - Mathf.Sqrt(det))/(2f*a);

            if (t1 > 0f){
                if (t2 > 0f){
                    return Mathf.Min(t1, t2);
                } else return t1;
            } else return Mathf.Max(t2, 0f);
        } else if (det < 0f) return 0f;
        else return Mathf.Max(-b/(2f*a), 0f);
    }
}
