using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SesKontrol : MonoBehaviour {

    public GameObject sesiKapaAcButonu;
    public Sprite sesiAcSprite;
    public Sprite sesiKapaSprite;

    public SeslerListesi[] sesler; //seslerlistesi classımıza erişiyoruz.

    void Awake() //start metoduna benzer ancak oyun başladığından değil başlamadan önce çalışır
    {
        foreach (SeslerListesi s in sesler) //sesler listesindeki her bir ses(s) için
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start()
    {
        sesAcikMiKapaliMi();
    }

    public void sesOynat(string sesAdi) //bu metot ile ismine göre seslerimizi istediğimiz yerde oynatacağız.
    {
        SeslerListesi s = Array.Find(sesler, ses => ses.sesAdi == sesAdi);
        if (s == null)
        {
            Debug.LogWarning(sesAdi + " adli ses dosyasi bulunamadi.");
            return;
        }
        s.source.Play();
    }

    public void sesleriKapatveAc()
    {
        if (PlayerPrefs.GetInt("sesAcikMiKapaliMi") == 0) //ses açıksa
        {
            sesiKapaAcButonu.GetComponent<Image>().sprite = sesiKapaSprite;
            AudioListener.volume = 0f; //sesi kapat
            PlayerPrefs.SetInt("sesAcikMiKapaliMi", 1);
        }
        else if (PlayerPrefs.GetInt("sesAcikMiKapaliMi") == 1) //ses kapalıysa
        {
            sesiKapaAcButonu.GetComponent<Image>().sprite = sesiAcSprite;
            AudioListener.volume = 1f; //sesi ac
            PlayerPrefs.SetInt("sesAcikMiKapaliMi", 0);
        }
    }

    void sesAcikMiKapaliMi() //restart sonrası ses acik veya kapali sprite sorununu çözen fonksiyon
    {
        if ((PlayerPrefs.GetInt("sesAcikMiKapaliMi")) == 0)
        {
            sesiKapaAcButonu.GetComponent<Image>().sprite = sesiAcSprite;
            AudioListener.volume = 1f; //sesi ac
        }
        else if ((PlayerPrefs.GetInt("sesAcikMiKapaliMi")) == 1)
        {
            sesiKapaAcButonu.GetComponent<Image>().sprite = sesiKapaSprite;
            AudioListener.volume = 0f; //sesi kapat
        }
    }
}
