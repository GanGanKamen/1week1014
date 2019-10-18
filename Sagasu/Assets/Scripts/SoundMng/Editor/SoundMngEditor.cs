using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SoundMngEditor : EditorWindow
{
    static SoundMngEditor soundEditor;
    public string category = "BGM";
    public Object[] source = new Object[20];
    private AudioClip[] audioClip = new AudioClip[20];

    [MenuItem("Window/ガンガン仮面/サウンドマネージャー")]
    
    static void Open()
    {
        if(soundEditor == null)
        {
            soundEditor = CreateInstance<SoundMngEditor>();
        }
        soundEditor.ShowUtility();
    }
    /*
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        SoundMngEditor window = (SoundMngEditor)EditorWindow.GetWindow(typeof(SoundMngEditor));
        window.Show();
    }*/

    private void OnGUI()
    {
        GUILayout.Label("サウンドマネージャー", EditorStyles.boldLabel);
        
        category = EditorGUILayout.TextField("Category", category);
        for(int i = 0; i < 20; i++)
        {
            source[i] = EditorGUILayout.ObjectField(source[i], typeof(AudioClip), true);
            audioClip[i] = (AudioClip)source[i];
        }

        
        if (GUI.Button(new Rect(100.0f, 70.0f + 20 * 18, 120.0f, 20.0f), "カテゴリー作成"))
        {
            CreatCategory();

        }

        if (GUI.Button(new Rect(60.0f, 100.0f + 20*18, 200.0f, 20.0f), "マネージャー作成"))
        {
            Creat();
        }

    }
    
    private void CreatCategory()
    {
        
        GameObject categoryObj;
        if (GameObject.Find(category) != null)
        {
            categoryObj = GameObject.Find(category);
        }
        else
        {
            categoryObj = new GameObject();
            categoryObj.transform.position = Vector3.zero;
            categoryObj.name = category;
        }
        for(int i = 0; i < 20; i++)
        {
            if(audioClip[i] != null)
            {
                GameObject gameObject = new GameObject();
                gameObject.name = audioClip[i].name;
                gameObject.transform.parent = categoryObj.transform;
                gameObject.transform.localPosition = Vector3.zero;
                gameObject.AddComponent<AudioSource>();
                AudioSource audioSource = gameObject.GetComponent<AudioSource>();
                audioSource.clip = audioClip[i];
                audioSource.playOnAwake = false;
            }
        }
        
        
    }
    
    private void Creat()
    {
        GameObject soundManager = new GameObject();
        soundManager.name = "SoundManager";
        soundManager.AddComponent<SoundManager>();
    }
}
