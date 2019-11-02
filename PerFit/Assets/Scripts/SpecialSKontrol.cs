using UnityEngine;

class SpecialSKontrol : ShapeControl
{

    SpecialEffectsControl sEffectsKontrol;

    //private new void Start()
    //{
    //    objeBul();
    //}

    //private new void Update()
    //{
    //    sekilDondur();
    //    paneleSabitle();
    //}

    public override void findObjects()
    {
        base.findObjects();
        sEffectsKontrol = GameObject.FindGameObjectWithTag("sEffectsKontrolTag").GetComponent<SpecialEffectsControl>();
    }

    protected override void OnTriggerEnter(Collider collision)
    {
        base.OnTriggerEnter(collision);

        if (collision.name == "FitPoint")
        {
            StartCoroutine(sEffectsKontrol.specialSquareEffects(gameObject.name));
        }
    }

    private new void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
    }

}
