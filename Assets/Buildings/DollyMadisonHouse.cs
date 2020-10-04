using System.Collections.Generic;
using UnityEngine;

public class DollyMadisonHouse : Building
{
    private static int Oid = 101;
    private static int price = 5000000; //20 Milions
    private static int buildTime = 1;     //Build time in seconds
    private static string name = "The Dolly Madison House";
    private static string description = "Built in 1820 by Richard Cutts, in 1837 it became the home of Mrs.Dolly Payne Madison, wife of President James Madison.Named the Dolly Madison House, she lived there till her death in 1849. In 1886 the Dolly Madison House became the private Cosmos Club.The Dolly Madison House served as NASA Headquarters from 1958 until October 1961.";
    private List<Task> tasks;
    private static bool visible = true;
    private static Sprite image = Resources.Load<Sprite>("Sprites/DollyMadisonHouse");

    public override Reward getReward()
    {
        //Money, Awareness
        return new Reward(0, 120);
    }

    public DollyMadisonHouse(Building.Callback callback) : base(callback)
    {
        initTasks();
    }

    public DollyMadisonHouse(bool built, bool buildingInProgress, int buildingTimeRemaining, Building.Callback callback) : base(built, buildingInProgress, buildingTimeRemaining, callback)
    {
        initTasks();
    }

    private void initTasks()
    {
        tasks = new List<Task>();
        tasks.Add(JamesVanAllen.getInstance());
        tasks.Add(ThorAble.getInstance());
    }


    public override int buildingTotalTime()
    {
        return buildTime;
    }

    public override string getDescription()
    {
        return description;
    }

    public override string getName()
    {
        return name;
    }

    public override int getPrice()
    {
        return price;
    }

    public override List<Task> getAvailableTasks()
    {
        return tasks;
    }

    public override bool isVisible()
    {
        return visible;
    }

    public override int getOID()
    {
        return Oid;
    }
    public override Sprite getImage() {
        return image;
    }
}
