using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour
{

    public bool rotate = false;
    AudioSource aS;
    public AudioClip[] clips;

    void Start()
    {
        aS = GetComponent<AudioSource>();
        int x = Random.Range(0, 2);
        aS.PlayOneShot(clips[x]);
        if (rotate == true)
        {
            transform.Rotate(0, 0, 90);
        }


    }

    private void Update()
    {
        if (!aS.isPlaying)
        {
            Destroy(gameObject);
        }
        
    }


}
