using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{
    private GameObject key = null;
    public GameObject keyanouncement = null;
    Color fadecolor;
    public float fadespeed = 0.01f;


    public GameObject WinLoseText = null;
    public GameObject Countdown = null;
    public GameObject Player = null;

    private void Start()
    {
        

        if (key == null && playermovement.keycollected == false)
        {
            
            key = GameObject.FindGameObjectWithTag("Key");
        }
        if(keyanouncement == null)
        {
            keyanouncement = GameObject.FindGameObjectWithTag("KeyText");
            fadecolor = keyanouncement.GetComponent<Text>().color;
            keyanouncement.SetActive(false);
        }
        WinLoseText.SetActive(false);

    }
    // Update is called once per frame
    void Update()
    {
        if(Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }
        

        if (key == null && playermovement.keycollected == false) {
            
            key = GameObject.FindGameObjectWithTag("Key");
        }
        
        if (playermovement.coinscollected == MazeRender.numofCoins && key != null)
        {
            key.SetActive(true);
            keyanouncement.SetActive(true);
            fadecolor = new Color(fadecolor.r, fadecolor.g, fadecolor.b, fadecolor.a -(Time.deltaTime*fadespeed));
            keyanouncement.GetComponent<Text>().color = fadecolor;
           
        }
        else if(key!= null)
        {
            key.SetActive(false);
        }
      
        if(playermovement.keycollected == true)
        {
            GameObject Spikes = GameObject.FindGameObjectWithTag("Spikes");

            Destroy(Spikes);
        }

        if(Countdown.GetComponent<Timer>().currentTime ==0)
        {
            WinLoseText.GetComponent<Text>().text = "YOU LOSE";
            WinLoseText.SetActive(true);
        }
        else if(Countdown.GetComponent<Timer>().currentTime > 0 && Player.GetComponent<playermovement>().exitpass == true && Player != null)
        {
            WinLoseText.SetActive(true);
        }


    }


}

