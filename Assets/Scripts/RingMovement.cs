using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingMovement : MonoBehaviour
{

    Transform mytransform;
    public float speed = 0.5f;
    public float Rspeed = 50f;

    // Start is called before the first frame update
    void Start()
    {
        mytransform = this.transform;    
    }

    // Update is called once per frame
    void Update()
    {
        mytransform.position = new Vector3(mytransform.position.x , mytransform.position.y +(speed * Time.deltaTime), mytransform.position.z);
        if (mytransform.position.y > 0.8f) speed *= -1;
        if (mytransform.position.y < 0.3f) speed *= -1;
        mytransform.Rotate(0, Rspeed* Time.deltaTime, 0);
    }
}
