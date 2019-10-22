using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    [SerializeField] private OPEyes eyes;
    [SerializeField] private Animator characterAnim;
    private AnimatorStateInfo stateInfo;

    [SerializeField] private AudioSource enviomentSource;
    [SerializeField] private AudioSource BGM;
    // Start is called before the first frame update
    void Start()
    {
        BGM.Play();
        enviomentSource.Play();
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
        while (!stateInfo.IsName("PlayerOp_BreakUp"))
        {
            yield return null;
        }
        characterAnim.GetComponent<AudioSource>().Play();
        while (!stateInfo.IsName("Over"))
        {
            yield return null;
        }
        BGM.Stop();
        eyes.Dark();
        while (eyes.getDark)
        {
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(name);
    }
}
