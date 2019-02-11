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
    PanelKontrol panelKontrol;

    Image effectAlert;
    Sprite fast, slow, reverse, question;
    Sprite[] sprites;

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
        Panel = GameObject.FindGameObjectWithTag("panelTag");
        cutPoint = GameObject.FindGameObjectWithTag("cutPointTag");
        fitPoint = GameObject.FindGameObjectWithTag("fitPointTag");
        oyunKontrol = GameObject.FindGameObjectWithTag("oyunKontrolTag").GetComponent<OyunKontrol>();
        effectAlert = GameObject.FindGameObjectWithTag("effectAlertTag").GetComponent<Image>();
        panelKontrol = GameObject.FindGameObjectWithTag("panelTag").GetComponent<PanelKontrol>();
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
            else if (sprites[i].name == "question")
            {
                question = sprites[i];
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
            yield return new WaitForSeconds(5f);
            oyunKontrol.oyunHizi = 1.75f;
            effectAlert.enabled = false; //effect alert image kapat
        }
        else if (whichEffect == "reverseSquare(Clone)")
        {
            effectAlert.enabled = true;
            effectAlert.overrideSprite = reverse;
            panelKontrol.reverseActive = true;
            yield return new WaitForSeconds(10f);
            panelKontrol.reverseActive = false;
            effectAlert.enabled = false;
        }
        else if (whichEffect == "questionSquare(Clone)")
        {
            effectAlert.enabled = true;
            effectAlert.overrideSprite = question;
            yield return new WaitForSeconds(5f);
            //Debug.Log("question effect happened");
            effectAlert.enabled = false;
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
