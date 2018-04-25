﻿using UnityEngine;
using System.Collections.Generic;

public class QuestsListManager : MonoBehaviour {

    public UI_QuestElement questTemplate;
    public Transform parent;
    private List<UI_QuestElement> listElements;

    private void Start()
    {
		
        FillQuestsList(Controller.Instance.User);
    }

    public void FillQuestsList(User user)
    {
        foreach(StateQuest stateQuest in user.Quests.Values)
        {
            UI_QuestElement temp = Instantiate(questTemplate, this.parent);
            temp.linkQuest(stateQuest);
            listElements.Add(temp);
        }
    }
}
