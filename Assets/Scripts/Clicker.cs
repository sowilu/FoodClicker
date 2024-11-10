using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Clicker : MonoBehaviour
{
    [Header("Animation settings")]
    public float scale = 1.2f;
    public float duration = 0.1f;
    public Ease ease = Ease.OutElastic;

    [Header("Audio")]
    public AudioClip clickSound;

    [Header("VFX")]
    public ParticleSystem clickVFX;


    [HideInInspector]public int clicks = 0;
    private AudioSource audioSource;
    private int oldClicks = 0;


    private void Start() 
    {
        clicks = PlayerPrefs.GetInt("clicks", 0);
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("CountClicks", 1, 1);
    }

    private void OnMouseDown() 
    {
        clickVFX.Emit(1);
        clicks++;
        //Debug.Log("Clicks: " + clicks);
        UiManager.instance.UpdateClicks(clicks);

        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(clickSound);

        transform
            .DOScale(1, duration)
            .ChangeStartValue(scale * Vector3.one)
            .SetEase(ease);
            //.SetLoops(2, LoopType.Yoyo);
    }

    private void CountClicks()
    {
        var cps = clicks - oldClicks;
        oldClicks = clicks;
        //todo: update cps text
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

    public void Save()
    {
        PlayerPrefs.SetInt("clicks", clicks);
        PlayerPrefs.Save();
    }
}
