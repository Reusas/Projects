using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject icicle;
    public GameObject coin;
    public int rot;

    public int upDownModifier;
    public int sizeMod;

    public bool coinSpawn = false;


    void Start()
    {
        if (coinSpawn == false)
        {
            StartCoroutine(spawnIsicle());
        }
        else
        {
            StartCoroutine(spawnCoin());
        }
    }



    IEnumerator spawnIsicle()
    {

        yield return new WaitForSeconds(1f);
        float x = Random.Range(0f, 1.5f) *upDownModifier;
        GameObject ic = Instantiate(icicle, transform.position, transform.rotation);
        ic.transform.position = new Vector2(ic.transform.position.x, ic.transform.position.y + x);
        ic.transform.rotation = new Quaternion(0, 0, rot, 0);
        ic.transform.localScale += new Vector3(0, x*sizeMod,0);
        Destroy(ic, 2f);
        StartCoroutine(spawnIsicle());
    }

    IEnumerator spawnCoin()
    {

        yield return new WaitForSeconds(1f);
        int coinChance = Random.Range(0, 101);
        if (coinChance > 90)
        {
            Instantiate(coin, transform.position, transform.rotation);
        }
        StartCoroutine(spawnCoin());
    }
}
