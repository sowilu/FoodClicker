using TMPro;
using UnityEngine;

//singleton pattern - only one instance of this class can exist in scene
//uimanager can be accessed from any script without creating a variable
public class UiManager : MonoBehaviour
{
    //step 1: create a static variable of class type
    public static UiManager instance;

    public TextMeshProUGUI clickText;
    public TextMeshProUGUI cpsText;

    //step 2: method that executes earlier than Start()
    private void Awake() 
    {
        //step 3: check if there is no uimanager yet
        if(instance == null)
        {
            //step 4: set this uimanager as the instance (main and only)
            instance = this;
        }
        else
        {
            //step 5: destroy the new uimanager if there is already one
            Destroy(gameObject);
        }
    }

    public void UpdateClicks(int clicks)
    {
        clickText.text = clicks.ToString();
    }

    public void UpdateCps(int cps)
    {
        cpsText.text = cps.ToString();
    }
}
