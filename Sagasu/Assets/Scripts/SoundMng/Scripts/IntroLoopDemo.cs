using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroLoopDemo : MonoBehaviour
{
    public AudioSource intro, loop; //分割したイントロとループ部分のAudioSource
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        KeyCtrl();
    }

    private void KeyCtrl()//キー操作の関数を用意する
    {
        if (Input.GetKeyDown(KeyCode.Space))//スペースキーが押された瞬間
        {
            SoundManager.IntroLoopPlay(intro, loop); //イントロ付きのBGMを再生する
        }
    }
}
