using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Project {

    public interface Callback {
        void onLaunchCompleted(Project project, bool success, Reward reward);
    }

    private bool _isCompleted;
    private bool _preparationStarted;
    private bool _willBeSuccessful;
    private int _timeRemaining;
    private int _missionStartDate;
    private Project.Callback callback;

    public Project(Callback c) {
        callback = c;
    }

    public abstract List<Task> getTasks();
    public abstract int getPrice();
    public abstract int getCompletionTime();

    public abstract int getOID();
    public abstract string getName();
    public abstract string getDescription();
    public abstract Reward getReward();
    public abstract Reward getPenalty();
    public abstract bool isVisible();
    public abstract Sprite getImage();

    //TODO Add reward
    public abstract int getDeadline();


    public int getRemainingTime() {
        return _timeRemaining;
    }

    public bool startMission(int date) {
        if (_isCompleted || _preparationStarted) return false;

        _timeRemaining = getCompletionTime();

        List<Task> tasks = getTasks();

        _willBeSuccessful = true;

        tasks.ForEach(delegate (Task task) {
            if (task == null) return;
            if (task.isNeeded() && !task.isCompleted()) {
                _willBeSuccessful = false;
            }
        });

        _preparationStarted = true;

        _missionStartDate = date;

        Debug.Log($"Project started: [{_willBeSuccessful}] {getName()} - {getOID()}");

        return true;
    }

    public bool isMissionStarted() {
        return _preparationStarted;
    }

    public void secondTick() {
        if (!_isCompleted && _preparationStarted) {
            if (_timeRemaining > 1) {
                _timeRemaining--;
            } else {
                _preparationStarted = false;
                Reward r = getPenalty();
                if (_willBeSuccessful) {
                    _isCompleted = true;
                    r = getReward();
                }
                if (callback != null) {
                    r.checkDeadline(_missionStartDate, getDeadline());
                    callback.onLaunchCompleted(this, _willBeSuccessful, r);
                }

            }

        }
    }


}