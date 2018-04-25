﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class User
{
    private static long instanceCounter = 0;
    private string id;
    private string username;
    private long xp;
    private List<Badge> badges;


    /// <summary>
    /// The Status of the quests began by the user.
    /// The key is the ID of the quest, the value is the state associated to this quest.
    /// </summary>
    private Dictionary<long, StateQuest> quests;

    public User(string name)
    {
        username = name;
        id = instanceCounter++.ToString() + "@" + username;
        xp = 0;
        badges = new List<Badge>();
        quests = new Dictionary<long, StateQuest>();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="User"/> class based on a previous instance.
    /// </summary>
    /// <param name="n">The name.</param>
    /// <param name="id">The identifier.</param>
    /// <param name="xp">The xp.</param>
    /// <param name="b">The badges.</param>
    /// <param name="q">The quests.</param>
    public User(string n, string id, long xp, List<Badge> b, Dictionary<long, StateQuest> q)
    {
        this.username = n;
        this.id = id;
        this.xp = xp;
        this.badges = b;
        this.quests = q;
    }


    /// <summary>
    /// Initializes a new instance of the <see cref="User"/> class based on a previous instance.
    /// </summary>
    /// <param name="u">The user.</param>
    public User(User u)
    {
        this.username = u.Username;
        this.id = u.Id;
        this.xp = u.Xp;
        this.badges = u.Badges;
        this.quests = u.Quests;
    }

    /// <summary>
    /// Checks the quest and modifies the users xp accordingly to the status of the quest.
    /// </summary>
    /// <param name="q">The quest.</param>
    public void CheckQuest(Quest q)
    {
        if (quests.ContainsKey(q.Id))
        //if the quest has begun
        {
            StateQuest sq = quests[q.Id];
            //if the quest is currently done we can lose xp if it has changed (x-1)
            //if the quest is unaccomplished, we can win xp by finishing it (x1)
            int done_coeff = sq.Done ? -1 : 1;
            if (sq.Done != sq.CheckQuest())
            //if the status has changed xp must change
            {
                xp += sq.Quest.Value * done_coeff;
            }
        }
    }

    public string Id
    {
        get { return id; }
    }

    public string Username
    {
        get { return username; }
        set { username = value; }
    }

    public long Xp
    {
        get { return xp; }
    }

    public List<Badge> Badges
    {
        get { return badges; }
    }

    public Dictionary<long, StateQuest> Quests
    {
        get { return quests; }
    }

    protected bool Equals(User other)
    {
        return string.Equals(id, other.id);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((User) obj);
    }

    public override int GetHashCode()
    {
        return (id != null ? id.GetHashCode() : 0);
    }

}