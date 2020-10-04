using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JamesVanAllen : Task
{
    private static int Oid = 201;
    private static int price = 0;               //0 Milions
    private static int completionTime = 10;     //Completion time in seconds
    private static string name = "James Van Allen";
    private static string description = "James Van Allen was a space scientist, physicist and professor at the University of Iowa. His main contribution was the discovery of the Van-Allen radiation belts, which he achieved by using  Geiger-Müller instruments on various satellites in 1958. He certainly was an important figure for scientific advancement in space research.";
    private static JamesVanAllen instance;
    private static bool visible = true;
    private static bool needed = true;
    private static Sprite image = Resources.Load<Sprite>("Sprites/VanAllen");

    private JamesVanAllen(Task.Callback c) : base(c)
    {

    }

    public override Reward getReward()
    {
        //Money, Awareness
        return new Reward(0, 150);
    }

    public override int getCompletionTime()
    {
        return completionTime;
    }

    public override bool isNeeded()
    {
        return needed;
    }

    public static JamesVanAllen createInstance(Task.Callback c)
    {
        if(instance == null)
        {
            instance = new JamesVanAllen(c);
        }

        return instance;
    }

    public static JamesVanAllen getInstance()
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
