using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staircase : MonoBehaviour
{
    [SerializeField] private CircleCollider2D[] points;
    [SerializeField] private float moveSpeed;
    private Vector3[] pointPos;
    private Vector3 distination;

    private int step;
    private Character player;
    // Start is called before the first frame update
    void Start()
    {
        pointPos = new Vector3[2];
        for(int i = 0; i < points.Length; i++)
        {
            pointPos[i] = points[i].transform.position;
        }
        step = 0;
    }

    // Update is called once per frame
    void Update()
    {
        switch (step)
        {
            case 1:
                if(player.transform.position.x < distination.x && player.transform.position.y < distination.y)
                {
                    player.transform.Translate(moveSpeed * Time.deltaTime, moveSpeed * Time.deltaTime, 0);
                }
                else
                {
                    player.transform.position = distination;
                    step = 0;
                }
                break;
            case 2:
                if (player.transform.position.x > distination.x && player.transform.position.y < distination.y)
                {
                    player.transform.Translate(-moveSpeed * Time.deltaTime, moveSpeed * Time.deltaTime, 0);
                }
                else
                {
                    player.transform.position = distination;
                    step = 0;
                }
                break;
            case 11:
                if (player.transform.position.x < distination.x + 3f)
                {
                    player.transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
                }
                else
                {
                    step = 0;
                }
                break;
            case 12:
                if (player.transform.position.x > distination.x - 3f)
                {
                    player.transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
                }
                else
                {
                    step = 0;
                }
                break;
            case 3:
                if (player.transform.position.x < distination.x && player.transform.position.y > distination.y)
                {
                    player.transform.Translate(moveSpeed * Time.deltaTime, -moveSpeed * Time.deltaTime, 0);
                }
                else
                {
                    player.transform.position = distination;
                    step = 0;
                }
                break;
            case 4:
                if (player.transform.position.x > distination.x && player.transform.position.y > distination.y)
                {
                    player.transform.Translate(-moveSpeed * Time.deltaTime, -moveSpeed * Time.deltaTime, 0);
                }
                else
                {
                    player.transform.position = distination;
                    step = 0;
                }
                break;
            case 13:
                if (player.transform.position.x < distination.x + 3f)
                {
                    player.transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
                }
                else
                {
                    step = 0;
                }
                break;
            case 14:
                if (player.transform.position.x > distination.x - 3f)
                {
                    player.transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
                }
                else
                {
                    step = 0;
                }
                break;
        }
    }

    public void PlayerClamb(int num, Character character)
    {
        StartCoroutine(Clambing(num, character));
    }

    private IEnumerator Clambing(int num, Character character)
    {
        Debug.Log("Clamb");
        SystemCtrl.canCtrl = false;
        player = character;
        player.GetComponent<Rigidbody2D>().isKinematic = true;
        for (int i = 0; i < points.Length; i++)
        {
            points[i].enabled = false;
        }
        if (num == 0)
        {
            player.transform.position = pointPos[0];
            distination = pointPos[1];
            int preStep = 0;
            if (player.transform.position.x < distination.x)
            {
                step = 1;
            }
            else
            {
                step = 2;
            }
            preStep = step;
            while(step != 0)
            {
                yield return null;
            }
            step = preStep + 10;
            while (step != 0)
            {
                yield return null;
            }
            player.GetComponent<Rigidbody2D>().isKinematic = false;
            player = null;
            for (int i = 0; i < points.Length; i++)
            {
                points[i].enabled = true;
            }
            SystemCtrl.canCtrl = true;
            yield break;
        }
        else
        {
            player.transform.position = pointPos[1];
            distination = pointPos[0];
            int preStep = 0;
            if (player.transform.position.x < distination.x)
            {
                step = 3;
            }
            else
            {
                step = 4;
            }
            preStep = step;
            while (step != 0)
            {
                yield return null;
            }
            step = preStep + 10;
            while (step != 0)
            {
                yield return null;
            }
            player.GetComponent<Rigidbody2D>().isKinematic = false;
            player = null;
            for (int i = 0; i < points.Length; i++)
            {
                points[i].enabled = true;
            }
            SystemCtrl.canCtrl = true;
            yield break;
        }
    }
}
