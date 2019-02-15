using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChanceKontrol : MonoBehaviour {

    [Range(0, 10)]
    public int startChance;
    public Text chanceText;
    public GameObject brokenAndChancePanel;
    public Text brokenAndChanceText;
    public Image brokenAndChanceImage;

    Sprite chance, brokenChance;
    Sprite[] sprites;

    [HideInInspector]
    public int chanceCounter;
    public Slider exitBCPanelSlider;
    [Range(0, 10)]
    public float exitTimeForBCPanel;

    bool bcPanelisActive = false;

    OyunKontrol oyunKontrol;

    void Start ()
    {
        chanceCounter = startChance;
        //chanceText = gameObject.GetComponent<Text>();
        chanceText.text = "x " + startChance;
        findSprites();
        exitBCPanelSlider.minValue = 0f;
        exitBCPanelSlider.maxValue = exitTimeForBCPanel;

        oyunKontrol = GameObject.FindGameObjectWithTag("oyunKontrolTag").GetComponent<OyunKontrol>();
    }
	
	void Update ()
    {
        chanceText.text = "x " + chanceCounter;
        countDownTimer();
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
            brokenAndChanceText.color = Color.green;
            brokenAndChanceImage.overrideSprite = chance;
            exitBCPanelSlider.GetComponentInChildren<Image>().color = Color.green; //sliderdaki renk
        }
        else if (incOrRed == "red") // reduce
        {
            chanceCounter -= 1;
            brokenAndChanceText.color = Color.red;
            brokenAndChanceImage.overrideSprite = brokenChance;
            exitBCPanelSlider.GetComponentInChildren<Image>().color = Color.red; //sliderdaki renk
        }
    }

    public void brokenChanceFunc(bool brokenChanceActive)
    {
        if (brokenChanceActive)
        {
            brokenAndChancePanel.SetActive(true);
            brokenAndChanceText.text = "x " + chanceCounter;
            bcPanelisActive = true;
        }
        else
        {
            brokenAndChancePanel.SetActive(false);
            bcPanelisActive = false;
        }
    }

    public void countDownTimer()
    {

        if (bcPanelisActive)
        {
            exitTimeForBCPanel -= Time.deltaTime;
            exitBCPanelSlider.value = exitTimeForBCPanel;
            Debug.Log("<color=gray>exit time for bc panel:</color>" + exitTimeForBCPanel);
        }
        else
        {
            exitTimeForBCPanel = exitBCPanelSlider.maxValue;
            exitBCPanelSlider.value = exitBCPanelSlider.maxValue;
        }
    }

    public IEnumerator chanceControlFunc()
    {
        if (chanceCounter == 0) // hiç chance kalmadıysa
        {
            oyunKontrol.oyunBitti(true);
        }
        else
        {
            oyunKontrol.oyunHizi = 0.5f;
            chanceIncOrRed("red");
            brokenChanceFunc(true);
            yield return new WaitForSeconds(exitTimeForBCPanel);
            brokenChanceFunc(false);
            oyunKontrol.oyunHizi = PlayerPrefs.GetFloat("oyunHizi");
        }
    }
}
