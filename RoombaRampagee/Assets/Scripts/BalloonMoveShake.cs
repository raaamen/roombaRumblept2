using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonMoveShake : MonoBehaviour
{
    public GameObject parent;
    Vector3 offset;
    float y_amplitude = 0.0f;
    float x_amplitude = 0.0f;
    Quaternion targetRotation;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        targetRotation = parent.transform.rotation;
        offset = this.transform.position - parent.transform.position;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = parent.transform.TransformPoint(offset);
        float angle = Vector3.Angle(new Vector3(transform.forward.x,0,transform.forward.z),parent.transform.forward);
        Vector3 eulerangs_before = transform.localEulerAngles;
        this.transform.rotation = Quaternion.RotateTowards(transform.rotation,parent.transform.rotation,Mathf.Abs(angle)*3*Time.deltaTime);
        transform.localEulerAngles = new Vector3(eulerangs_before.x,transform.eulerAngles.y,eulerangs_before.z);
    }
}
