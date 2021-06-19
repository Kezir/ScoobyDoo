using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestionNPC
{
    public string pytanie;
    public int chmurkaPytanie;
    public string odpowiedz;
    public int chmurkaOdpowiedz;
    //public int wymaganyPoziom;
    public int[] wymaganyPoziom;
    //public bool progress;
    public bool[] progressList;
    public string nazwaNPC;
    public int storyUpdate;
}
