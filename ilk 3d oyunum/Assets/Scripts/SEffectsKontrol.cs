using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SEffectsKontrol : MonoBehaviour {

    Image effectAlert;
    OyunKontrol oyunKontrol;
    Sprite fast, slow, reverse, lottery;
    Sprite[] sprites;
    GameObject Panel;
    Text increaseScoreText;
    Text reduceScoreText;
    ChanceKontrol chanceKontrol;

    int increase1OrReduce2OrChance3;

    [Range(0f, 30f)]
    public float fastEffectTime;
    [Range(0f, 30f)]
    public float slowEffectTime;
    [Range(0f, 30f)]
    public float reverseEffectTime;

    [Range(0f, 5f)]
    public float speedAfterFast;
    [Range(0f, 5f)]
    public float speedAfterSlow;

    void Start()
    {
        findObjects();
        findSprites();
    }

    void findObjects()
    {
        effectAlert = GameObject.FindGameObjectWithTag("effectAlertTag").GetComponent<Image>();
        oyunKontrol = GameObject.FindGameObjectWithTag("oyunKontrolTag").GetComponent<OyunKontrol>();
        Panel = GameObject.FindGameObjectWithTag("panelTag");
        increaseScoreText = GameObject.FindGameObjectWithTag("increaseScoreTag").GetComponent<Text>();
        reduceScoreText = GameObject.FindGameObjectWithTag("reduceScoreTag").GetComponent<Text>();
        chanceKontrol = GameObject.FindGameObjectWithTag("chanceKontrolTag").GetComponent<ChanceKontrol>();
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
        }
    }

    public IEnumerator specialSquareEffects(string whichEffect)
    {
        if (whichEffect == "fastSquare(Clone)")
        {
            effectAlert.enabled = true;
            effectAlert.overrideSprite = fast;
            oyunKontrol.oyunHizi = speedAfterFast;
            yield return new WaitForSeconds(fastEffectTime);
            oyunKontrol.oyunHizi = PlayerPrefs.GetFloat("oyunHizi");
            effectAlert.enabled = false;
        }
        else if (whichEffect == "slowSquare(Clone)")
        {
            effectAlert.enabled = true; //effect alert image ac
            effectAlert.overrideSprite = slow; //slow spriteini effect alert olarak ata
            oyunKontrol.oyunHizi = speedAfterSlow;
            yield return new WaitForSeconds(slowEffectTime);
            oyunKontrol.oyunHizi = PlayerPrefs.GetFloat("oyunHizi");
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
    }

    IEnumerator lotteryStart()
    {
        float lotteryRandomPoint = Random.value;
        //Debug.Log("<color=black>Random Value:</color>" + lotteryRandomPoint);
        float randomIncOrReduceScore = Random.Range(0.1f, 5f);
        float incOrReduceScore = Mathf.Round(randomIncOrReduceScore * 100f) / 100f; //noktadan sonra sadece 2 basamak gözüksün
        //Debug.Log("inc or reduce score: " + incOrReduceScore);
        float scoreAfterLottery = oyunKontrol.score;

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
            increaseScoreText.text = incOrReduceScore.ToString();
            increaseScoreText.enabled = true;
            //Debug.Log("<color=green>score before inc</color>" + oyunKontrol.score);
            scoreAfterLottery += incOrReduceScore;
            oyunKontrol.score = fixScoreFunc(scoreAfterLottery);
            //Debug.Log("<color=green>score after inc</color> " + oyunKontrol.score);
        }
        else if (increase1OrReduce2OrChance3 == 2) //reduce
        {
            reduceScoreText.text = incOrReduceScore.ToString();
            reduceScoreText.enabled = true;
            //Debug.Log("<color=red>score before red</color>" + oyunKontrol.score);
            scoreAfterLottery -= incOrReduceScore;
            oyunKontrol.score = fixScoreFunc(scoreAfterLottery);
            //Debug.Log("<color=red>score after red</color> " + oyunKontrol.score);
        }
        else if (increase1OrReduce2OrChance3 == 3) //chance
        {
            oyunKontrol.oyunHizi = 0.5f;
            chanceKontrol.chanceIncOrRed("inc");
            chanceKontrol.brokenChanceFunc(true);
        }

        yield return new WaitForSeconds(2f);
        lotteryFinish(increase1OrReduce2OrChance3);
    }

    float fixScoreFunc(float scoreForFix)
    {
        if (scoreForFix < 0) //eğer reduce sonrası skor 0'ın altına düşerse skor olarak 0 gönder.
        {
            return 0;
        }
        else
        {
            return (Mathf.Round(scoreForFix * 100f) / 100f);
        }
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
            oyunKontrol.oyunHizi = 1.75f;
        }
    }
}
