using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Camera cam;
    public bool right = false;
    public int speed;
    public float colOffset = .5f;
    public int zoomOutLimit = 6;
    public PlayerController p1;
    public PlayerController p2;

    public Transform center;

    float needToMove;
    float moveLoc;
    float t;
    public bool expand = false;
    public bool impand = false;

    private void Start()
    {
        StartCoroutine(findPlayers());
    }



    private void Update()
    {
        if (p1 && p2)
        {
            PlayerCameraMovement();
        }


        if (expand == true)
        {

            if (cam.orthographicSize < moveLoc)
            {
                needToMove = cam.orthographicSize += .01f;
                cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, needToMove, t);
                t += Time.time;
            }
            else
            {
                expand = false;
                
            }
            
            
        }

        if (impand == true)
        {

            if (cam.orthographicSize > moveLoc)
            {
                needToMove = cam.orthographicSize -= .01f;
                cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, needToMove, t);
                t += Time.time;
            }
            else
            {
                impand = false;

            }


        }


    }

    void Expand()
    {
        if (cam.orthographicSize < 6)
        {
            moveLoc = cam.orthographicSize + .45f;
            expand = true;
        }
    }

    void Impand()
    {
        if (cam.orthographicSize > 2.5)
        {
            moveLoc = cam.orthographicSize - .25f;
            impand = true;
        }
    }

    void PlayerCameraMovement()
    {

        float dist = Vector2.Distance(new Vector2(p1.transform.position.x, p1.transform.position.y), new Vector2(center.transform.position.x, center.transform.position.y));
        float dist2 = Vector2.Distance(new Vector2(p2.transform.position.x, p2.transform.position.y), new Vector2(center.transform.position.x, center.transform.position.y));
        bool ex = true;
        if (dist > 3 && cam.orthographicSize < 3 && ex == true)
        {
            Expand();
            ex = false;
        }

        if (dist > 4  && cam.orthographicSize < 4 && ex == true)
        {
            Expand();
            ex = false;
        }


        if (dist > 5  && cam.orthographicSize < 5 && ex == true)
        {
            Expand();
            ex = false;
        }

        //p2
        if (dist2 > 3 && cam.orthographicSize < 3 && ex == true)
        {
            Expand();
            ex = false;
        }

        if (dist2 > 4 && cam.orthographicSize < 4 && ex == true)
        {
            Expand();
            ex = false;
        }


        if (dist2 > 5 && cam.orthographicSize < 5 && ex == true)
        {
            Expand();
            ex = false;
        }

        if (dist < 3 && dist2 < 3&& cam.orthographicSize > 3 && ex == true)
        {
            Impand();
            ex = false;
        }

        if (dist < 4 && dist2 < 4 && cam.orthographicSize > 4 && ex == true)
        {
            Impand();
            ex = false;
        }

        if (dist < 5 && dist2 < 5 && cam.orthographicSize > 5 && ex == true)
        {
            Impand();
            ex = false;
        }
    }



    IEnumerator findPlayers()
    {
        yield return new WaitForSeconds(.2f);
        p1 = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        p2 = GameObject.FindWithTag("Player2").GetComponent<PlayerController>();
    }
    




}
