using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TransformSetter
{
    public Vector3 pos;
    public Quaternion rot;
    public Vector3 scale;

    TransformSetter(Vector3 pos, Quaternion rot, Vector3 scale)
    {
        this.pos = pos;
        this.rot = rot;
        this.scale = scale;
    }
}

public class Utilities : MonoBehaviour
{
    public static Quaternion QI => Quaternion.identity;
}
