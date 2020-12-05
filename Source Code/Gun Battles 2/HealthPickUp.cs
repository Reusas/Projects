using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPickUp : MonoBehaviour
{
    Rigidbody2D rb;
    BoxSpawner bS;
    Text wepText;

    private void Start()
    {
        wepText = GameObject.Find("WeaponBoxText").GetComponent<Text>();
        rb = GetComponent<Rigidbody2D>();
        bS = GameObject.FindWithTag("BoxL").GetComponent<BoxSpawner>();

        Enemy enem = GameObject.Find("Enemy").GetComponent<Enemy>();
        enem.goToPackage = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Ground")
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;

        }
        if (collision.transform.name == "Player")
        {
            PlayerController pC = collision.transform.GetComponent<PlayerController>();
            pC.lives++;
            pC.updateHealthUI();
            wepText.text = "Extra Life";
            wepText.rectTransform.anchoredPosition = Camera.main.WorldToScreenPoint(transform.position);
            int z = Random.Range(-45, 45);
            wepText.rectTransform.localEulerAngles = new Vector3(0, 0, z);
            wepText.GetComponent<WeaponText>().lerp = true;
            bS.spawnTheBox();

            Enemy enem = GameObject.Find("Enemy").GetComponent<Enemy>();
            enem.goToPackage = false;

            Destroy(transform.gameObject);
        }
        else if (collision.transform.name == "Enemy")
        {
            Enemy eN= collision.transform.GetComponent<Enemy>();
            eN.lives++;
            eN.updateHealthUI();
            wepText.text = "Extra Life";
            wepText.rectTransform.anchoredPosition = Camera.main.WorldToScreenPoint(transform.position);
            int z = Random.Range(-45, 45);
            wepText.rectTransform.localEulerAngles = new Vector3(0, 0, z);
            wepText.GetComponent<WeaponText>().lerp = true;
            bS.spawnTheBox();

            Enemy enem = GameObject.Find("Enemy").GetComponent<Enemy>();
            enem.goToPackage = false;

            Destroy(transform.gameObject);
        }
    }
}
