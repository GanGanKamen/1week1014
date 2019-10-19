using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float moveSpeed;
    public GameObject body;
    [SerializeField] private GameObject hands;
    [SerializeField] private GameObject foots;
    [SerializeField] private GameObject wing;
    [SerializeField] private GameObject head;
    public Eyes eyes;
    public bool hasHands = false;
    public bool hasFoots = false;
    public bool hasWing = false;
    public bool hasHead = false;

    private float prePosX;
    private bool direction;

    public Door onDoor;

    public bool canJump;
    [SerializeField] private float jumpPower;

    public float hp;
    public float partHp;

    [SerializeField] private float hpDecrease;
    [SerializeField] private HpCtrl hpCtrl;

    public bool flying = false;

    private CapsuleCollider2D capsuleCollider;
    [SerializeField] private Vector2 newCapsuleSize; //x:OffestY y:SizeY

    public bool muteki = false;

    private Animator animator;

    public AudioSource voiceAudio;
    public AudioSource seAudio;
    [SerializeField] private AudioClip[] seS;
    [SerializeField] private AudioClip[] voices;

    private AudioClip stepSE;

    [SerializeField] private GameObject frontBody,frontHead;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        prePosX = transform.position.x;
        direction = true;
        partHp = hp;
        canJump = false;

        capsuleCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();

        stepSE = seS[2];
    }

    // Update is called once per frame
    void Update()
    {
        Direction();
        PartsDocking();
        if (flying == true) Fly();
    }

    private void HpDecrease()
    {
        if (SystemCtrl.canCtrl == false || hp <= 0 || muteki == true) return;
        hp -= hpDecrease * Time.deltaTime;
    }

    private void Direction()
    {
        if (transform.position.x != prePosX)
        {
            if (SystemCtrl.canCtrl)
            {
                if (transform.position.x > prePosX)
                {
                    direction = true;
                    body.transform.eulerAngles = new Vector3(0, 0, 0);
                }
                else
                {
                    direction = false;
                    body.transform.eulerAngles = new Vector3(0, 180, 0);
                }
                HpDecrease();
            }

            prePosX = transform.position.x;
        }

        else
        {
            animator.SetBool("Walk", false);
        }
    }

    private void PartsDocking()
    {
        if (hasHands == true && hands.activeSelf == false)
        {
            hands.SetActive(true);
        }
        if (hasWing == true && wing.activeSelf == false)
        {
            wing.SetActive(true);
        }
        if (hasFoots == true && foots.activeSelf == false)
        {
            foots.SetActive(true);
            capsuleCollider.offset = new Vector2(capsuleCollider.offset.x, newCapsuleSize.x);
            capsuleCollider.size = new Vector2(capsuleCollider.size.x, newCapsuleSize.y);
            stepSE = seS[3];
        }
        if (hasHead == true && head.activeSelf == false)
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
        if (enemy.transform.position.x > transform.position.x)
        {
            rb.AddForce(new Vector2(-jumpPower / 2, jumpPower / 5), ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(new Vector2(jumpPower / 2, jumpPower / 5), ForceMode2D.Impulse);
        }
        SoundManager.PlaySEOneTime(voiceAudio, voices[0]);
        enemy.GetComponent<AudioSource>().Stop();
        animator.SetTrigger("Hit");
        yield return new WaitForSeconds(3f);
        SystemCtrl.canCtrl = true;
        yield return new WaitForSeconds(1f);
        if (enemy.pattern != Enemy.Pattern.GameOver)
        {
            enemy.pattern = Enemy.Pattern.Patrol;
            enemy.GetComponent<AudioSource>().Play();
        }

        yield break;
    }

    public void CharacterMove(float moveDirection)
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, 0, 0);
        if (canJump)
        {
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }
    }

    public void UseDoor()
    {
        if (hasHands == false) return;
        onDoor.UseDoor(this);
    }

    public void Jump()
    {
        if (hasFoots == false || canJump == false) return;
        Debug.Log("Jump");
        rb.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
        SoundManager.PlaySEOneTime(seAudio, seS[0]);
        animator.SetTrigger("Jump");
    }

    public void StepSound()
    {
        SoundManager.PlaySEOneTime(seAudio, stepSE);
    }

    public void Dead()
    {
        animator.SetTrigger("Dead");
    }

    private void Fly()
    {
        //if (rb.isKinematic == false) rb.isKinematic = true;
        if (SystemCtrl.canCtrl == true) SystemCtrl.canCtrl = false;
        capsuleCollider.isTrigger = true;
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
                    if (hasHands == false)
                    {
                        hasHands = true;
                    }
                    break;
                case Item.PartsCategory.foot:
                    if (hasFoots == false)
                    {
                        hasFoots = true;
                    }
                    break;
                case Item.PartsCategory.wing:
                    if (hasWing == false)
                    {
                        hasWing = true;
                    }
                    break;
                case Item.PartsCategory.head:
                    if (hasHead == false)
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
            Destroy(rb);
            body.SetActive(false);
            frontBody.SetActive(true);
            if (hasHead) frontHead.SetActive(true);
            SoundManager.PlaySELoop(seAudio, seS[1]);
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
