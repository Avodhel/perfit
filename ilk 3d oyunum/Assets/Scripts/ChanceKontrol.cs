using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChanceKontrol : MonoBehaviour {

    [Range(0, 10)]
    public int startChance;

    [HideInInspector]
    public int chanceCounter;
    Text chanceText;

	void Start ()
    {
        chanceCounter = startChance;
        chanceText = gameObject.GetComponent<Text>();
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
            chanceCounter -= 1;
        }
        else if (incOrRed == "red") // reduce
        {
            chanceCounter -= 1;
        }
    }
}
