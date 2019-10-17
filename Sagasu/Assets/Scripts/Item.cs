using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;
    public enum PartsCategory
    {
        hand,
        foot,
        wing,
        head
    }

    public PartsCategory partsCategory;

    private float startPosY;
    [SerializeField] private float shakeSpeed;
    [SerializeField] private float shakeWidth;
    private int direction;
    // Start is called before the first frame update
    void Start()
    {
        startPosY = sprite.transform.localPosition.y;
        direction = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(direction == 1)
        {
            sprite.transform.localPosition += new Vector3(0, shakeSpeed * Time.deltaTime, 0);
            if(sprite.transform.localPosition.y >= startPosY + shakeWidth)
            {
                direction = -1;
            }
        }
        else if(direction == -1)
        {
            sprite.transform.localPosition -= new Vector3(0, shakeSpeed * Time.deltaTime, 0);
            if (sprite.transform.localPosition.y <= startPosY - shakeWidth)
            {
                direction = 1;
            }
        }
    }
}
