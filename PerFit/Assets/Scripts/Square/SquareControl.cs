using System.Collections;
using UnityEngine;

[System.Serializable]
public class SquareControl : MonoBehaviour {

    [Header("Speed")]
    [Range(0f, 1f)]
    public float rotateSpeed = 0.15f;

    [Header("Square Scale")]
    [Range(0f, 1f)]
    public float minSquareScale;
    [Range(0f, 1f)]
    public float maxSquareScale;

    [Header("Destroying Time")]
    public float destroyingTimeForObject;

    [HideInInspector]
    public bool fixSquareToPanelControl = false;

    private float rotateSide;
    private float rightOrLeft;
    private float putRotateSpeed;

    private GameObject panel;
    private GameObject cutPoint;
    private GameObject fitPoint;
    //private SpawnControl spawnControl;

    public void OnEnable()
    {
        FindObjects();
        DetermineRotateSide();
        DetermineSquareScale();
        putRotateSpeed = rotateSpeed;
    }

    public void Update()
    {
        RotateSquare();
        FixSquareToPanel();
    }

    public virtual void FindObjects() //override yapabilmemiz icin "virtual" 
    {
        panel        = GameObject.FindGameObjectWithTag("panelTag");
        cutPoint     = GameObject.FindGameObjectWithTag("cutPointTag");
        fitPoint     = GameObject.FindGameObjectWithTag("fitPointTag");
        //spawnControl = GameObject.FindGameObjectWithTag("spawnPointTag").GetComponent<SpawnControl>(); 
    }

    public void DetermineRotateSide()
    {
        rightOrLeft = Random.value; //random.value 0 ile 1 arasında random deger secer
        rotateSide = rightOrLeft <= 0.5f ? 1 : -1;
    }

    public void DetermineSquareScale()
    {
        float squareScale = Random.Range(minSquareScale, maxSquareScale);
        //Debug.Log("<color=black>sekil boyutu:</color> " + sekilBoyut);
        gameObject.transform.localScale = new Vector3(squareScale, gameObject.transform.localScale.y, squareScale);

        //if (spawnControl.squareRainSpawnKontrol == true & gameObject.name == "square(Clone)")
        //{
        //    float shapeScaleAfterSRain = Random.Range(0.8f, 0.85f);
        //    gameObject.transform.localScale = new Vector3(shapeScaleAfterSRain, gameObject.transform.localScale.y, shapeScaleAfterSRain);
        //}
    }

    public void RotateSquare()
    {
        if (!fixSquareToPanelControl)
        {
            transform.Rotate(new Vector3(0, Random.Range(0, 360f), 0) * ((Time.deltaTime * rotateSide) * rotateSpeed));
        }
    }

    public void FixSquareToPanel() //objeleri panele sabitleyerek onunla birlikte hereket etmesini sağlayan fonksiyon
    {
        if (fixSquareToPanelControl)
        {
            transform.localRotation = panel.transform.localRotation; //şeklin panele sabitlenmesi
        }

        cutPoint.transform.localRotation = panel.transform.localRotation; //cutpointin panele sabitlenmesi
        fitPoint.transform.localRotation = panel.transform.localRotation; //fitpointin panele sabitlenmesi
    }

    protected virtual void OnTriggerEnter(Collider other) //gecirgen yuzeye temas ettiginde
    {
        if (other.name == "FitPoint")
        {
            panel.GetComponent<PanelControl>().PanelParticlePlay(true);
            FindObjectOfType<SFXControl>().SesOynat("FitSound");
            rotateSpeed = 0f;
            GameControl.gameManager.AssignHeight(); //increase panel scale
            GameControl.gameManager.AssignScore("regular", 1f); //increase score
            GameControl.gameManager.GameSpeed("operation", 0.1f); //increase game speed
            GameControl.gameManager.ComboCount("inc");
            fixSquareToPanelControl = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "cutPointTag")
        {
            other.transform.localScale = new Vector3(gameObject.transform.localScale.x/5,
                                                     other.transform.localScale.y,
                                                     gameObject.transform.localScale.z/5);
            StartCoroutine(DestroySquare());
        }
    }

    public IEnumerator DestroySquare()
    {
        yield return new WaitForSeconds(destroyingTimeForObject);
        gameObject.SetActive(false);
        fixSquareToPanelControl = false;
        rotateSpeed = putRotateSpeed; //rotate square again
    }

}
