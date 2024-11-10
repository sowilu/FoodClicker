using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class Clicker : MonoBehaviour
{
    [Header("Animation")]
    public float scale = 1.2f;
    public float duration = 0.5f;
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
        oldClicks = clicks = PlayerPrefs.GetInt("clicks", 0);
        UiManager.instance.UpdateClicks(clicks);

        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("CountCps", 1, 1);
    }

    private void OnMouseDown() 
    {
        clickVFX.Emit(1);

        clicks++;
        UiManager.instance.UpdateClicks(clicks);

        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(clickSound);

        transform
            .DOScale(1, duration)
            .ChangeStartValue(scale * Vector3.one)
            .SetEase(ease);
            //.SetLoops(2, LoopType.Yoyo);3
    }

    void CountCps()
    {
        int cps = clicks - oldClicks;
        oldClicks = clicks;

        if(cps > 0)
            UiManager.instance.UpdateCps(cps);
    }

    private void OnApplicationPause(bool pauseStatus) 
    {
        if(pauseStatus)
            Save();
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
}
