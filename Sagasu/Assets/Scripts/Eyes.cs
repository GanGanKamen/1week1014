using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyes : MonoBehaviour
{
    [SerializeField] private Transform mask;
    public float startScale;
    public float overScale;
    [SerializeField] private float speed;
    public bool flash = false;
    public bool getDark = false;
    public float nowScale;
    // Start is called before the first frame update
    void Start()
    {
        nowScale = startScale;
    }

    // Update is called once per frame
    void Update()
    {
        mask.localScale = new Vector3(nowScale, nowScale, 1);
        if(flash == true)
        {
            if(nowScale < overScale)
            {
                nowScale += Time.deltaTime * speed;
            }
            else
            {
                nowScale = overScale;
                flash = false;
            }
        }
        else if (getDark)
        {
            if (nowScale >startScale)
            {
                nowScale -= Time.deltaTime * speed *2;
            }
            else
            {
                nowScale = startScale;
                getDark = false;
            }
        }
    }

    public void Flash()
    {
        if (nowScale == overScale) return;
        flash = true;
    }

    public void Dark()
    {
        if (nowScale == startScale) return;
        getDark = true;
    }
}
