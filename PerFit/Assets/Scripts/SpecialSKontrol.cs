using UnityEngine;

class SpecialSKontrol : SekilKontrol
{

    SEffectsKontrol sEffectsKontrol;

    private new void Start()
    {
        objeBul();
        sekilDonmeYonu();
        sekilBoyutAyarla();
    }

    private new void Update()
    {
        sekilDondur();
        paneleSabitle();
    }

    public override void objeBul()
    {
        base.objeBul();
        sEffectsKontrol = GameObject.FindGameObjectWithTag("sEffectsKontrolTag").GetComponent<SEffectsKontrol>();
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
