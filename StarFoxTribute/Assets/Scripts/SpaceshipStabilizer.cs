using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipStabilizer : MonoBehaviour
{
    public float stability = 0.3f;
    public float speed = 2f;

    void Start() {
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        Vector3 predicted = Quaternion.AngleAxis(
            rigidbody.angularVelocity.magnitude * Mathf.Rad2Deg * stability / speed,
            rigidbody.angularVelocity
        ) * transform.up;

        Vector3 torque = Vector3.Cross(predicted, Vector3.up);
        rigidbody.AddTorque(torque);
    }
}
