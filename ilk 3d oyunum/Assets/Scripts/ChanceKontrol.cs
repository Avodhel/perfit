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
    public Image brokenChanceImage;

    Sprite chance, brokenChance;
    Sprite[] sprites;

    [HideInInspector]
    public int chanceCounter;

	void Start ()
    {
        chanceCounter = startChance;
        //chanceText = gameObject.GetComponent<Text>();
        chanceText.text = "x " + startChance;
        findSprites();
	}
	
	void Update ()
    {
        chanceText.text = "x " + chanceCounter;
	}

    void findSprites()
    {
        sprites = Resources.LoadAll<Sprite>("Textures");

        for (int i = 0; i < sprites.Length; i++)
        {
            if (sprites[i].name == "chance")
            {
                chance = sprites[i];
            }
            else if (sprites[i].name == "brokenChance")
            {
                brokenChance = sprites[i];
            }
        }
    }

    public void chanceIncOrRed(string incOrRed)
    {
        if (incOrRed == "inc") //increase
        {
            chanceCounter += 1;
            brokenChanceText.color = Color.green;
            brokenChanceImage.overrideSprite = chance;
        }
        else if (incOrRed == "red") // reduce
        {
            chanceCounter -= 1;
            brokenChanceText.color = Color.red;
            brokenChanceImage.overrideSprite = brokenChance;
        }
    }

    public void brokenChanceFunc(bool brokenChanceActive)
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
