using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward
{
    private int money;
    private int awareness;

    public static double lateMultiplier = 0.95;

    public Reward(int money, int awareness)
    {
        this.money = money;
        this.awareness = awareness;
    }

    public int getMoney()
    {
        return money;
    }

    public int getAwareness()
    {
        return awareness;
    }

    public void checkDeadline(int start, int deadline) {
        if(start > deadline) {
            int daysLate = start - deadline;
            double subtractPercentage = (1 - lateMultiplier) * daysLate;

            money = money - (int)(money * (double)subtractPercentage);
            awareness = awareness - (int)(awareness * (double)subtractPercentage);
        }
    }
}
