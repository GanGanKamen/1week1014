using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyes : MonoBehaviour
{
    [SerializeField] private Transform mask;
    [SerializeField] private float startScale;
    [SerializeField] private float overScale;
    [SerializeField] private float speed;
    private bool flash = false;
    private float nowScale;
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
    }

    public void Flash()
    {
        if (nowScale == overScale) return;
        flash = true;
    }
}
