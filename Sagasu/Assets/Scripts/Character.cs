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
    // Start is called before the first frame update
    void Start()
    {
        prePosX = transform.position.x;
        direction = true;
        partHp = hp;
    }

    // Update is called once per frame
    void Update()
    {
        Direction();
        PartsDocking();
    }

    private void HpDecrease()
    {
        if (SystemCtrl.canCtrl == false || hp <= 0) return;
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
            GetComponent<Rigidbody2D>().AddForce(new Vector2(-jumpPower/2, jumpPower/2), ForceMode2D.Impulse);
        }
        else
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(jumpPower / 2, jumpPower / 2), ForceMode2D.Impulse);
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
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpPower),ForceMode2D.Impulse);
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
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Jump"))
        {
            canJump = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && SystemCtrl.canCtrl == true)
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            StartCoroutine(Damaged(enemy));
        }
    }
}
