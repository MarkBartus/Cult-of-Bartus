using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sounds : MonoBehaviour
{
    AudioManager audioManager;
    public AudioSource footstepsSound;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.FindWithTag("Audio").GetComponent<AudioManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S))
        {
            footstepsSound.enabled = true;

        }
        else
        {
            footstepsSound.enabled = false;
        }
    }


}