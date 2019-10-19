using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemCtrl : MonoBehaviour
{
    static public bool canCtrl;

    static public GameObject fadeCanvas;
    static public GameObject HpBar;

    [SerializeField] private Character player;
    [SerializeField] private UnityEngine.UI.Text log;

    [SerializeField] private AudioClip[] audios;
    // Start is called before the first frame update
    void Start()
    {
        canCtrl = true;

        fadeCanvas = Resources.Load<GameObject>("FadeCanvas");
        HpBar = Resources.Load<GameObject>("Hpbar");
    }

    // Update is called once per frame
    void Update()
    {
        GameOver();
    }

    private void GameOver()
    {
        if(player.hp <= 0 && canCtrl == true)
        {
            canCtrl = false;
            StartCoroutine(BackToTitle());
        }
    }

    private IEnumerator BackToTitle()
    {
        if (log.text == "Game Over") yield break;
        log.text = "Game Over";
        SoundManager.PlaySEOneTime(player.voiceAudio, audios[0]);
        player.Dead();
        canCtrl = false;
        foreach(GameObject enemyObj in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemyObj.GetComponent<Enemy>().pattern = Enemy.Pattern.GameOver;
        }
        yield return new WaitForSeconds(2f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
        yield break;
    }
}
