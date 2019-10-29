using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialSquarePooler : ObjectPooler {

    public static SpecialSquarePooler SharedInstance { get; private set; }

    public override void Awake()
    {
        SharedInstance = this;
        base.Awake();
    }
}
