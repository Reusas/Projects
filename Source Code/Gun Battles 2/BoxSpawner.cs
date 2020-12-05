using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    public GameObject[] item;
    public Transform[] positions=new Transform[5];
    public int minTime = 10;
    public int maxTime = 20;

    private void Start()
    {

        StartCoroutine(spawnBox());
    }


   public IEnumerator spawnBox()
    {
        int waitTime = Random.Range(minTime, maxTime);
        yield return new WaitForSeconds(waitTime);
        int spawnPick = Random.Range(0, positions.Length);
        int itemPick = Random.Range(0, item.Length);

        Instantiate(item[itemPick], positions[spawnPick].position, positions[spawnPick].rotation);

    }

    public void spawnTheBox()
    {
        StartCoroutine(spawnBox());
    }

 
}
