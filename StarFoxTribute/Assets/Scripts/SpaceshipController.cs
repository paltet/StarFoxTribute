using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

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
    
    public AudioClip laserShot;

    public float maxHealth = 100;
    public float currentHealth;

    public GameObject HealthBar;

    float originalCartSpeed;
    bool alive = true;

    public float velocityPercentage = 0.2f; //we set speed to 1 and then velocityPercentage*originalCartSpeed
    public float accelerationSteps = 13f; //hence during 1.3 seconds


    void Start(){
        currentHealth = maxHealth;
        originalCartSpeed = transform.parent.GetComponent<CinemachineDollyCart>().m_Speed;
    }

    void Update(){
        if (Input.GetMouseButtonDown(0)){
            Shoot();
        }
        if (!alive) transform.localScale /= 1.01f;

        if (currentHealth <= 0) StartCoroutine(Die());
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
        transform.Find("AudioSource").gameObject.GetComponent<AudioSource>().PlayOneShot(laserShot,1f);
    }

    void Hit(){
        ParticleSystem exp = transform.Find("FlashHit").gameObject.GetComponent<ParticleSystem>();
        exp.Play();
    }

    void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "Terrain"){
            modifyHealth(-5);
            Hit();
            ResetPosition();
            Brake();  
        } else if (other.gameObject.tag == "Laser"){
            modifyHealth(-2);
            Hit();
            Destroy(other.gameObject);
        } else if (other.gameObject.tag == "Asteroid"){
            modifyHealth(-5);
            Hit();
            Brake();
        }
    }

    void ResetPosition(){
        transform.position = resetPoint.position;
    }

    void modifyHealth(float change){

        if (!Camera.main.GetComponent<SceneController>().godMode){
            currentHealth += change;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

            HealthBarController h = HealthBar.GetComponent<HealthBarController>();
            h.updateSlider(currentHealth/maxHealth);
        }
    }

    void Brake() {
        if (!Camera.main.GetComponent<SceneController>().godMode) {
            transform.parent.GetComponent<CinemachineDollyCart>().m_Speed = 1;
            InvokeRepeating("Accelerate",0.0f,0.1f);
        }
    }

    void Accelerate() {
        float accelerateRate = Mathf.Pow((1-velocityPercentage)*(originalCartSpeed-1), 1/accelerationSteps);
        if(transform.parent.GetComponent<CinemachineDollyCart>().m_Speed == 1) {
            transform.parent.GetComponent<CinemachineDollyCart>().m_Speed += velocityPercentage*(originalCartSpeed-1);
        }
        float cartSpeed = transform.parent.GetComponent<CinemachineDollyCart>().m_Speed;
        float newSpeed = (cartSpeed - velocityPercentage*originalCartSpeed)*accelerateRate + velocityPercentage*originalCartSpeed;
        if(newSpeed<=originalCartSpeed) {
            transform.parent.GetComponent<CinemachineDollyCart>().m_Speed=newSpeed;
        }
        else {
            transform.parent.GetComponent<CinemachineDollyCart>().m_Speed=originalCartSpeed;
            CancelInvoke("Accelerate");
        }        
    }
    

    IEnumerator Die(){
        ParticleSystem ps = transform.Find("Explosion").GetComponent<ParticleSystem>();
        ps.Play();
        yield return new WaitForSeconds(ps.main.duration);
        Camera.main.GetComponent<SceneController>().DieScene();
        //Destroy(gameObject);
    }
}
