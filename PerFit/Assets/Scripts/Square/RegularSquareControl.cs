using UnityEngine;

class RegularSquareControl : SquareControl{

    private new void OnTriggerEnter(Collider collision)
    {
        base.OnTriggerEnter(collision);
    }

    private new void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
    }

}
