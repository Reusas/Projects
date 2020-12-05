using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LScript : MonoBehaviour
{
    Text txt;
    public int speed;
    void Start()
    {
        txt = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        txt.fontSize += Mathf.RoundToInt(speed * Time.deltaTime);

        if (txt.fontSize > 300)
        {
            Destroy(this.gameObject);
        }
    }
}
