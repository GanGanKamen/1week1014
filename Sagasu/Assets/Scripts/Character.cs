using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject body;
    [SerializeField] private GameObject hands;
    [SerializeField] private GameObject foots;
    [SerializeField] private GameObject wing;

    public bool hasHands = false;
    public bool hansFoots = false;
    public bool hasWing = false;

    private float prePosX;
    private bool direction;
    // Start is called before the first frame update
    void Start()
    {
        prePosX = transform.position.x;
        direction = true;
    }

    // Update is called once per frame
    void Update()
    {
        Direction();
    }

    private void Direction()
    {
        if(transform.position.x != prePosX)
        {
            if(transform.position.x > prePosX)
            {
                direction = true;
                body.transform.eulerAngles = new Vector3(0,0,0);
            }
            else
            {
                direction = false;
                body.transform.eulerAngles = new Vector3(0, 180, 0);
            }
            prePosX = transform.position.x;
        }
    }

    public void CharacterMove(float moveDirection)
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, 0, 0);
    }
}
