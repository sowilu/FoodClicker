using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Clikcer : MonoBehaviour
{
    [Header("Animation settings")]
    public float scale = 1.2f;
    public float duration = 0.1f;
    public Ease ease;

    [Header("Sound")]
    public AudioClip clickSound;

    [Header("Particles")]
    public ParticleSystem clickParticles;

    [Header("Settings")]
    public int clickValue = 1;
    public List<GameObject> updates;
    [HideInInspector] public int updateIndex = 0;

    [HideInInspector] public int clicks = 0;

    private AudioSource audioSource;
    private int oldClicks = 0;
    
    void Start()
    {
        clicks = PlayerPrefs.GetInt("clicks", 0);
        UiManager.instance.UpdateClicks(clicks);

        clickValue = PlayerPrefs.GetInt("clickValue", 1);

        updates[updateIndex].SetActive(false);
        updateIndex = PlayerPrefs.GetInt("updateIndex", 0);
        updates[updateIndex].SetActive(true);
        clickParticles.GetComponent<ParticleSystemRenderer>().mesh = updates[updateIndex].GetComponent<MeshFilter>().sharedMesh;




        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("CountCps", 1, 1);
    }

    void Update()
    {
        
    }


    private void OnMouseDown() 
    {
        clickParticles.Emit(1);

        clicks += clickValue;
        UiManager.instance.UpdateClicks(clicks);

        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(clickSound);

        transform
            .DOScale(1, duration)
            .ChangeStartValue(scale * Vector3.one)
            .SetEase(ease);
            //.SetLoops(2, LoopType.Yoyo);
    }

    private void CountCps()
    {
        int cps = clicks - oldClicks;
        oldClicks = clicks;
        UiManager.instance.UpdateCps(cps);
    }


    private void OnApplicationPause(bool pauseStatus) 
    {
        if(pauseStatus)
        {
            Save();
        }
    }

    private void OnApplicationQuit() 
    {
        Save();
    }

    private void Save()
    {
        PlayerPrefs.SetInt("clicks", clicks);
        PlayerPrefs.SetInt("clickValue", clickValue);
        PlayerPrefs.SetInt("updateIndex", updateIndex);
        PlayerPrefs.Save();
    }

    public void UpdateCookie()
    {
        clickValue++;

        //change model
        updates[updateIndex].SetActive(false);
        updateIndex++;
        updates[updateIndex].SetActive(true);

        //set particle system mesh to new mesh
        clickParticles.GetComponent<ParticleSystemRenderer>().mesh = updates[updateIndex].GetComponent<MeshFilter>().sharedMesh;
    }

}
