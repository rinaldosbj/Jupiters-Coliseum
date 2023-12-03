using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoard : MonoBehaviour
{
    public Text timeText;
    private string time;
    private int runNumber;
    private string playerName = "Teste";

    void Start()
    {
        runNumber = PlayerPrefs.GetInt("Run") + 1;
        PlayerPrefs.SetInt("Run", runNumber);
    }

    private void Update()
    {
        time = timeText.text;
        if (Input.GetKeyDown(KeyCode.Y))
        {
            wonBattle();
            logRunsWons();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            clear();
        }
    }

    public void wonBattle()
    {
        int runsWons = PlayerPrefs.GetInt("RunWons") + 1;
        PlayerPrefs.SetInt("RunWons",runsWons);
        string[] currentTimeSplited = time.Split(":");
        int currentMinutes = int.Parse(currentTimeSplited[0]);
        int currentSeconds = int.Parse(currentTimeSplited[1]);
        int currentMiliseconds = int.Parse(currentTimeSplited[2]);

        int count = 1;
        while (count <= 3)
        {
            if (PlayerPrefs.GetString($"Won{count}") == "")
            {
                PlayerPrefs.SetString($"Won{count}", $"{runNumber} {playerName}: {time}");
                break;
            }
            string currentRecord = PlayerPrefs.GetString($"Won{count}");
            string[] splitedString = currentRecord.Split(" ");
            string[] timeSplited = splitedString[2].Split(":");
            int minutes = int.Parse(timeSplited[0]);
            int seconds = int.Parse(timeSplited[1]);
            int miliseconds = int.Parse(timeSplited[2]);

            if (currentMinutes < minutes)
            {
                poeNoRank(count);
                break;
            }
            else if (currentMinutes == minutes)
            {
                if (currentSeconds < seconds)
                {
                    poeNoRank(count);
                    break;
                }
                else if (currentSeconds == seconds)
                {
                    if (currentMiliseconds < miliseconds)
                    {
                        poeNoRank(count);
                        break;
                    }
                    else if (currentMiliseconds == miliseconds)
                    {
                        poeNoRank(count);
                        break;
                    }
                }
            }

            count++;
        }

    }

    private void poeNoRank(int count)
    {
        if (count == 3)
        {
            PlayerPrefs.SetString("Won3", $"{runNumber} {playerName}: {time}");
        }
        else if (count == 2)
        {
            if (PlayerPrefs.GetString("Won2") != "")
            {
                PlayerPrefs.SetString("Won3", $"{PlayerPrefs.GetString("Won2")}");
            }
            PlayerPrefs.SetString("Won2", $"{runNumber} {playerName}: {time}");
        }
        else if (count == 1)
        {
            if (PlayerPrefs.GetString("Won2") != "")
            {
                PlayerPrefs.SetString("Won3", $"{PlayerPrefs.GetString("Won2")}");
            }
            PlayerPrefs.SetString("Won2", $"{PlayerPrefs.GetString("Won1")}");
            PlayerPrefs.SetString("Won1", $"{runNumber} {playerName}: {time}");
        }
    }

    private void logRunsWons()
    {
        int runsCount = PlayerPrefs.GetInt("RunWons");
        if (runsCount > 3)
        {
            runsCount = 3;
        }
        while (runsCount > 0)
        {
            Debug.Log($"{runsCount} lugar: {PlayerPrefs.GetString($"Won{runsCount}")}");
            runsCount--;
        }
    }

    private void clear()
    {
        PlayerPrefs.DeleteAll();
        runNumber = PlayerPrefs.GetInt("Run") + 1;
        PlayerPrefs.SetInt("Run", runNumber);
    }
}
