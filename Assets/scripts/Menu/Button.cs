using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    
    AudioManager audioManager;
    
     
    private void Awake()
    {
        audioManager = GameObject.FindWithTag("Audio").GetComponent<AudioManager>();
      
    }
    public void Update()
    {
        

    }
    public void button()
    {
        audioManager.PlaySFX(audioManager.Button);

    }
    public void bell()
    {
        audioManager.PlaySFX(audioManager.playS);
    }



}
