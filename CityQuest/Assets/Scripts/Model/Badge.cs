﻿


public class Badge
{
    private static long instanceCounter = 0;
    private long id;
    private string name;
    private string description;
    private long earn;

    public Badge(string n, string d, long v)
    {
        id = instanceCounter++;
        name = n;
        description = d;
        earn = v;
    }

    public long Id
    {
        get { return id; }
    }

    public string Name
    {
        get { return name; }
    }

    public string Description
    {
        get { return description; }
    }

    public long Earn
    {
        get { return earn; }
    }
    public override string ToString()
    {
        return "Badge : id : " + id + ", name : " + name + ", description : " + description + ", earn : " + earn;
    }

}