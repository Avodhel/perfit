using UnityEngine;

public class BackgroundControl : MonoBehaviour {

    [Range(0f, 1000f)]
    public float cycleSeconds = 500f; // set to say 0.5f to test

    [HideInInspector]
    public float colorRateR;
    [HideInInspector]
    public float colorRateG;
    [HideInInspector]
    public float colorRateB;

    public void Start()
    {
        AssignColorRate();
    }

    public void Update()
    {
        ChangeBGColor();
    }

    public void AssignColorRate()
    {
        colorRateR = Random.Range(1f, 359f);
        colorRateG = Random.Range(0.3f, 0.8f);
        colorRateB = Random.Range(0.3f, 0.8f);
    }

    public virtual void ChangeBGColor()
    {
        GetComponent<Renderer>().material.color = Color.HSVToRGB
            (
            Mathf.Repeat((Time.time + colorRateR) / cycleSeconds, 1f),
            colorRateG,     // set to a pleasing value. 0f to 1f
            colorRateB      // set to a pleasing value. 0f to 1f
            );
    }
}
