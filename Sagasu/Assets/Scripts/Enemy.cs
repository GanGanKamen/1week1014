using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject body;
    [SerializeField] private float speed;
    public enum Pattern
    {
        Patrol,
        Stop,
        GameOver
    }

    public Pattern pattern = Pattern.Patrol;
    [SerializeField] private Vector2 twoPoint;
    private float posX;
    private int direction;

    // Start is called before the first frame update
    void Start()
    {
        posX = transform.position.x;
        direction = 1;

        SoundManager.PlayBGM(GetComponent<AudioSource>());
    }

    // Update is called once per frame
    void Update()
    {
        
        switch (pattern)
        {
            case Pattern.Patrol:
                Patrol();
                break;
        }
    }

    private void Patrol()
    {
        if(direction == 1)
        {
            if(posX < twoPoint.y)
            {
                posX += speed * Time.deltaTime;
            }
            else
            {
                posX = twoPoint.y;
                direction = -1;
            }
        }
        else if(direction == -1)
        {
            if (posX > twoPoint.x)
            {
                posX -= speed * Time.deltaTime;
            }
            else
            {
                posX = twoPoint.x;
                direction = 1;
            }
        }
        if (transform.position.x != posX)
        {
            if (transform.position.x > posX)
            {
                body.transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else
            {
                body.transform.eulerAngles = new Vector3(0, 0, 0);
            }
            transform.position = new Vector3(posX, transform.position.y, 0);
        }
    }
}
