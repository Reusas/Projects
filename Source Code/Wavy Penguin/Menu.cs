using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    AudioSource aS;
    void Start()
    {
        aS = GetComponent<AudioSource>();
        StartCoroutine(begin());
    }



    IEnumerator begin()
    {
        yield return new WaitForSeconds(2f);
        aS.Play();
        yield return new WaitForSeconds(aS.clip.length);
        SceneManager.LoadScene(1);
    }
}
