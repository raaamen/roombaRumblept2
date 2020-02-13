using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonMoveShake : MonoBehaviour
{
    Vector3 offset;
    float y_amplitude = 0.0f;
    float x_amplitude = 0.0f;
    Quaternion lastRotation;
    // Start is called before the first frame update
    void Start()
    {
        lastRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.rotation = lastRotation;
        float angle = Vector3.Angle(transform.forward,transform.parent.forward);
        //Vector3 eulerangs_before = transform.localEulerAngles;
        this.transform.rotation = Quaternion.RotateTowards(transform.rotation,transform.parent.rotation,Mathf.Abs(angle)*3*Time.deltaTime);
        //transform.localEulerAngles = new Vector3(eulerangs_before.x,transform.eulerAngles.y,eulerangs_before.z);
        lastRotation = this.transform.rotation;
    }
}
