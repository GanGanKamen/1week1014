using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    [SerializeField] private OPEyes eyes;
    [SerializeField] private Animator characterAnim;
    private AnimatorStateInfo stateInfo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        stateInfo = characterAnim.GetCurrentAnimatorStateInfo(0);
    }

    public void NextScene(string name)
    {
        StartCoroutine(StartChangeScene(name));
    }

    private IEnumerator StartChangeScene(string name)
    {
        characterAnim.SetTrigger("Break");
        while (!stateInfo.IsName("Over"))
        {
            yield return null;
        }
        eyes.Dark();
        while (eyes.getDark)
        {
            yield return null;
        }
        SceneManager.LoadScene(name);
    }
}
