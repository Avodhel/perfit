using UnityEngine;

public class PanelControl : MonoBehaviour {

#if UNITY_WEBGL
    [Range(1f, 100f)]
    public float panelMoveSpeed = 50f;
#elif UNITY_ANDROID
        [Range(1f, 30f)]
        public float rotSpeed = 12f;
#else
        Debug.Log("platform bulunamadı");
#endif
    public ParticleSystem panelParticle;

    [HideInInspector]
    public bool reverseActive = false;
    [HideInInspector]
    public float cycleSeconds = 500f;

    private Material bottomPointMat;
    private Material cutPointMat;
    private ChanceControl chanceControl;

    private void Start()
    {
        findObjects();
    }

    private void Update ()
    {
        panelMovement();
    }

    private void findObjects()
    {
        bottomPointMat = GameObject.FindGameObjectWithTag("bottomPointTag").GetComponent<Renderer>().material;
        cutPointMat = GameObject.FindGameObjectWithTag("cutPointTag").GetComponent<Renderer>().material;
        chanceControl = GameObject.FindGameObjectWithTag("chanceKontrolTag").GetComponent<ChanceControl>();
    }

    private void panelMovement()
    {
        int assignRotateDir;
        assignRotateDir = !reverseActive ? 1 : -1;

#if UNITY_WEBGL
        transform.Rotate(0, Input.GetAxis("Horizontal") * panelMoveSpeed * Time.deltaTime * assignRotateDir, 0); //sağa veya sola döndür(mobilde çalışmıyor)
#elif UNITY_ANDROID

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) //mobilde sağa ve sola döndür
        {
            Vector3 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            transform.Rotate(0, -(assignRotateDir * (touchDeltaPosition.x) * rotSpeed * Time.deltaTime), 0);
        }
#else
        Debug.Log("platform bulunamadı");
#endif

    }

    public void panelChangeColor(float colorRateR, float colorRateG, float colorRateB)
    {
        GetComponent<Renderer>().material.color = Color.HSVToRGB
                (
                Mathf.Repeat((Time.time + colorRateR) / cycleSeconds, 1f),
                colorRateG,     // set to a pleasing value. 0f to 1f
                colorRateB      // set to a pleasing value. 0f to 1f
                );

        bottomPointMat.color = Color.HSVToRGB
                (
                Mathf.Repeat((Time.time + colorRateR) / cycleSeconds, 1f),
                colorRateG,     // set to a pleasing value. 0f to 1f
                colorRateB      // set to a pleasing value. 0f to 1f
                );

        cutPointMat.color = Color.HSVToRGB
                (
                Mathf.Repeat((Time.time + colorRateR) / cycleSeconds, 1f),
                colorRateG,     // set to a pleasing value. 0f to 1f
                colorRateB      // set to a pleasing value. 0f to 1f
                );
    }

    public void panelParticlePlay(bool particleKontrol)
    {
        if (particleKontrol)
        {
            panelParticle.Play();
        }
    }

    private void OnCollisionEnter(Collision col) 
    {
        if (col.gameObject.tag.Equals("squareTag"))
        {
            determineHitAngle(col);
        }
    }

    private void determineHitAngle(Collision col) //panele carpan sekillerin hangi acidan carptigini saptama
    {
        Vector3 hit = col.contacts[0].normal;
        float angle = Vector3.Angle(hit, Vector3.up);

        if (Mathf.Approximately(angle, 180)) //panele üstten çarptıysa
        {
            FindObjectOfType<SFXControl>().sesOynat("HitSound");
            GameControl.gameManager.gameSpeed("default", GameControl.gameManager.defaultSpeedValue); //reset speed
            GameControl.gameManager.comboCount("reset");
            StartCoroutine(chanceControl.chanceControlFunc()); //chance kalıp kalmadığını kontrol et
        }
    }

}
