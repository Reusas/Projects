using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RPG : MonoBehaviour {
    public int AmmoCount;
    private Text AmmoText;
    private string ammoText;
    private bool canSpawnDefaultGun = false;
    Rigidbody2D rocketRB;
    Canvas gameScreen;
    AudioSource ac;
    public float speed;
    bool canShoot;
    public float ReloadTime;
    public GameObject rocket;
    public Vector3 offset;
    public Vector3 offset2;
    public Transform player;
    int enemyJumpRandomFactor;


    void Start () {
        Transform launcher = GetComponent<Transform>();
        launcher.Rotate(0, 180, 0);
        player = GameObject.Find("Player").GetComponent<Transform>();
        gameScreen = GameObject.Find("Canvas").GetComponent<Canvas>();
        AmmoText = GameObject.Find("AmmoText").GetComponent<Text>();
        ammoText = AmmoCount.ToString();
        AmmoText.text = "Ammo: " + ammoText;
        Instantiate(rocket, launcher.position+offset, launcher.rotation, launcher);
        rocketRB = GetComponentInChildren<Rigidbody2D>();
        ac = GetComponent<AudioSource>();
        canShoot = true;
		
	}
	

	void Update () {
        ammoText = AmmoCount.ToString();
        AmmoText.text = "Ammo: " + ammoText;
        if (Input.GetMouseButtonDown(0)&&canShoot==true)
        {
            canShoot = false;
            rocketRB.isKinematic = false;
            rocketRB.transform.parent = gameScreen.transform;
            rocketRB.velocity = -transform.right * speed;
            Destroy(rocketRB.gameObject, 2f);
            ac.Play();
            StartCoroutine(Reload());
            GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
            Enemy en = enemy.GetComponent<Enemy>();
            enemyJumpRandomFactor = Random.Range(0, 2);
            AmmoCount--;
            if (enemyJumpRandomFactor == 1)
            {

                en.Jump();
            }

        }

        if (AmmoCount == 0)
        {
            StartCoroutine(waitForGun());
        }

    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(ReloadTime);
        Transform launcher = GetComponent<Transform>();
        if (player.transform.rotation.y == 0)
        {
            Instantiate(rocket, launcher.position + offset, launcher.rotation, launcher);
            rocketRB = GetComponentInChildren<Rigidbody2D>();
            canShoot = true;
        }
        if (player.transform.rotation.y == 1)
        {
            Instantiate(rocket, launcher.position - offset2, launcher.rotation, launcher);
            rocketRB = GetComponentInChildren<Rigidbody2D>();
            canShoot = true;
        }

    }

    IEnumerator waitForGun()
    {
        yield return new WaitForSeconds(.1f);
        canSpawnDefaultGun = true;
        GameObject ch = GameObject.Find("Chest");
        Chest chs = ch.GetComponent<Chest>();
        chs.GiveDefaultGun();



    }
}
