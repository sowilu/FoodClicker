using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public ParticleSystem clickParticles;

    [Header("Granny")]
    public ShopButton grannyButton;
    public float grannyPrice = 10;
    public int grannyCount = 0;
    public int cpg = 1; //cookies per granny
    public float cookTime = 1;
    [Header("Upgrades")]

    public ShopButton upgradeButton;

    public float upgradePrice = 100;

    public int upgradeCount = 0;


    private Clikcer clikcer;

    void Start()
    {
        grannyPrice = PlayerPrefs.GetInt("grannyPrice", 0);
        grannyCount = PlayerPrefs.GetInt("grannyCount", 0);
        grannyButton.UpdateText((int)Mathf.Ceil(grannyPrice),grannyCount);
        //search in whole scene for object with type Clikcer
        clikcer = FindAnyObjectByType<Clikcer>();

        InvokeRepeating("Cook", 0, cookTime);
    }

    public void BuyGranny()
    {
        if (clikcer.clicks >= grannyPrice)
        {
            clikcer.clicks -= (int)Mathf.Ceil(grannyPrice);
            UiManager.instance.UpdateClicks(clikcer.clicks);

            grannyCount++;
            grannyPrice = grannyPrice * 1.1f;//increase price by 10%   
            grannyButton.UpdateText((int)Mathf.Ceil(grannyPrice), grannyCount);
        }
    }

    void Cook()
    {
        var particles = Mathf.Min(grannyCount * cpg, 100);
        clickParticles.Emit(particles);
        clikcer.clicks += grannyCount * cpg;
        UiManager.instance.UpdateClicks(clikcer.clicks);
    }
    private void OnApplicationQuit()
    {
        Save();   
    }
    private void Save()
    {
        PlayerPrefs.SetFloat("grannyPrice", grannyPrice);
        PlayerPrefs.SetInt("grannycount", grannyCount);
        PlayerPrefs.Save();
    }
    public void BuyUpgrade()
    {
        if(clikcer.clicks >= upgradePrice)
        {
            clikcer.clicks -= (int)Mathf.Ceil(upgradePrice);
            UiManager.instance.UpdateClicks(clikcer.clicks);

            upgradeCount++;
            upgradePrice = upgradePrice * 2f;
            upgradeButton.UpdateText((int)Mathf.Ceil(upgradePrice),upgradeCount);

            clikcer.UpdateCokie();
        }
    }
}
