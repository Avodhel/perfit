using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class SquareKontrol : SekilKontrol {

    private new void Start()
    {
        objeBul();
        sekilDonmeYonu();
    }

    private new void Update()
    {
        sekilDondur();
        paneleSabitle();
    }

    private new void OnTriggerEnter(Collider collision)
    {
        base.OnTriggerEnter(collision);
    }

    private new void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
    }

    //private new void OnCollisionEnter(Collision col)
    //{
    //    base.OnCollisionEnter(col);
    //}
}
