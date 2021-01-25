using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
public class ItemSpawner : MonoBehaviour
{
    string[] items = new string[] { "Item", "Item2","Item3" };



    private void Awake()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Destroy(gameObject);
        }

        StartCoroutine(spawnItem());
    }

    IEnumerator spawnItem()
    {
        int x = Random.Range(10, 25);
        yield return new WaitForSeconds(x);

        int item = Random.Range(0, items.Length);

        Vector2 position = Random.insideUnitSphere * 7;

        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", items[item]), position, Quaternion.identity);

        StartCoroutine(spawnItem());


    }




}
