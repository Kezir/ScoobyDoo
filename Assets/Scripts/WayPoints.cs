using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WayPoints
{
    public Transform point;
    public enum whatToDO
    {
        wait,
        work,
        nothing
    }
    public whatToDO whatToDo;
    public float timeToWait;
}
