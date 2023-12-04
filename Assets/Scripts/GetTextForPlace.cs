using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetTextForPlace : MonoBehaviour
{
    public int place;
    public Text textLeaderboard;
    void Start()
    {
        textLeaderboard.text = PlayerPrefs.GetString($"Won{place}").ToString();
    }
}
