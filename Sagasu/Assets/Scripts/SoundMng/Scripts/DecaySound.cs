using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecaySound : MonoBehaviour
{
    [SerializeField] private Transform listener;
    [SerializeField] private AudioSource thisAudio;
    [SerializeField] private int level;
    [SerializeField] private float decayDistance;

    private float[] levelVolumes;
    private float deltaDistance;
    // Start is called before the first frame update
    void Start()
    {
        levelVolumes = new float[level];
        levelVolumes[0] = 1f;
        deltaDistance = decayDistance / level;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDecay())
        {
            VolumePlus();
        }
    }

    private bool IsDecay()
    {
        float dis = Vector3.Distance(listener.position, thisAudio.transform.position);
        if (dis <= decayDistance) return true;
        else return false;
    }

    private void VolumePlus()
    {
        float dis = Vector3.Distance(listener.position, thisAudio.transform.position);
    }
}
