using System.Collections;
using UnityEngine;

[System.Serializable]
public class SekilKontrol : MonoBehaviour {

    [Header("Speed")]
    [Range(0f, 1f)]
    public float donmeHizi = 0.15f;
    [Header("Square Scale")]
    [Range(0f, 1f)]
    public float minSquareScale;
    [Range(0f, 1f)]
    public float maxSquareScale;
    [Header("Destroying Time")]
    public float destroyingTimeForObject;

    [HideInInspector]
    public bool sekliPaneleSabitleKontrol = false;

    float donmeYonu;
    float rightOrLeft;

    GameObject Panel;
    GameObject cutPoint;
    GameObject fitPoint;
    SpawnKontrol spawnKontrol;

    public void OnEnable()
    {
        objeBul();
        sekilDonmeYonu();
        sekilBoyutAyarla();
    }

    public void Update()
    {
        sekilDondur();
        paneleSabitle();
    }

    public virtual void objeBul() //specialSkontrol'da override yapabilmemiz icin "virtual" 
    {
        Panel        = GameObject.FindGameObjectWithTag("panelTag");
        cutPoint     = GameObject.FindGameObjectWithTag("cutPointTag");
        fitPoint     = GameObject.FindGameObjectWithTag("fitPointTag");
        spawnKontrol = GameObject.FindGameObjectWithTag("spawnPointTag").GetComponent<SpawnKontrol>(); 
    }

    public void sekilDonmeYonu()
    {
        rightOrLeft = Random.value; //random.value 0 ile 1 arasında random deger secer
        donmeYonu = rightOrLeft <= 0.5f ? 1 : -1;
    }

    public void sekilBoyutAyarla()
    {
        float sekilBoyut = Random.Range(minSquareScale, maxSquareScale);
        //Debug.Log("<color=black>sekil boyutu:</color> " + sekilBoyut);
        gameObject.transform.localScale = new Vector3(sekilBoyut, gameObject.transform.localScale.y, sekilBoyut);

        if (spawnKontrol.squareRainSpawnKontrol == true & gameObject.name == "square(Clone)")
        {
            float shapeScaleAfterSRain = Random.Range(0.8f, 0.85f);
            gameObject.transform.localScale = new Vector3(shapeScaleAfterSRain, gameObject.transform.localScale.y, shapeScaleAfterSRain);
        }
    }

    public void sekilDondur()
    {
        if (!sekliPaneleSabitleKontrol)
        {
            transform.Rotate(new Vector3(0, Random.Range(0, 360f), 0) * ((Time.deltaTime * donmeYonu) * donmeHizi));
        }
    }

    public void paneleSabitle() //objeleri panele sabitleyerek onunla birlikte hereket etmesini sağlayan fonksiyon
    {
        if (sekliPaneleSabitleKontrol)
        {
            transform.localRotation = Panel.transform.localRotation; //şeklin panele sabitlenmesi
        }

        cutPoint.transform.localRotation = Panel.transform.localRotation; //cutpointin panele sabitlenmesi
        fitPoint.transform.localRotation = Panel.transform.localRotation; //fitpointin panele sabitlenmesi
    }

    protected virtual void OnTriggerEnter(Collider other) //gecirgen yuzeye temas ettiginde
    {
        if (other.name == "FitPoint")
        {
            Panel.GetComponent<PanelKontrol>().panelParticleOynat(true);
            FindObjectOfType<SesKontrol>().sesOynat("FitSound");
            donmeHizi = 0f;
            //Panel.transform.localScale += new Vector3(0f, 0.2f, 0f);
            GameControl.gameManager.assignHeight(); //increase panel scale
            GameControl.gameManager.assignScore(1f);
            sekliPaneleSabitleKontrol = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "cutPointTag")
        {
            other.transform.localScale = new Vector3(gameObject.transform.localScale.x/5,
                                                     other.transform.localScale.y,
                                                     gameObject.transform.localScale.z/5);
            StartCoroutine(destroySquare());
        }
    }

    IEnumerator destroySquare()
    {
        //Debug.Log(destroyingTimeForObject);
        yield return new WaitForSeconds(destroyingTimeForObject);
        //Destroy(gameObject);
        gameObject.SetActive(false);
        sekliPaneleSabitleKontrol = false;
        donmeHizi = 0.15f;
    }

}
