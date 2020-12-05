using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Helicopter : MonoBehaviour
{
    public float health = 1000;
    public float maxHealth = 1000;
    AudioSource aS;
    public AudioClip[] clips;
    public Slider healthSlider;
    public GameObject lossScreen;
    public Score sC;
    PlayerController pC;

    private void Start()
    {
        aS = GetComponent<AudioSource>();
        pC = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        
    }


    public void takeDamage(int dmg)
    {
        int x = Random.Range(0, clips.Length);
        aS.PlayOneShot(clips[x]);
        health -= dmg;
        healthSlider.value = health;
        if (health <= 0)
        {
            lossScreen.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            pC.isPaused = true;
            sC.updateScore();
            
        }
    }
}
