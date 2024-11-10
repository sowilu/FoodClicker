using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;

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

    private AudioSource audioSource;
    [HideInInspector] public int clicks = 0;
    private int oldClicks = 0;
    void Start()
    {
        clicks = PlayerPrefs.GetInt("clicks", 0);
        UiManager.instance.UpdateClicks(clicks);
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
    private void cps()
    {
        int cps = clicks - oldClicks;
        oldClicks = clicks;
        UiManager.instance.Updatecps(cps);
    }

    private void OnApplicationQuit()
    {
        Save();
    }
    private void Save()
    {
        PlayerPrefs.SetInt("clicks", clicks);
        PlayerPrefs.Save();
    }
    public void UpdateCokie()
    {
        clickValue++;
        updates[updateIndex].SetActive(false);
        updateIndex++;
        updates[updateIndex].SetActive(true);
    }
}
