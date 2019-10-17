using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject body;
    [SerializeField] private GameObject hands;
    [SerializeField] private GameObject foots;
    [SerializeField] private GameObject wing;
    [SerializeField] private GameObject head;
    [SerializeField] private Eyes eyes;
    public bool hasHands = false;
    public bool hasFoots = false;
    public bool hasWing = false;
    public bool hasHead = false;

    private float prePosX;
    private bool direction;

    public Door onDoor;

    [SerializeField] private bool canJump;
    [SerializeField] private float jumpPower;

    public float hp;
    public float partHp;

    [SerializeField] private float hpDecrease;
    [SerializeField] private HpCtrl hpCtrl;

    public bool flying = false;

    [SerializeField] private Collider2D[] colliders;

    public bool muteki = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        prePosX = transform.position.x;
        direction = true;
        partHp = hp;
        canJump = false;
    }

    // Update is called once per frame
    void Update()
    {
        Direction();
        PartsDocking();
        if(flying == true) Fly();
    }

    private void HpDecrease()
    {
        if (SystemCtrl.canCtrl == false || hp <= 0 || muteki == true) return;
        hp -= hpDecrease * Time.deltaTime;
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
            HpDecrease();
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
        if(hasHead == true && head.activeSelf == false)
        {
            head.SetActive(true);
        }
    }

    private IEnumerator Damaged(Enemy enemy)
    {
        if (enemy.pattern != Enemy.Pattern.Patrol) yield break;
        Debug.Log("Hit");
        enemy.pattern = Enemy.Pattern.Stop;
        SystemCtrl.canCtrl = false;
        hp -= 10;
        if(enemy.transform.position.x > transform.position.x)
        {
            rb.AddForce(new Vector2(-jumpPower/2, jumpPower/5), ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(new Vector2(jumpPower / 2, jumpPower / 5), ForceMode2D.Impulse);
        }

        yield return new WaitForSeconds(3f);
        SystemCtrl.canCtrl = true;
        yield return new WaitForSeconds(1f);
        enemy.pattern = Enemy.Pattern.Patrol;
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

    public void Jump()
    {
        if (hasFoots == false||canJump == false) return;
        Debug.Log("Jump");
        rb.AddForce(new Vector2(0, jumpPower),ForceMode2D.Impulse);
    }

    private void Fly()
    {
        if (rb.isKinematic == false) rb.isKinematic = true;
        if(SystemCtrl.canCtrl == true) SystemCtrl.canCtrl = false;
        for (int i = 0; i < colliders.Length; i++)
        {
            if(colliders[i].isTrigger == false)colliders[i].isTrigger = true;
        }
        transform.Translate(0, moveSpeed * 2 * Time.deltaTime, 0);
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
                        hasFoots = true;
                    }
                    break;
                case Item.PartsCategory.wing:
                    if(hasWing == false)
                    {
                        hasWing = true;
                    }
                    break;
                case Item.PartsCategory.head:
                    if(hasHead == false)
                    {
                        hasHead = true;
                        eyes.Flash();
                    }
                    break;
            }
            hpCtrl.HpPlus();
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Jump"))
        {
            canJump = true;
            Debug.Log("jumpEnter");
        }

        if (collision.CompareTag("Wind") && hasWing == true && flying == false && SystemCtrl.canCtrl == true)
        {
            flying = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Jump"))
        {
            canJump = false;
            Debug.Log("jumpExit");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && SystemCtrl.canCtrl == true)
        {
            if (muteki == true) return;
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            StartCoroutine(Damaged(enemy));
        }
    }
}
