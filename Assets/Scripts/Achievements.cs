using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievements : MonoBehaviour
{
    [SerializeField] string[] achievementsStrings;
    bool[] achievementsBool;

    private void Start()
    {
        achievementsBool = new bool[achievementsStrings.Length];
    }

    public Dictionary<string, bool> GetAchievements()
    {
        Dictionary<string, bool> achievements = new Dictionary<string, bool>();
        
        for (int i=0; i<achievementsBool.Length; i++)
        {
            achievements.Add(achievementsStrings[i], achievementsBool[i]);
        }

        return achievements;
    }

    public void UnlockAchievement(string label)
    {
        for(int i=0; i<achievementsStrings.Length; i++)
        {
            if (achievementsStrings[i].Equals(label))
            {
                achievementsBool[i] = true;
            }
        }
    }
}
