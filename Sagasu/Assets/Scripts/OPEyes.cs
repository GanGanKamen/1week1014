using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OPEyes : MonoBehaviour
{
    [SerializeField] private Transform mask;
    public float startScale;
    public float overScale;
    [SerializeField] private float speed;
    public float nowScale;
    public bool getDark = false;
    // Start is called before the first frame update
    private void Awake()
    {
        nowScale = startScale;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mask.localScale = new Vector3(nowScale, nowScale, 1);
        if (getDark)
        {
            if (nowScale > overScale)
            {
                nowScale -= Time.deltaTime * speed;
            }
            else
            {
                nowScale = overScale;
                getDark = false;
            }
        }
    }

    public void Dark()
    {
        if (nowScale == overScale) return;
        getDark = true;
    }
}
