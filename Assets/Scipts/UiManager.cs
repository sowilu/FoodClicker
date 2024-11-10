using System;
using TMPro;
using UnityEngine;

//singleton pattern - only one instance of this class can exist, 
//can be accessed from anywhere
public class UiManager : MonoBehaviour
{
    //step 1: create a static instance of this class
    public static UiManager instance;

    public TextMeshProUGUI clickerText;
    public TextMeshProUGUI cpsText;

    //step 2: awake function is called before start, 
    //we use awake because instance may be called in start method
    private void Awake()
    {
        //step 3: check if there are no instances of this class
        if(instance == null)
        {
            //step 4: if there are no instances, set this instance to this class
            instance = this;
        }
        else
        {
            //step 5: if there is an instance, destroy this instance
            Destroy(this);
            Debug.LogWarning("There is already an instance of UiManager");
        }
    }


    public void UpdateClicks(int clicks)
    {
        clickerText.text = clicks.ToString();
    }

    public void UpdateCps(int cps)
    {
        cpsText.text = $"{cps} cps";
    }
}
