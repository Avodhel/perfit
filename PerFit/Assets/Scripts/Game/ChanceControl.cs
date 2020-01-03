using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ChanceControl : MonoBehaviour {

    [Header("Chance for Start")]
    [Range(0, 10)]
    public int startChance = 2;

    [Header("Broken and Chance Panel")]
    [Range(0, 10)]
    public float exitTimeForBCPanel = 1.5f;

    public GameObject brokenAndChancePanel;
    public Image brokenAndChanceImage;
    public Slider exitBCPanelSlider;

    [Header("Texts")]
    public Text chanceText;
    public Text brokenAndChanceText;

    [Header("Chance Control On Off Control")]
    public bool chanceControlOnOffControl = false;

    [HideInInspector]
    public int chanceCounter;

    private Sprite chance, brokenChance;
    private Sprite[] sprites;

    private bool bcPanelisActive = false;

    private void Start ()
    {
        chanceCounter = startChance;
        chanceText.text = "x " + startChance;
        FindSprites();
        exitBCPanelSlider.minValue = 0f;
        exitBCPanelSlider.maxValue = exitTimeForBCPanel;
    }

    private void Update()
    {
        CountDownTimer();
    }

    private void FindSprites()
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

    public void ChanceIncOrRed(string incOrRed)
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

        chanceText.text = "x " + chanceCounter;
    }

    public void BrokenChanceState(bool brokenChanceActive)
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

    private void CountDownTimer()
    {
        if (bcPanelisActive)
        {
            GameControl.gameManager.GameSpeed("assign", 1f);
            exitTimeForBCPanel -= Time.deltaTime;
            exitBCPanelSlider.value = exitTimeForBCPanel;
            //Debug.Log("<color=gray>exit time for bc panel:</color>" + exitTimeForBCPanel);
        }
        else
        {
            exitTimeForBCPanel = exitBCPanelSlider.maxValue;
            exitBCPanelSlider.value = exitBCPanelSlider.maxValue;
        }
    }

    public IEnumerator ChanceState()
    {
        //Debug.Log(chanceControlOnOffControl);
        if (!chanceControlOnOffControl) // squareRain
        {
            if (chanceCounter == 0) // hiç chance kalmadıysa
            {
                GameControl.gameManager.GameOver(true);
            }
            else
            {
                //GameControl.gameManager.gameSpeed("assign", 1f);
                ChanceIncOrRed("red");
                BrokenChanceState(true);
                yield return new WaitForSeconds(exitTimeForBCPanel);
                BrokenChanceState(false);
                GameControl.gameManager.GameSpeed("assign", GameControl.gameManager.gameSpeedValue);
            }
        }
    }
}
