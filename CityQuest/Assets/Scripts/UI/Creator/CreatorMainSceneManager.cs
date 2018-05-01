﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CreatorMainSceneManager : MonoBehaviour
{
    public InputField questNameInputField;

    public InputField positionLatInputField;
    public InputField positionLongInputField;

    public InputField questDescriptionInputField;
    public InputField questValueInputField;

    public CreatorCheckpointManager checkpointTemplate;

    protected List<CreatorCheckpointManager> allCheckpoints = new List<CreatorCheckpointManager>();

    public void Btn_StartNewQuest()
    {
        this.gameObject.SetActive(false);
        AddNewCheckpoint(0);
    }

    public void AddNewCheckpoint(int index)
    {
        if (allCheckpoints.Count != 0)
        {
            allCheckpoints.Last().gameObject.SetActive(false);
        }
        CreatorCheckpointManager temp = Instantiate(checkpointTemplate);
        temp.SetIndex(index, this);
        allCheckpoints.Add(temp);
    }

    public void Btn_UseCurrentLoc()
    {
        if (GeoManager.Instance.IsLoaded())
        {
            Vector2 loca = GeoManager.Instance.GetUserPosition();
            positionLatInputField.text = loca.x.ToString();
            positionLongInputField.text = loca.y.ToString();
        }
    }

    public void ValidateQuest()
    {
        Controller.Instance.CreateNewQuest(
            questNameInputField.text,
            questDescriptionInputField.text,
            questValueInputField.text,
            positionLatInputField.text,
            positionLongInputField.text,
            ToCheckPoints()); // Pass checkpoints
    }

    public List<CheckPoint> ToCheckPoints()
    {
        List<CheckPoint> MyCheckPoints = new List<CheckPoint>();
        foreach (CreatorCheckpointManager creatorCheckpoint in allCheckpoints)
        {
            List<string> choices = new List<string>
            {
                creatorCheckpoint.FirstAnswerInputField.text,
                creatorCheckpoint.SecondAnswerInputField.text,
                creatorCheckpoint.ThirdAnswerInputField.text
            };

            CheckPoint temp = new CheckPoint(
                creatorCheckpoint.FurnishedImage.ToString(), // Pass image as string 
                creatorCheckpoint.EnigmaInputField.text,
                choices,
                creatorCheckpoint.Answer);  
            MyCheckPoints.Add(temp);
        }

        return MyCheckPoints;
    }

}