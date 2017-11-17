using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sentence {

    public int id;
    public string text;
    public int answerId;

    public Sentence() { }

    public Sentence( int id, string text, int answerId)
    {
        this.id = id;
        this.text = text;
        this.answerId = answerId;
    }

}
