using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        grannyCount = PlayerPrefs.GetInt("grannyCount", 0);
        grannyPrice = PlayerPrefs.GetFloat("grannyPrice", 10);
        upgradeCount = PlayerPrefs.GetInt("upgradeCount", 0);
        upgradePrice = PlayerPrefs.GetFloat("upgradePrice", 100);
        grannyButton.UpdateText((int)Mathf.Ceil(grannyPrice), grannyCount);
        upgradeButton.UpdateText((int)Mathf.Ceil(upgradePrice), upgradeCount);

        if(upgradeCount >= 3)
        {
            upgradeButton.GetComponent<Button>().interactable = false;
        }


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

    public void BuyUpgrade()
    {
        if (clikcer.clicks >= upgradePrice)
        {
            clikcer.clicks -= (int)Mathf.Ceil(upgradePrice);
            UiManager.instance.UpdateClicks(clikcer.clicks);

            upgradeCount++;
            upgradePrice = upgradePrice * 2f;//increase price by 100%   
            upgradeButton.UpdateText((int)Mathf.Ceil(upgradePrice), upgradeCount);

            clikcer.UpdateCookie();

            if(upgradeCount >= 3)
            {
                upgradeButton.GetComponent<Button>().interactable = false;
            }
        }
    }

    void Cook()
    {
        var particles = Mathf.Min(grannyCount * cpg, 100);
        clickParticles.Emit(particles);
        clikcer.clicks += grannyCount * cpg;
        UiManager.instance.UpdateClicks(clikcer.clicks);
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
        PlayerPrefs.SetInt("grannyCount", grannyCount);
        PlayerPrefs.SetFloat("grannyPrice", grannyPrice);
        PlayerPrefs.SetInt("upgradeCount", upgradeCount);
        PlayerPrefs.SetFloat("upgradePrice", upgradePrice);
        PlayerPrefs.Save();
    }
}
