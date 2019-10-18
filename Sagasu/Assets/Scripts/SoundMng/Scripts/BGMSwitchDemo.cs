using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMSwitchDemo : MonoBehaviour
{
    public AudioSource now, intro, loop; //現在再生中の音、イントロ、ループ部分
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        KeyCtrl();
    }

    private void KeyCtrl() //キー操作の関数を用意する
    {
        if (Input.GetKeyDown(KeyCode.Space)) //スペースキーが押された瞬間
        {
            SoundManager.SwitchBGM(now, intro, loop, 4f); //4秒間で現在のBGMがフェードアウトして、イントロから次のBGMに切り替える
        }
    }
}
