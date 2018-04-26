﻿
using UnityEngine;

/// <summary>
/// 
/// </summary>
/// <seealso cref="State" />
public class DefaultState : State
{
    protected Controller controller;

    public DefaultState(Controller c)
    {
        controller = c;
    }

    /// <summary>
    /// Returns to the map page
    /// </summary>
    /// <seealso cref="MapState" />
    public virtual void ReturnAction()
    {
        controller.Transition(new MapState(controller));
    }

    public virtual void OptionAction() { }

    public virtual void LoginLocalAction() { }
    public virtual void LoginServerAction() { }
    public virtual void InscriptionAction() { }
    public virtual void SelectionQuestInHistoricAction() { }

    public virtual void StartQuestAction() { }
}