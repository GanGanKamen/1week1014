using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator animator;

    [SerializeField] private Door nextDoor;
    private AnimatorStateInfo stateInfo;
    // Start is called before the first frame update

    void Start()
    {

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        stateInfo = animator.GetCurrentAnimatorStateInfo(0);
    }

    public void UseDoor(Character character)
    {
        if (SystemCtrl.canCtrl == false) return;
        StartCoroutine(PlayerTranlate(character));
    }

    private IEnumerator PlayerTranlate(Character character)
    {
        Debug.Log("Translate");
        SystemCtrl.canCtrl = false;
        animator.SetBool("Open", true);
        animator.SetBool("Close", false);
        nextDoor.animator.SetBool("Open", true);
        nextDoor.animator.SetBool("Close", false);
        GameObject fadeObj = Instantiate(SystemCtrl.fadeCanvas);
        FadeCanvas fade = fadeObj.GetComponent<FadeCanvas>();
        fade.alpha = 0; fade.fadeSwitch = 2;
        while (fade.fadeSwitch != 0)
        {
            yield return null;
        }
        character.transform.position = nextDoor.transform.position;
        character.body.SetActive(false);
        yield return new WaitForSeconds(1f);
        
        fade.fadeSwitch = 2;
        while (fade.fadeSwitch != 0)
        {
            yield return null;
        }
        Destroy(fadeObj);
        animator.SetBool("Open", false);
        animator.SetBool("Close", true);
        nextDoor.animator.SetBool("Open", false);
        nextDoor.animator.SetBool("Close", true);
        character.body.SetActive(true);
        character.canJump = true;
        while (!stateInfo.IsName("Door_Idle"))
        {
            yield return null;
        }

        SystemCtrl.canCtrl = true;
        character.onDoor = nextDoor;
        yield break;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Character>().onDoor = this;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Character>().onDoor = null;
        }
    }
}
