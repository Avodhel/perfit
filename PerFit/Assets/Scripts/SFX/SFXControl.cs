﻿using UnityEngine;
using System;
using UnityEngine.UI;

public class SFXControl : MonoBehaviour {

    [Header("Mute Unmute Button")]
    public GameObject soundMuteUnmuteButton;
    public Sprite soundUnmuteSprite;
    public Sprite soundMuteSprite;

    [Header("SFX List")]
    public SFXList[] sesler; //seslerlistesi classımıza erişiyoruz.

    private void Awake() //start metoduna benzer ancak oyun başladığından değil başlamadan önce çalışır
    {
        foreach (SFXList s in sesler) //sesler listesindeki her bir ses(s) için
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        soundMuteUnmuteCheck();
    }

    public void sesOynat(string sesAdi) //bu metot ile ismine göre seslerimizi istediğimiz yerde oynatacağız.
    {
        SFXList s = Array.Find(sesler, ses => ses.sesAdi == sesAdi);
        if (s == null)
        {
            Debug.LogWarning(sesAdi + " adli ses dosyasi bulunamadi.");
            return;
        }
        s.source.Play();
    }

    public void soundMuteAndUnmute()
    {
        if (PlayerPrefs.GetInt("sesAcikMiKapaliMi") == 0) //ses açıksa
        {
            soundMuteUnmuteButton.GetComponent<Image>().sprite = soundMuteSprite;
            AudioListener.volume = 0f; //sesi kapat
            PlayerPrefs.SetInt("sesAcikMiKapaliMi", 1);
        }
        else if (PlayerPrefs.GetInt("sesAcikMiKapaliMi") == 1) //ses kapalıysa
        {
            soundMuteUnmuteButton.GetComponent<Image>().sprite = soundUnmuteSprite;
            AudioListener.volume = 1f; //sesi ac
            PlayerPrefs.SetInt("sesAcikMiKapaliMi", 0);
        }
    }

    private void soundMuteUnmuteCheck() //restart sonrası ses acik veya kapali sprite sorununu çözen fonksiyon
    {
        if ((PlayerPrefs.GetInt("sesAcikMiKapaliMi")) == 0)
        {
            soundMuteUnmuteButton.GetComponent<Image>().sprite = soundUnmuteSprite;
            AudioListener.volume = 1f; //sesi ac
        }
        else if ((PlayerPrefs.GetInt("sesAcikMiKapaliMi")) == 1)
        {
            soundMuteUnmuteButton.GetComponent<Image>().sprite = soundMuteSprite;
            AudioListener.volume = 0f; //sesi kapat
        }
    }
}
