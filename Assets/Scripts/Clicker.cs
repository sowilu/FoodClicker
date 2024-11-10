using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Clicker : MonoBehaviour
{
    [Header("Animation settings")]
    public float scale = 1.2f;
    public float duration = 0.1f;
    public Ease ease;

    [Header("Sound")]
    public AudioClip clickSound;

    [Header("VFX")]
    public ParticleSystem clickParticles;

    [Header("Settings")]
    public int clickValue = 1;
    public List<GameObject> upgrades;

    [HideInInspector]public int clicks = 0;

    private AudioSource audioSource;
    private int upgradeIndex = 0;

    private int oldClicks = 0;

   
    private void Start() 
    {
        clicks = PlayerPrefs.GetInt("clicks", 0);
        clickValue = PlayerPrefs.GetInt("clickValue", 1);

        upgrades[upgradeIndex].SetActive(false);
        upgradeIndex = PlayerPrefs.GetInt("upgradeIndex", 0);
        upgrades[upgradeIndex].SetActive(true);
        clickParticles.GetComponent<ParticleSystemRenderer>().mesh = upgrades[upgradeIndex].GetComponent<MeshFilter>().sharedMesh;

        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("CountCps", 1, 1);
    }

    private void OnMouseDown() 
    {
        clickParticles.Emit(1);
        clicks++;
        UiManager.instance.UpdateClicks(clicks);
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(clickSound);

        transform
            .DOScale(1, duration)
            .ChangeStartValue(scale * Vector3.one)
            .SetEase(ease);//ease - how the animation will be played
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
        PlayerPrefs.SetInt("upgradeIndex", upgradeIndex);
        PlayerPrefs.Save();
    }

    public void UpgradeClicker()
    {
        if (upgradeIndex < upgrades.Count)
        {
            upgrades[upgradeIndex].SetActive(false);
            upgradeIndex++;
            upgrades[upgradeIndex].SetActive(true);

            clickValue++;

            clickParticles.GetComponent<ParticleSystemRenderer>().mesh = upgrades[upgradeIndex].GetComponent<MeshFilter>().sharedMesh;

        }
    }
}
