
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface ICollectable
{
    //All scriptable objects inheriting from ICollectable can be stored in inv
    public void Collect();
}
