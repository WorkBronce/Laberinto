using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovement : MonoBehaviour
{
    public Rigidbody player;
    public float m_speed, rotatespeed;

    public static int coinscollected = 0;
    public static bool keycollected = false;
    public  bool exitpass = false;

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            player.velocity = transform.forward * m_speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            player.velocity = -transform.forward * m_speed * Time.deltaTime;
        }
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, -rotatespeed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, rotatespeed * Time.deltaTime, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Coin")
        {
            coinscollected++;
            Destroy(other.gameObject);
           // Debug.Log(coinscollected);
        }else if(other.tag == "Key")
        {
            keycollected = true;
            Destroy(other.gameObject);
        }else if(other.tag =="Exit")
        {
            exitpass = true;

        }

    }

}
