using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Note 
{
    public string pytanie;
    public string odpowiedz;
    public string ktoPowiedzial;

    public Note(string pytanie, string odpowiedz, string ktoPowiedzial)
    {
        this.pytanie = pytanie;
        this.odpowiedz = odpowiedz;
        this.ktoPowiedzial = ktoPowiedzial;
    }
}
