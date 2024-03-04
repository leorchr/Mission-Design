using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Camera Data", menuName = "Camera/Create Camera Data", order = 1)]
public class CameraDataScriptableObject : ScriptableObject
{
    public CameraData cData;
}
