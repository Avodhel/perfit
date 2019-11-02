using UnityEngine;

[System.Serializable] //diğer classlardan erişilebilmesi için
public class SFXList {


    public string sesAdi;
    public AudioClip clip;
    [Range(0f, 1f)]//düzey ayarlama kısmını bar şekline getiriyoruz.
    public float volume;
    [Range(1f, 3f)]
    public float pitch;
    public bool loop;
    [HideInInspector] //unity arayüzünde görünmesin
    public AudioSource source;
}
