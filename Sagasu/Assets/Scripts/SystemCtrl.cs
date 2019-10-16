using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemCtrl : MonoBehaviour
{
    static public bool canCtrl;

    static public GameObject fadeCanvas;
    static public GameObject HpBar;
    // Start is called before the first frame update
    void Start()
    {
        canCtrl = true;

        fadeCanvas = Resources.Load<GameObject>("FadeCanvas");
        HpBar = Resources.Load<GameObject>("Hpbar");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
