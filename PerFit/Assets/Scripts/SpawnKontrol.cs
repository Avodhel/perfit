using System.Collections;
using UnityEngine;

public class SpawnKontrol : MonoBehaviour {

    //[Header("Square")]
    //public GameObject sekil;
    //[Header("Special Squares")]
    //public GameObject[] ozelSekiller;
    //[Header("Lottery Square")]
    //public GameObject lottery;

    GameObject ozelSekil;
    //GameObject square;
    GameObject[] squares;

    [Header("Spawn Time")]
    [Range(1, 50)]
    public int spawnSuresi;

    [Header("Frequency for Special Squares")]
    [Range(0, 10)]
    public int minSekildeBirOzelSekil;
    [Range(0, 10)]
    public int maxSekildeBirOzelSekil;
    [Range(0, 20)]
    public int minSekildeBirLottery;
    [Range(0, 20)]
    public int maxSekildeBirLottery;

    int kacSekildeBirOzelSekil;
    int kacSekildeBirLottery;
    int sekilCount = 0;
    int countForLottery = 0;
    bool squareOlustuKontrol = false;
    bool renkDegistiKontrol = false;

    [HideInInspector]
    public bool squareRainSpawnKontrol = false;
    [HideInInspector]
    public float cycleSeconds = 500f;

    void Awake()
    {
        kacSekildeBirOzelSekil = Random.Range(minSekildeBirOzelSekil, maxSekildeBirOzelSekil);
        kacSekildeBirLottery = Random.Range(minSekildeBirLottery, maxSekildeBirLottery);
    }

    void Start ()
    {        
        StartCoroutine(sekilOlustur());  
    }

    IEnumerator sekilOlustur()
    {
        while (true)
        {
            if (!squareRainSpawnKontrol)
            {
                sekilCount += 1;
                countForLottery += 1;
            }

            if (countForLottery == kacSekildeBirLottery & !squareRainSpawnKontrol) //spawn lottery
            {
                //Instantiate(lottery,
                //            transform.position,
                //            Quaternion.Euler(transform.rotation.x, Random.Range(-360f, 360f), transform.rotation.z));
                GameObject lotterySquare = SquarePooler.SharedInstance.GetPooledObject(1);
                lotterySquare.SetActive(true);
                lotterySquare.transform.position = transform.position;
                lotterySquare.transform.rotation = Quaternion.Euler(transform.rotation.x, Random.Range(-360f, 360f), transform.rotation.z);

                countForLottery = 0;
                kacSekildeBirLottery = Random.Range(minSekildeBirLottery, maxSekildeBirLottery);

                while (sekilCount == kacSekildeBirOzelSekil) //eger lottery, ozel sekille cakisirsa
                {
                    sekilCount = 0;
                    kacSekildeBirOzelSekil = Random.Range(minSekildeBirOzelSekil, maxSekildeBirOzelSekil);
                }
            }
            else if (sekilCount == kacSekildeBirOzelSekil & !squareRainSpawnKontrol)
            {
                //ozelSekil = ozelSekiller[Random.Range(0, ozelSekiller.Length)];
                //Instantiate(ozelSekil,
                //            transform.position,
                //            Quaternion.Euler(transform.rotation.x, Random.Range(-360f, 360f), transform.rotation.z));
                GameObject specialSquare = SpecialSquarePooler.SharedInstance.GetPooledObject(Random.Range(0, SpecialSquarePooler.SharedInstance.itemsToPool.Count));
                specialSquare.SetActive(true);
                specialSquare.transform.position = transform.position;
                specialSquare.transform.rotation = Quaternion.Euler(transform.rotation.x, Random.Range(-360f, 360f), transform.rotation.z);

                sekilCount = 0;
                kacSekildeBirOzelSekil = Random.Range(minSekildeBirOzelSekil, maxSekildeBirOzelSekil);

                if (specialSquare.name == "squareRain")
                {
                    //Debug.Log("squareRain spawnlandı");
                    squareRainSpawnKontrol = true;

                    //countForLottery = 0;
                }

            }
            else if (sekilCount < kacSekildeBirOzelSekil & countForLottery < kacSekildeBirLottery)
            {
                //Instantiate(sekil,
                //            transform.position,
                //            Quaternion.Euler(transform.rotation.x, Random.Range(-360f, 360f), transform.rotation.z));
                GameObject square = SquarePooler.SharedInstance.GetPooledObject(0);
                square.SetActive(true);
                square.transform.position = transform.position;
                square.transform.rotation = Quaternion.Euler(transform.rotation.x, Random.Range(-360f, 360f), transform.rotation.z);
            }

            squares = GameObject.FindGameObjectsWithTag("squareTag");

            squareOlustuKontrol = true;
            renkDegistiKontrol = false;

            yield return new WaitForSeconds(spawnSuresi);

        }
    }


    public void sekilRenkDegistir(float renkSinirR, float renkSinirG, float renkSinirB)
    {

        if (squareOlustuKontrol && !renkDegistiKontrol)
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
            renkDegistiKontrol = true;
        }
    }
}
