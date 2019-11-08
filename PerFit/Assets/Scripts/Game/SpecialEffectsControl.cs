using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpecialEffectsControl : MonoBehaviour {

    [Header("Effects Time")]
    [Range(0f, 30f)]
    public float fastEffectTime = 20f;
    [Range(0f, 30f)]
    public float slowEffectTime = 14f;
    [Range(0f, 30f)]
    public float reverseEffectTime = 12f;
    [Range(0f, 30f)]
    public float squareRainEffectTime = 12f;
    [Range(0f, 30f)]
    public float expandEffectTime = 15f;

    [Header("Speed after Effects")]
    [Range(0f, 5f)]
    public float speedAfterFast = 2.8f;
    [Range(0f, 5f)]
    public float speedAfterSlow = 1f;

    private Image effectAlert;
    private ChanceControl chanceControl;
    //private SpawnControl spawnControl;
    private Sprite fast, slow, reverse, lottery, squareRain, expand;
    private Sprite[] sprites;
    private GameObject Panel;
    private Text increaseScoreText;
    private Text reduceScoreText;

    private enum LotteryState { Increase, Reduce, ExtraChance}
    private LotteryState lotteryResult;

    private void Start()
    {
        findObjects();
        findSprites();
    }

    private void findObjects()
    {
        effectAlert       = GameObject.FindGameObjectWithTag("effectAlertTag").GetComponent<Image>();
        Panel             = GameObject.FindGameObjectWithTag("panelTag");
        increaseScoreText = GameObject.FindGameObjectWithTag("increaseScoreTag").GetComponent<Text>();
        reduceScoreText   = GameObject.FindGameObjectWithTag("reduceScoreTag").GetComponent<Text>();
        chanceControl     = GameObject.FindGameObjectWithTag("chanceKontrolTag").GetComponent<ChanceControl>();
        //spawnControl      = GameObject.FindGameObjectWithTag("spawnPointTag").GetComponent<SpawnControl>();
    }

    private void findSprites()
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
        //if (whichEffect == "fastSquare(Clone)")
        //{
        //    effectAlert.enabled = true;
        //    effectAlert.overrideSprite = fast;
        //    //GameControl.gameManager.gameSpeed("assign", speedAfterFast);
        //    GameControl.gameManager.gameSpeed("operation", 0.45f);
        //    yield return new WaitForSeconds(fastEffectTime);
        //    //GameControl.gameManager.gameSpeed("assign", GameControl.gameManager.gameSpeedValue);
        //    effectAlert.enabled = false;
        //}
        if (whichEffect == "slowSquare(Clone)")
        {
            effectAlert.enabled = true; //effect alert image ac
            effectAlert.overrideSprite = slow; //slow spriteini effect alert olarak ata
            //GameControl.gameManager.gameSpeed("assign", speedAfterSlow);
            GameControl.gameManager.gameSpeed("operation", -0.15f);
            yield return new WaitForSeconds(slowEffectTime);
            //GameControl.gameManager.gameSpeed("assign", GameControl.gameManager.gameSpeedValue);
            effectAlert.enabled = false; //effect alert image kapat
        }
        else if (whichEffect == "reverseSquare(Clone)")
        {
            effectAlert.enabled = true;
            effectAlert.overrideSprite = reverse;
            Panel.GetComponent<PanelControl>().reverseActive = true;
            yield return new WaitForSeconds(reverseEffectTime);
            Panel.GetComponent<PanelControl>().reverseActive = false;
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

    private IEnumerator lotteryStart()
    {
        float lotteryPossibility = Random.value; //between 0 and 1
        float randomLotteryPoint = Random.Range(GameControl.gameManager.score * 0.10f, GameControl.gameManager.score * 0.50f);
        float lotteryPoint = Mathf.Round(randomLotteryPoint * 100f) / 100f; //noktadan sonra sadece 2 basamak gözüksün

        if (lotteryPossibility >= 0 & lotteryPossibility <= 0.45f)
        {
            lotteryResult = LotteryState.Increase;
        }
        else if (lotteryPossibility > 0.45f & lotteryPossibility <= 0.9f)
        {
            lotteryResult = LotteryState.Reduce;
        }
        else if (lotteryPossibility > 0.9f & lotteryPossibility <= 1f)
        {
            lotteryResult = LotteryState.ExtraChance;
        }

        if (lotteryResult == LotteryState.Increase) //increase
        {
            increaseScoreText.text = lotteryPoint.ToString();
            increaseScoreText.enabled = true;
            //Debug.Log("<color=green>score before inc</color>" + oyunKontrol.score);
            GameControl.gameManager.assignScore("lottery", lotteryPoint);
            //Debug.Log("<color=green>score after inc</color> " + oyunKontrol.score);
        }
        else if (lotteryResult == LotteryState.Reduce) //reduce
        {
            reduceScoreText.text = lotteryPoint.ToString();
            reduceScoreText.enabled = true;
            //Debug.Log("<color=red>score before red</color>" + oyunKontrol.score);
            GameControl.gameManager.assignScore("lottery", -lotteryPoint);
            //Debug.Log("<color=red>score after red</color> " + oyunKontrol.score);
        }
        else if (lotteryResult == LotteryState.ExtraChance) //chance
        {
            GameControl.gameManager.gameSpeed("assign", 1f);
            chanceControl.chanceIncOrRed("inc");
            chanceControl.brokenChanceFunc(true);
        }

        yield return new WaitForSeconds(2f);
        lotteryFinish(lotteryResult);
    }

    private void lotteryFinish(LotteryState lotteryResult)
    {
        if (lotteryResult == LotteryState.Increase) //increase
        {
            increaseScoreText.enabled = false;
        }
        else if (lotteryResult == LotteryState.Reduce) // reduce
        {
            reduceScoreText.enabled = false;
        }
        else if (lotteryResult == LotteryState.ExtraChance)
        {
            chanceControl.brokenChanceFunc(false);
            GameControl.gameManager.gameSpeed("assign", GameControl.gameManager.gameSpeedValue);
        }
    }
}
