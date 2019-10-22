using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Text log;
    [SerializeField] private Character player;
    [SerializeField] private string nextSceneName;
    private SystemCtrl system;
    // Start is called before the first frame update
    void Start()
    {
        system = GameObject.FindGameObjectWithTag("System").GetComponent<SystemCtrl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && log.text != "Stage Clear")
        {
            StartCoroutine(GoToNextStage(nextSceneName));
        }
    }

    private IEnumerator GoToNextStage(string name)
    {
        if (log.text == "Stage Clear") yield break;
        log.text = "Stage Clear";
        player.flying = false;
        if (player.hasHead) player.eyes.Dark();
        system.BGM.Stop();
        SoundManager.PlayBGM(GetComponent<AudioSource>());
        yield return new WaitForSeconds(5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(name);
        yield break;
    }
}
