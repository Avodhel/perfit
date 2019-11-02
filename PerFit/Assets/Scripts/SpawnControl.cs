using System.Collections;
using UnityEngine;

public class SpawnControl : MonoBehaviour {

    [Header("Frequency for Squares")]
    [Range(1, 50)]
    public int spawnRate = 6;

    [Header("Frequency for Special Squares")]
    [Range(0, 10)]
    public int minSpecialSquareRate = 3;
    [Range(0, 10)]
    public int maxSpecialSquareRate = 7;
    [Range(0, 20)]
    public int minLotteryRate = 6;
    [Range(0, 20)]
    public int maxLotteryRate = 18;

    [HideInInspector]
    public bool squareRainSpawnControl = false;
    [HideInInspector]
    public float cycleSeconds = 500f;

    private GameObject specialSquare;
    private GameObject[] squares;

    private int specialSquareRate;
    private int lotteryRate;
    private int squareCounter = 0;
    private int counterForLottery = 0;
    private bool squareSpawnedControl = false;
    private bool colorChangedControl = false;

    private void Awake()
    {
        specialSquareRate = Random.Range(minSpecialSquareRate, maxSpecialSquareRate);
        lotteryRate = Random.Range(minLotteryRate, maxLotteryRate);
    }

    private void Start ()
    {        
        StartCoroutine(spawnSquare());  
    }

    #region Spawn Square
    private IEnumerator spawnSquare()
    {
        while (true)
        {
            if (!squareRainSpawnControl)
            {
                squareCounter += 1;
                counterForLottery += 1;
            }

            chooseSquare();

            squares = GameObject.FindGameObjectsWithTag("squareTag");

            squareSpawnedControl = true;
            colorChangedControl = false;

            yield return new WaitForSeconds(spawnRate);
        }
    }

    private void chooseSquare()
    {
        /*----------spawn lottery----------*/
        if (counterForLottery == lotteryRate & !squareRainSpawnControl)
        {
            GameObject lotterySquare = SquarePooler.SharedInstance.GetPooledObject(1);
            lotterySquare.SetActive(true);
            lotterySquare.transform.position = transform.position;
            lotterySquare.transform.rotation = Quaternion.Euler(transform.rotation.x, Random.Range(-360f, 360f), transform.rotation.z);

            counterForLottery = 0;
            lotteryRate = Random.Range(minLotteryRate, maxLotteryRate);

            while (squareCounter == specialSquareRate) //eger lottery, ozel sekille cakisirsa
            {
                squareCounter = 0;
                specialSquareRate = Random.Range(minSpecialSquareRate, maxSpecialSquareRate);
            }
        }
        /*----------spawn special square----------*/
        else if (squareCounter == specialSquareRate & !squareRainSpawnControl)
        {
            GameObject specialSquare = SpecialSquarePooler.SharedInstance.GetPooledObject(Random.Range(0, SpecialSquarePooler.SharedInstance.itemsToPool.Count));
            specialSquare.SetActive(true);
            specialSquare.transform.position = transform.position;
            specialSquare.transform.rotation = Quaternion.Euler(transform.rotation.x, Random.Range(-360f, 360f), transform.rotation.z);

            squareCounter = 0;
            specialSquareRate = Random.Range(minSpecialSquareRate, maxSpecialSquareRate);

            if (specialSquare.name == "squareRain")
            {
                squareRainSpawnControl = true;
                //countForLottery = 0;
            }
        }
        /*----------spawn square----------*/
        else if (squareCounter < specialSquareRate & counterForLottery < lotteryRate)
        {
            GameObject square = SquarePooler.SharedInstance.GetPooledObject(0);
            square.SetActive(true);
            square.transform.position = transform.position;
            square.transform.rotation = Quaternion.Euler(transform.rotation.x, Random.Range(-360f, 360f), transform.rotation.z);
        }
    }
    #endregion

    public void squareChangeColor(float renkSinirR, float renkSinirG, float renkSinirB)
    {

        if (squareSpawnedControl && !colorChangedControl)
        {
            for (int i = 0; i < squares.Length; i++)
            {
                if (squares[i] == null) //missing object hatası için yazıldı ancak test edilmedi
                {
                    continue;
                }
                squares[i].GetComponent<Renderer>().material.color = Color.HSVToRGB
                        (
                        Mathf.Repeat((Time.time + renkSinirR) / cycleSeconds, 1f),
                        renkSinirG,     // set to a pleasing value. 0f to 1f
                        renkSinirB      // set to a pleasing value. 0f to 1f
                        );
            }
            colorChangedControl = true;
        }
    }
}
