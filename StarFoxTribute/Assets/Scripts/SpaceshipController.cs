using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipController : MonoBehaviour
    {
    private float xVelocity = 0;
    private float yVelocity = 0;

    //public Camera cam;   

    public float maxRotation = 30f;

    public float speed = 1;

    public float lookRange = 10;


    public GameObject pointer;
    public Transform shootPointLeft;
    public Transform shootPointRight;
    public GameObject laserPrefab;
    public Transform resetPoint;

    public float maxHealth = 100;
    public float currentHealth;

    public GameObject HealthBar;


    void Start(){
        currentHealth = maxHealth;
    }

    void Update(){
        if (Input.GetMouseButtonDown(0)){
            Shoot();
        }
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {

        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        //Debug.Log(Input.mousePosition.x);

        //pointer.transform.position = lookPos;
        //pointer.transform.rotation = cam.transform.rotation;

        xVelocity = speed * horizontal;
        yVelocity = speed * vertical;

        transform.Rotate(new Vector3(yVelocity, 0, -xVelocity));

        transform.Translate(new Vector3(xVelocity, yVelocity, 0), Camera.main.transform);

        var pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp(pos.x, 0.07f, 0.93f);
        pos.y = Mathf.Clamp(pos.y, 0.07f, 0.93f);
        transform.position = Camera.main.ViewportToWorldPoint(pos);


        Vector3 currentRotation = transform.localRotation.eulerAngles;
        this.transform.LookAt(pointer.transform);
        currentRotation.z = clampRotation(currentRotation.z, maxRotation);
        currentRotation.x = transform.localRotation.eulerAngles.x;
        currentRotation.y = transform.localRotation.eulerAngles.y;
        transform.localRotation = Quaternion.Euler (currentRotation);
    }

    float clampRotation(float x, float max){
        
        if (x < max || x > 360-max) return x;
        else if (x > 180) return 360 - max;
        else return max;
    }

    void Shoot(){

        Instantiate(laserPrefab, shootPointLeft.position, shootPointLeft.rotation);
        Instantiate(laserPrefab, shootPointRight.position, shootPointRight.rotation);

    }

    void OnTriggerEnter(Collider other){

        if (other.gameObject.tag == "Terrain"){
            ResetPosition();
            modifyHealth(-5);    
        }
    }

    void ResetPosition(){
        transform.position = resetPoint.position;
    }

    void modifyHealth(float change){
        currentHealth += change;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        HealthBarController h = HealthBar.GetComponent<HealthBarController>();
        h.updateSlider(currentHealth/maxHealth);
    }
}

