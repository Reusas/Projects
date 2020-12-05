using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MysteryBox : MonoBehaviour
{
    public GameObject[] weapons;
    GameObject wep;
    public Vector3 weaponRotations;
    public Transform weaponHolder;
    public Transform currentWep;
    DisableText dT;
    WeaponSwitcher wS;
    int x;

    private void Start()
    {
        weaponHolder = GameObject.Find("WeaponHolder").GetComponent<Transform>();
        wS = weaponHolder.GetComponent<WeaponSwitcher>();
        currentWep = weaponHolder.GetChild(wS.currentWeapIndex);
        dT = GameObject.Find("WeaponTextFam").GetComponent<Text>().GetComponent<DisableText>();

    }

    private void Update()
    {
        currentWep = weaponHolder.GetChild(wS.currentWeapIndex);
        if (transform.position.y <= -1.5f)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.isKinematic = true;
            transform.position = new Vector3(transform.position.x, -1.5f, transform.position.z);
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.transform.tag == "Player"&&currentWep.GetComponent<Weapon>().canShoot==true && currentWep.GetComponent<Weapon>().isAim==false)
        {

            if (wS.wepIndex < 7)
            {

                x= Random.Range(0, weapons.Length);
                if (x == 0 && wS.hasSG == true)
                {
                    wS.weps[0].clips = wS.weps[0].maxClips;
                    wS.weps[0].updateClips();
                    dT.showText(2, "Shotgun ammo refilled!");
                }
                else if (x == 1 && wS.hasUzi == true)
                {
                    wS.weps[1].clips = wS.weps[1].maxClips;
                    wS.weps[1].updateClips();
                    dT.showText(2, "Uzi ammo refilled!");
                }
                else if (x == 2 && wS.hasM4 == true)
                {
                    wS.weps[2].clips = wS.weps[2].maxClips;
                    wS.weps[2].updateClips();
                    dT.showText(2, "M4 ammo refilled!");
                }
                else if (x == 3 && wS.hasM249 == true)
                {
                    wS.weps[3].clips = wS.weps[3].maxClips;
                    wS.weps[3].updateClips();
                    dT.showText(2, "M249 ammo refilled!");
                }
                else if (x == 4 && wS.hasSniper == true)
                {
                    wS.weps[4].clips = wS.weps[4].maxClips;
                    wS.weps[4].updateClips();
                    dT.showText(2, "Sniper ammo refilled!");
                }
                else if (x == 5 && wS.hasSKS == true)
                {
                    wS.weps[5].clips = wS.weps[5].maxClips;
                    wS.weps[5].updateClips();
                    dT.showText(2, "Rifle ammo refilled!");
                }
                else if (x == 6 && wS.hasM60 == true)
                {
                    wS.weps[6].clips = wS.weps[6].maxClips;
                    wS.weps[6].updateClips();
                    dT.showText(2, "MG ammo refilled!");
                }
                else if (x == 7 && wS.hasM60 == true)
                {
                    wS.weps[7].clips = wS.weps[7].maxClips;
                    wS.weps[7].updateClips();
                    dT.showText(2, "SMG ammo refilled!");
                }
                else
                {
                    currentWep.GetComponent<Weapon>().clearCanvas();
                    wS.disablePrevWep();
                    wep = Instantiate(weapons[x], weapons[x].transform.position, weapons[x].transform.rotation);
                    wep.transform.SetParent(weaponHolder);
                    wep.transform.localRotation = Quaternion.Euler(0, -26.3f, 0);
                    wS.addWeapon();
                    setWeapons();
                }



            }
            Destroy(transform.gameObject);


        }
    }



    void setWeapons()
    {
        if (x == 0)
        {
            wS.hasSG = true;
            wS.weps[0] = wep.GetComponent<Weapon>();
            dT.showText(2, "You got the Shotgun!");
        }
        if (x == 1)
        {
            wS.hasUzi = true;
            wS.weps[1] = wep.GetComponent<Weapon>();
            dT.showText(2, "You got the Uzis!");
        }
        if (x == 2)
        {
            wS.hasM4 = true;
            wS.weps[2] = wep.GetComponent<Weapon>();
            dT.showText(2, "You got the M4!");
        }
        if (x == 3)
        {
            wS.hasM249 = true;
            wS.weps[3] = wep.GetComponent<Weapon>();
            dT.showText(2, "You got the M249!");
        }
        if (x == 4)
        {
            wS.hasSniper = true;
            wS.weps[4] = wep.GetComponent<Weapon>();
            dT.showText(2, "You got the Sniper Rifle!");
        }
        if (x == 5)
        {
            wS.hasSKS = true;
            wS.weps[5] = wep.GetComponent<Weapon>();
            dT.showText(2, "You got the Rifle!");
        }
        if (x == 6)
        {
            wS.hasM60 = true;
            wS.weps[6] = wep.GetComponent<Weapon>();
            dT.showText(2, "You got the Machine gun!");
        }
        if (x == 7)
        {
            wS.hasSMG = true;
            wS.weps[7] = wep.GetComponent<Weapon>();
            dT.showText(2, "You got the SMG!");
        }
    }



}
