using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    Camera cam;
    Shop sh;
    public Score sC;
    

    public GameObject pauseMenu;

    public float speed;
    public float damageMultiplier=1;
    public float camSensitivity;


    public bool isPaused = false;

    //Mouse Stuff
    float rotX;
    float rotY;

    void Start()
    {
        sh = GameObject.Find("Shop").GetComponent<Shop>();
        //Hide Cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        //References
        rb = GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>();
        ScoreData sD = ScoreSaver.LoadScore();
        sC.highScore = sD.highScore;


    }

    
    void Update()
    {
        if (isPaused == false&&sh.shopOpen==false)
        {
            Movement();
            MouseMovement();
        }



        if (Input.GetKeyDown(KeyCode.P))
        {
            isPaused = !isPaused;
            if (isPaused == true)
            {
                pauseMenu.SetActive(true);
                Time.timeScale = 0;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                pauseMenu.SetActive(false);
                Time.timeScale = 1;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    void Movement()
    {
        Vector3 dir= transform.right*Input.GetAxisRaw("Horizontal")+transform.forward*Input.GetAxisRaw("Vertical");
        dir.Normalize();
        Vector3 dirSpeed = dir * speed * Time.deltaTime;
        rb.velocity = dirSpeed;      
        
    }

    void MouseMovement()
    {
        rotX += Input.GetAxisRaw("Mouse X") * camSensitivity;
        rotY += Input.GetAxisRaw("Mouse Y") * camSensitivity;
        rotY=Mathf.Clamp(rotY, -90, 90);
        transform.localEulerAngles = new Vector3(0, rotX, 0);
        cam.transform.localEulerAngles = new Vector3(-rotY, 0, 0);
 

    }

    public void restartL()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
}
