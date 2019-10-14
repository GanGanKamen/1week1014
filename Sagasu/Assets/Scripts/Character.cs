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
    public bool hasFoots = false;
    public bool hasWing = false;

    private float prePosX;
    private bool direction;

    public Door onDoor;
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
        PartsDocking();
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

    private void PartsDocking()
    {
        if(hasHands == true && hands.activeSelf == false)
        {
            hands.SetActive(true);
        }
        if(hasWing == true && wing.activeSelf == false)
        {
            wing.SetActive(true);
        }
        if(hasFoots == true && foots.activeSelf == false)
        {
            foots.SetActive(true);
        }
    }

    public void CharacterMove(float moveDirection)
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, 0, 0);
    }

    public void UseDoor()
    {
        if (hasHands == false) return;
        onDoor.UseDoor(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            Item newItem = collision.gameObject.GetComponent<Item>();
            switch (newItem.partsCategory)
            {
                case Item.PartsCategory.hand:
                    if(hasHands == false)
                    {
                        hasHands = true;
                    }
                    break;
                case Item.PartsCategory.foot:
                    if(hasFoots == false)
                    {
                        transform.Translate(0, 0.7f, 0);
                        hasFoots = true;
                    }
                    break;
                case Item.PartsCategory.wing:
                    if(hasWing == false)
                    {
                        hasWing = true;
                    }
                    break;
            }
            Destroy(collision.gameObject);
        }
    }
}
