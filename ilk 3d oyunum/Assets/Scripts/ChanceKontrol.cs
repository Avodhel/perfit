using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChanceKontrol : MonoBehaviour {

    [Range(0, 10)]
    public int startChance;
    public Text chanceText;
    public GameObject brokenChancePanel;
    public Text brokenChanceText;

    [HideInInspector]
    public int chanceCounter;

	void Start ()
    {
        chanceCounter = startChance;
        //chanceText = gameObject.GetComponent<Text>();
        chanceText.text = "x " + startChance;
	}
	
	void Update ()
    {
        chanceText.text = "x " + chanceCounter;
	}

    public void chanceIncOrRed(string incOrRed)
    {
        if (incOrRed == "inc") //increase
        {
            chanceCounter += 1;
            brokenChanceText.color = Color.green;
        }
        else if (incOrRed == "red") // reduce
        {
            chanceCounter -= 1;
            brokenChanceText.color = Color.red;
        }
    }

    public void brokenChance(bool brokenChanceActive)
    {
        if (brokenChanceActive)
        {
            brokenChancePanel.SetActive(true);
            brokenChanceText.text = "x " + chanceCounter;
        }
        else
        {
            brokenChancePanel.SetActive(false);
        }
    }
}
