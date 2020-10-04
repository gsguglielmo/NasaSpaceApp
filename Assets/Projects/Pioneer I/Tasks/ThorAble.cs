using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThorAble : Task
{
    private static int Oid = 202;
    private static int price = 30000000;               //0 Milions
    private static int completionTime = 15;     //Completion time in seconds
    private static string name = "Thor Able";
    private static string description = "The Thor-Able was a launch system and sounding rocket used for a series of satellite launches between 1958 and 1960. It was a two stage rocket, consisting of a Thor IRBM as a first stage and a Vanguard-derived Able second stage.";
    private static ThorAble instance;
    private static bool visible = true;
    private static bool needed = true;
    private static Sprite image = Resources.Load<Sprite>("Sprites/ThorAble");

    public override Reward getReward()
    {
        //Money, Awareness
        return new Reward(5000000,80);
    }

    private ThorAble(Task.Callback c) : base(c)
    {

    }

    public override int getCompletionTime()
    {
        return completionTime;
    }

    public override bool isNeeded()
    {
        return needed;
    }

    public static ThorAble createInstance(Task.Callback c)
    {
        if (instance == null)
        {
            instance = new ThorAble(c);
        }

        return instance;
    }

    public static ThorAble getInstance()
    {
        return instance;
    }

    public override int getPrice()
    {
        return price;
    }

    public override bool isVisible()
    {
        return visible;
    }

    public override int getOID()
    {
        return Oid;
    }

    public override string getDescription()
    {
        return description;
    }

    public override string getName()
    {
        return name;
    }

    public override Sprite getImage() {
        return image;
    }
}
