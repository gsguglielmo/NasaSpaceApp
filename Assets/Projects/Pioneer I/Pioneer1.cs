using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pioneer1 : Project
{
    private static int Oid = 301;
    private static int price = 30000000;               //30 Milions
    private static int completionTime = 20;            //Completion time in seconds
    private static string name = "Pioneer 1";
    private static string description = "Pioneer 1 was the first space probe launched by NASA. The satellite used a Thor-Able rocket and was launched on 11 October 1958. It was intended to orbit the Moon and make scientific measurements.";
    private static int deadline = 10;                 //Days since 01/10/1958
    private List<Task> tasks;
    //private static Pioneer1 instance;
    private static bool visible = true;
    private static Sprite image = Resources.Load<Sprite>("Sprites/Pioneer1");

    public override Reward getReward()
    {
        //Money, Awareness
        return new Reward(50000000, 80);
    }

    public override Reward getPenalty()
    {
        //Money, Awareness
        return new Reward(0, -100);
    }


    public Pioneer1(Project.Callback c) : base(c)
    {
        initTasks();
    }

    private void initTasks()
    {
        tasks = new List<Task>();
        tasks.Add(JamesVanAllen.getInstance());
        tasks.Add(ThorAble.getInstance());
    }

    public override int getCompletionTime()
    {
        return completionTime;
    }

    public override string getDescription()
    {
        return description;
    }

    public override int getDeadline()
    {
        return deadline;
    }

    public override string getName()
    {
        return name;
    }

    public override int getOID()
    {
        return Oid;
    }

    public override int getPrice()
    {
        return price;
    }

    public override List<Task> getTasks()
    {
        return tasks;
    }

    public override bool isVisible()
    {
        return visible;
    }

    public override Sprite getImage() {
        return image;
    }
}
