using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeValue = 0;
    public Text TimeText;

    private void Update()
    {
        timeValue += Time.deltaTime;
        DisplayTime(timeValue);
    }

    private void DisplayTime(float timeToDisplay)
    {
        float Minutes = Mathf.FloorToInt(timeToDisplay / 60);
        timeToDisplay -= Minutes * 60;
        var secondsDisplay = timeToDisplay.ToString("00.00").Replace('.', ':');

        TimeText.text = ($"{Minutes:0}:{secondsDisplay}");
    }
}

