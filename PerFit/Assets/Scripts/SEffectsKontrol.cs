using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SEffectsKontrol : MonoBehaviour {

    Image effectAlert;
    ChanceKontrol chanceKontrol;
    SpawnKontrol spawnKontrol;
    Sprite fast, slow, reverse, lottery, squareRain, expand;
    Sprite[] sprites;
    GameObject Panel;
    Text increaseScoreText;
    Text reduceScoreText;

    int increase1OrReduce2OrChance3;

    [Header("Effects Time")]
    [Range(0f, 30f)]
    public float fastEffectTime = 20f;
    [Range(0f, 30f)]
    public float slowEffectTime = 14f;
    [Range(0f, 30f)]
    public float reverseEffectTime = 13f;
    [Range(0f, 30f)]
    public float squareRainEffectTime = 12f;
    [Range(0f, 30f)]
    public float expandEffectTime = 15f;

    [Header("Speed after Effects")]
    [Range(0f, 5f)]
    public float speedAfterFast = 2.8f;
    [Range(0f, 5f)]
    public float speedAfterSlow = 1f;

    void Start()
    {
        findObjects();
        findSprites();
    }

    void findObjects()
    {
        effectAlert       = GameObject.FindGameObjectWithTag("effectAlertTag").GetComponent<Image>();
        Panel             = GameObject.FindGameObjectWithTag("panelTag");
        increaseScoreText = GameObject.FindGameObjectWithTag("increaseScoreTag").GetComponent<Text>();
        reduceScoreText   = GameObject.FindGameObjectWithTag("reduceScoreTag").GetComponent<Text>();
        chanceKontrol     = GameObject.FindGameObjectWithTag("chanceKontrolTag").GetComponent<ChanceKontrol>();
        spawnKontrol      = GameObject.FindGameObjectWithTag("spawnPointTag").GetComponent<SpawnKontrol>();
    }

    void findSprites()
    {
        sprites = Resources.LoadAll<Sprite>("Textures"); //textures içindeki bütün spriteları bul

        for (int i = 0; i < sprites.Length; i++)
        {
            if (sprites[i].name == "fast")
            {
                fast = sprites[i];
            }
            else if (sprites[i].name == "slow")
            {
                slow = sprites[i];
            }
            else if (sprites[i].name == "reverse")
            {
                reverse = sprites[i];
            }
            else if (sprites[i].name == "lottery")
            {
                lottery = sprites[i];
            }
            else if (sprites[i].name == "squareRain")
            {
                squareRain = sprites[i];
            }
            else if (sprites[i].name == "expand")
            {
                expand = sprites[i];
            }
        }
    }

    public IEnumerator specialSquareEffects(string whichEffect)
    {
        if (whichEffect == "fastSquare(Clone)")
        {
            effectAlert.enabled = true;
            effectAlert.overrideSprite = fast;
            GameControl.gameManager.gameSpeed(speedAfterFast);
            yield return new WaitForSeconds(fastEffectTime);
            GameControl.gameManager.gameSpeed(GameControl.gameManager.gameSpeedValue);
            effectAlert.enabled = false;
        }
        else if (whichEffect == "slowSquare(Clone)")
        {
            effectAlert.enabled = true; //effect alert image ac
            effectAlert.overrideSprite = slow; //slow spriteini effect alert olarak ata
            GameControl.gameManager.gameSpeed(speedAfterSlow);
            yield return new WaitForSeconds(slowEffectTime);
            GameControl.gameManager.gameSpeed(GameControl.gameManager.gameSpeedValue);
            effectAlert.enabled = false; //effect alert image kapat
        }
        else if (whichEffect == "reverseSquare(Clone)")
        {
            effectAlert.enabled = true;
            effectAlert.overrideSprite = reverse;
            Panel.GetComponent<PanelKontrol>().reverseActive = true;
            yield return new WaitForSeconds(reverseEffectTime);
            Panel.GetComponent<PanelKontrol>().reverseActive = false;
            effectAlert.enabled = false;
        }
        else if (whichEffect == "lotterySquare(Clone)")
        {
            effectAlert.enabled = true;
            effectAlert.overrideSprite = lottery;
            StartCoroutine(lotteryStart());
            effectAlert.enabled = false;
        }
        //else if (whichEffect == "squareRain(Clone)")
        //{
        //    effectAlert.enabled = true;
        //    effectAlert.overrideSprite = squareRain;

        //    spawnKontrol.spawnSuresi = 3;
        //    spawnKontrol.minSekildeBirOzelSekil = 0;
        //    spawnKontrol.maxSekildeBirOzelSekil = 0;
        //    spawnKontrol.minSekildeBirLottery = 0;
        //    spawnKontrol.maxSekildeBirLottery = 0;
        //    yield return new WaitForSeconds(squareRainEffectTime);
        //    spawnKontrol.spawnSuresi = 7;
        //    spawnKontrol.minSekildeBirOzelSekil = 3;
        //    spawnKontrol.maxSekildeBirOzelSekil = 7;
        //    spawnKontrol.minSekildeBirLottery = 6;
        //    spawnKontrol.maxSekildeBirLottery = 18;
        //    spawnKontrol.squareRainSpawnKontrol = false;

        //    effectAlert.enabled = false;
        //}
        else if (whichEffect == "expandSquare(Clone)")
        {
            effectAlert.enabled = true;
            effectAlert.overrideSprite = expand;

            Panel.transform.localScale = new Vector3(1.8f, Panel.transform.localScale.y, 1.8f);
            yield return new WaitForSeconds(expandEffectTime);
            Panel.transform.localScale = new Vector3(1.7f, Panel.transform.localScale.y, 1.7f);

            effectAlert.enabled = false;
        }
    }

    IEnumerator lotteryStart()
    {
        float lotteryRandomPoint = Random.value;
        //Debug.Log("<color=black>Random Value:</color>" + lotteryRandomPoint);
        float randomLotteryPoint = Random.Range(0f, GameControl.gameManager.score * 0.25f);
        float lotteryPoint = Mathf.Round(randomLotteryPoint * 100f) / 100f; //noktadan sonra sadece 2 basamak gözüksün
        //Debug.Log("inc or reduce score: " + incOrReduceScore);
        //float scoreBeforeLottery = GameControl.gameManager.score;

        if (lotteryRandomPoint >= 0 & lotteryRandomPoint <= 0.45f)
        {
            increase1OrReduce2OrChance3 = 1;
        }
        else if (lotteryRandomPoint > 0.45f & lotteryRandomPoint <= 0.9f)
        {
            increase1OrReduce2OrChance3 = 2;
        }
        else if (lotteryRandomPoint > 0.9f & lotteryRandomPoint <= 1f)
        {
            increase1OrReduce2OrChance3 = 3;
        }
        //Debug.Log("<color=blue>inc or red or cha</color> " + increase1OrReduce2OrChance3);

        if (increase1OrReduce2OrChance3 == 1) //increase
        {
            increaseScoreText.text = lotteryPoint.ToString();
            increaseScoreText.enabled = true;
            //Debug.Log("<color=green>score before inc</color>" + oyunKontrol.score);
            GameControl.gameManager.assignScore(lotteryPoint);
            //Debug.Log("<color=green>score after inc</color> " + oyunKontrol.score);
        }
        else if (increase1OrReduce2OrChance3 == 2) //reduce
        {
            reduceScoreText.text = lotteryPoint.ToString();
            reduceScoreText.enabled = true;
            //Debug.Log("<color=red>score before red</color>" + oyunKontrol.score);
            GameControl.gameManager.assignScore(-lotteryPoint);
            //Debug.Log("<color=red>score after red</color> " + oyunKontrol.score);
        }
        else if (increase1OrReduce2OrChance3 == 3) //chance
        {
            GameControl.gameManager.gameSpeed(GameControl.gameManager.gameSpeedValue * 0.5f);
            chanceKontrol.chanceIncOrRed("inc");
            chanceKontrol.brokenChanceFunc(true);
        }

        yield return new WaitForSeconds(4f);
        lotteryFinish(increase1OrReduce2OrChance3);
    }

    void lotteryFinish(int incOrReduceOrChance)
    {
        if (incOrReduceOrChance == 1) //increase
        {
            increaseScoreText.enabled = false;
        }
        else if (incOrReduceOrChance == 2) // reduce
        {
            reduceScoreText.enabled = false;
        }
        else if (incOrReduceOrChance == 3)
        {
            chanceKontrol.brokenChanceFunc(false);
            GameControl.gameManager.gameSpeed(GameControl.gameManager.gameSpeedValue);
        }
    }
}
