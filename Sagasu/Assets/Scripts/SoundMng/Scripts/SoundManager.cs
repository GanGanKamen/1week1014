using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static void IntroLoopPlay(AudioSource introSource,AudioSource loopSource) //イントロ付きループ再生
    {
        //イントロ部分のオーディオソードとループ部分のオーディオソースを用意する
        introSource.loop = false;
        introSource.Play();
        loopSource.loop = true;
        loopSource.PlayScheduled(AudioSettings.dspTime - 0.1f + introSource.clip.length);        
    }

    public static void IntroLoopPlay(AudioSource introSource, AudioSource loopSource,float OptionVolume) //ゲームオプションの音量調節
    {
        introSource.loop = false;
        introSource.Play();
        loopSource.loop = true;
        loopSource.PlayScheduled(AudioSettings.dspTime - 0.1f + introSource.clip.length);
    }

    public static void SwitchBGM(AudioSource preBGM, AudioSource newBgm, float time) //現在再生中のBGMをフェードアウトで新しいBGMに切り替える
    {
        GameObject fadeObj = new GameObject();
        fadeObj.AddComponent<BGMFade>();
        BGMFade bgmFade = fadeObj.GetComponent<BGMFade>();
        bgmFade.preBGM = preBGM;
        bgmFade.nextBGM = newBgm;
        bgmFade.fadeTime = time;
    }

    public static void SwitchBGM(AudioSource preBGM, AudioSource newBgm, float time,float OptionVolume) //ゲームオプションの音量調節
    {
        GameObject fadeObj = new GameObject();
        fadeObj.AddComponent<BGMFade>();
        BGMFade bgmFade = fadeObj.GetComponent<BGMFade>();
        bgmFade.preBGM = preBGM;
        bgmFade.nextBGM = newBgm;
        bgmFade.fadeTime = time;
        bgmFade.optionVolume = OptionVolume;
    }

    public static void SwitchBGM(AudioSource preBGM, AudioSource newBgmIntro, AudioSource newBGMLoop, float time) //フェードアウトからのイントロ付きループ再生
    {
        GameObject fadeObj = new GameObject();
        fadeObj.AddComponent<BGMFade>();
        BGMFade bgmFade = fadeObj.GetComponent<BGMFade>();
        bgmFade.preBGM = preBGM;
        bgmFade.introBGM = newBgmIntro;
        bgmFade.nextBGM = newBGMLoop;
        bgmFade.fadeTime = time;
    }

    public static void SwitchBGM(AudioSource preBGM, AudioSource newBgmIntro, AudioSource newBGMLoop, float time, float OptionVolume)
    {
        GameObject fadeObj = new GameObject();
        fadeObj.AddComponent<BGMFade>();
        BGMFade bgmFade = fadeObj.GetComponent<BGMFade>();
        bgmFade.preBGM = preBGM;
        bgmFade.introBGM = newBgmIntro;
        bgmFade.nextBGM = newBGMLoop;
        bgmFade.fadeTime = time;
        bgmFade.optionVolume = OptionVolume;
    }

    public static void PlayBGM(AudioSource audioSource)
    {
        audioSource.Play();
    }

    public static void PlayBGM(AudioSource audioSource, float OptionVolume) //ゲームオプションの音量調節に適応するBGM再生
    {
        audioSource.volume = OptionVolume;
        audioSource.Play();
    }

    public static void PlaySEOneTime(AudioSource audioSource,AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public static void PlaySEOneTime(AudioSource audioSource, AudioClip clip, float OptionVolume) //ゲームオプションの音量調節に適応するSE再生
    {
        audioSource.volume = OptionVolume;
        audioSource.PlayOneShot(clip);
    }
}
