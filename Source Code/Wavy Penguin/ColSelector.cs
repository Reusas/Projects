using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColSelector : MonoBehaviour
{
    public static int colIndex = 0;
    public Color[] cols;
    public Image curCol;
    public TrailRenderer tR;

    private void Start()
    {
        curCol.color = cols[colIndex];
        tR.startColor = cols[colIndex];
    }

    public void forwardPress()
    {
        colIndex++;
        if (colIndex > cols.Length - 1)
        {
            colIndex = 0;
        }

        curCol.color = cols[colIndex];
        tR.startColor = cols[colIndex];
    }

    public void backPress()
    {
        colIndex--;
        if (colIndex < 0)
        {
            colIndex = cols.Length - 1;
        }
        curCol.color = cols[colIndex];
        tR.startColor = cols[colIndex];

    }
}
