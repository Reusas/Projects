using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public Transform[] currentWeapons;
    Transform wH;
    Weapon wep;
    public Weapon[] weps;

    public int wepIndex = 0;
    public int currentWeapIndex;

    public bool hasPistol = true;
    public bool hasUzi = false;
    public bool hasSG = false;
    public bool hasM4 = false;
    public bool hasM249 = false;
    public bool hasSniper = false;
    public bool hasSKS = false;
    public bool hasM60 = false;
    public bool hasSMG = false;


    void Start()
    {
        wH = GetComponent<Transform>();
        currentWeapons[wepIndex] = wH.GetChild(0);
        currentWeapIndex = 0;
        wepIndex++;
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Q)&&currentWeapIndex>0)
        {
            wep= currentWeapons[currentWeapIndex].GetComponent<Weapon>();
            if (wep.isAim == false && wep.canSwitch == true&&wep.canShoot==true)
            {
                wep.canSwitch = false;
                wep.clearCanvas();
                wep.anim.SetBool("Aim", false);
                wep.canShoot = false;
                currentWeapons[currentWeapIndex].gameObject.SetActive(false);
                currentWeapIndex--;
                currentWeapons[currentWeapIndex].gameObject.SetActive(true);
                wep = currentWeapons[currentWeapIndex].GetComponent<Weapon>();
                wep.drawCanvas();
                wep.updateClips();
                wep.anim.SetTrigger("TakeOut");
            }

        }
        if (Input.GetKeyDown(KeyCode.E)&&currentWeapIndex<wepIndex-1)
        {
            wep = currentWeapons[currentWeapIndex].GetComponent<Weapon>();
            if (wep.isAim == false&&wep.canSwitch==true && wep.canShoot == true)
            {
                wep.canSwitch = false;
                wep.clearCanvas();
                wep.anim.SetBool("Aim", false);
                wep.canShoot = false;
                currentWeapons[currentWeapIndex].gameObject.SetActive(false);
                currentWeapIndex++;
                currentWeapons[currentWeapIndex].gameObject.SetActive(true);
                wep = currentWeapons[currentWeapIndex].GetComponent<Weapon>();
                wep.drawCanvas();
                wep.updateClips();
                wep.anim.SetTrigger("TakeOut");
            }


        }

    }

    public void disablePrevWep()
    {
        currentWeapons[currentWeapIndex].gameObject.SetActive(false);
        wep = currentWeapons[currentWeapIndex].GetComponent<Weapon>();
        wep.canSwitch = false;
        wep.clearCanvas();
        wep.anim.SetBool("Aim", false);
        wep.canShoot = false;
    }

    public void addWeapon()
    {
        if (wepIndex < weps.Length-1)
        {
            currentWeapons[wepIndex] = wH.GetChild(wepIndex);
            currentWeapIndex = wepIndex;
            wepIndex++;
        }

    }
}
