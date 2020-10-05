using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AdventureGame : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textComponent;
    [SerializeField] State startingState;

    State state;
    Achievements achievementsScript;

    bool displayAchievements = false;

    // Start is called before the first frame update
    void Start()
    {
        achievementsScript = GetComponent<Achievements>();

        state = startingState;
        textComponent.text = state.GetStateStory();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForDisplayAchievements();
        if (!displayAchievements)
        {
            ManageState();
            CheckForExit();
        }
    }

    private void CheckForDisplayAchievements()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            displayAchievements = !displayAchievements;
            string achievementText = "ACHIEVEMENTS \n-------------\n";
            foreach(KeyValuePair<string, bool> achievements in achievementsScript.GetAchievements())
            {
                string checkbox = "[" + (achievements.Value ? "x" : " ") + "]";
                achievementText += checkbox + "\t" + achievements.Key;
                achievementText += "\n";
            }
            achievementText += "Press 'a' to return to your quest.";
            textComponent.fontStyle = FontStyles.SmallCaps;
            textComponent.text = achievementText;
        }
    }

    private void CheckForExit()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            state = startingState;
        }
    }

    private void ManageState()
    {
        var nextStates = state.GetNextStates();

        for (int index = 0; index < nextStates.Length; index++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + index) || Input.GetKeyDown(KeyCode.Keypad1 + index))
            {
                state = nextStates[index];
                if (!String.IsNullOrEmpty(state.GetAchievement()))
                {
                    achievementsScript.UnlockAchievement(state.GetAchievement());
                }
            }
        }

        textComponent.fontStyle = FontStyles.Normal;
        textComponent.text = state.GetStateStory();
    }
}
