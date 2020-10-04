using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Task
{
    public interface Callback
    {
        void onTaskCompleted(Task task,Reward reward);
    }

    private bool _isCompleted;
    private bool _isAssigned;
    private int  _timeRemaining;
    private Task.Callback callback;

    public Task(Task.Callback callback)
    {
        this.callback = callback;
    }

    abstract public int getPrice();
    abstract public bool isNeeded();
    abstract public int getCompletionTime();  
    public abstract int getOID();
    public abstract string getName();
    public abstract string getDescription();
    public abstract bool isVisible();
    public abstract Sprite getImage();
    public abstract Reward getReward();
    public int getRemainingTime()
    {
        return _timeRemaining;
    }

    public bool isCompleted()
    {
        return _isCompleted;
    }

    public bool isAssigned()
    {
        return _isAssigned;
    }


    public void secondTick()
    {
        if(!_isCompleted && _isAssigned)
        {
            if(_timeRemaining > 1)
            {
                _timeRemaining--;
            }
            else
            {
                _isAssigned = false;
                _isCompleted = true;
                if(callback != null)
                {
                    callback.onTaskCompleted(this,getReward());
                }
            }
        }
        //Debug.Log($"Remaining: {_timeRemaining} {getName()} - {getOID()}");
    }

    public bool assignTask()
    {
        if (_isCompleted || _isAssigned) return false;

        _timeRemaining = getCompletionTime();
        _isAssigned = true;

        Debug.Log($"Assigned: {getName()} - {getOID()}");

        return true;
    }
        

    //TODO Add reward
}
