using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialSKontrol : MonoBehaviour {

    [Range(0f, 1f)]
    public float donmeHizi;

    float donmeYonu;
    bool sekliPaneleSabitleKontrol = false;

    GameObject Panel;
    GameObject cutPoint;
    GameObject fitPoint;
    OyunKontrol oyunKontrol;

    Image effectAlert;
    Sprite fast, slow, reverse, lottery;
    Sprite[] sprites;

    Text increaseScoreText;
    Text reduceScoreText;

    int increase1OrReduce2;

    void Start()
    {
        objeBul();
        sekilDonmeYonu();
        findSprites();
    }

    void Update()
    {
        sekilDondur();
        paneleSabitle();
    }

    private void objeBul()
    {
        Panel        = GameObject.FindGameObjectWithTag("panelTag");
        cutPoint     = GameObject.FindGameObjectWithTag("cutPointTag");
        fitPoint     = GameObject.FindGameObjectWithTag("fitPointTag");
        oyunKontrol  = GameObject.FindGameObjectWithTag("oyunKontrolTag").GetComponent<OyunKontrol>();
        effectAlert  = GameObject.FindGameObjectWithTag("effectAlertTag").GetComponent<Image>();

        increaseScoreText = GameObject.FindGameObjectWithTag("increaseScoreTag").GetComponent<Text>();
        reduceScoreText = GameObject.FindGameObjectWithTag("reduceScoreTag").GetComponent<Text>();
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

    void sekilDonmeYonu()
    {
        donmeYonu = Random.value <= 0.5f ? 1 : -1;

        //if (Random.value <= 0.5f) //random.value 0 ile 1 arasında random deger secer
        //    donmeYonu = 1; //sola donmesi icin
        //else
        //    donmeYonu = -1; // saga donmesi icin
    }

    void sekilDondur()
    {
        if (!sekliPaneleSabitleKontrol)
        {
            transform.Rotate(new Vector3(0, Random.Range(0, 360f), 0) * ((Time.deltaTime * donmeYonu) * donmeHizi));
        }
    }

    void paneleSabitle() //objeleri panele sabitleyerek onunla birlikte hereket etmesini sağlayan fonksiyon
    {
        if (sekliPaneleSabitleKontrol)
        {
            transform.localRotation = Panel.transform.localRotation; //şeklin panele sabitlenmesi
        }

        cutPoint.transform.localRotation = Panel.transform.localRotation; //cutpointin panele sabitlenmesi
        fitPoint.transform.localRotation = Panel.transform.localRotation; //fitpointin panele sabitlenmesi
    }

    void OnTriggerEnter(Collider other) //gecirgen yuzeye temas ettiginde
    {
        if (other.name == "FitPoint")
        {
            Panel.GetComponent<PanelKontrol>().panelParticleOynat(true);
            FindObjectOfType<SesKontrol>().sesOynat("FitSound");
            donmeHizi = 0f;
            Panel.transform.localScale += new Vector3(0f, 0.2f, 0f);
            oyunKontrol.score += 1.25f;
            sekliPaneleSabitleKontrol = true;

            StartCoroutine(specialSquareEffects(gameObject.name));
        }
    }

    IEnumerator specialSquareEffects(string whichEffect)
    {
        if (whichEffect == "fastSquare(Clone)")
        {
            effectAlert.enabled = true;
            effectAlert.overrideSprite = fast;
            oyunKontrol.oyunHizi = 3f;
            yield return new WaitForSeconds(15f);
            oyunKontrol.oyunHizi = 1.85f;
            effectAlert.enabled = false;
        }
        else if (whichEffect == "slowSquare(Clone)")
        {
            effectAlert.enabled = true; //effect alert image ac
            effectAlert.overrideSprite = slow; //slow spriteini effect alert olarak ata
            oyunKontrol.oyunHizi = 1f;
            yield return new WaitForSeconds(10f);
            oyunKontrol.oyunHizi = 1.75f;
            effectAlert.enabled = false; //effect alert image kapat
        }
        else if (whichEffect == "reverseSquare(Clone)")
        {
            effectAlert.enabled = true;
            effectAlert.overrideSprite = reverse;
            Panel.GetComponent<PanelKontrol>().reverseActive = true;
            yield return new WaitForSeconds(10f);
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
        increase1OrReduce2 = Random.value <= 0.5f ? 1 : 2;
        Debug.Log("<color=blue>inc or red</color> " + increase1OrReduce2);
        float randomIncOrReduceScore = Random.Range(0.1f, 5f);
        float incOrReduceScore = Mathf.Round(randomIncOrReduceScore * 100f) / 100f; //noktadan sonra sadece 2 basamak gözüksün
        Debug.Log("inc or reduce score: " + incOrReduceScore);
        float scoreAfterLottery = oyunKontrol.score;
        float fixScore;

        if (increase1OrReduce2 == 1) //increase
        {
            increaseScoreText.text = incOrReduceScore.ToString();
            increaseScoreText.enabled = true;
            Debug.Log("<color=green>score before inc</color>" + oyunKontrol.score);
            scoreAfterLottery += incOrReduceScore;
            fixScore = Mathf.Round(scoreAfterLottery * 100f) / 100f;
            oyunKontrol.score = fixScore;
            Debug.Log("<color=green>score after inc</color> " + oyunKontrol.score);
        }
        else if (increase1OrReduce2 == 2) //reduce
        {
            reduceScoreText.text = incOrReduceScore.ToString();
            reduceScoreText.enabled = true;
            Debug.Log("<color=red>score before red</color>" + oyunKontrol.score);
            scoreAfterLottery -= incOrReduceScore;
            fixScore = Mathf.Round(scoreAfterLottery * 100f) / 100f;
            oyunKontrol.score = fixScore;
            Debug.Log("<color=red>score after red</color> " + oyunKontrol.score);
        }

        yield return new WaitForSeconds(6f);
        lotteryFinish(increase1OrReduce2);
    }

    void lotteryFinish(int incOrReduce)
    {
        if (incOrReduce == 1) //increase
        {
            increaseScoreText.enabled = false;
        }
        else if (incOrReduce == 2) // reduce
        {
            reduceScoreText.enabled = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "cutPointTag")
        {
            StartCoroutine(destroySpecialSquare());
        }
    }

    IEnumerator destroySpecialSquare()
    {
        yield return new WaitForSeconds(50f);
        Destroy(gameObject);
    }
}
