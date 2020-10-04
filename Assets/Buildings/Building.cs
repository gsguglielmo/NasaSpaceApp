using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building {
    public interface Callback {
        void onBuildingCompleted(Building building, Reward reward);
    }

    private bool _built;
    private bool _buildingInProgress;
    private int _buildingTimeRemaining; //Time remaining in seconds -1 if null
    private Building.Callback callback;

    public Building(Building.Callback callback) {
        _built = false;
        _buildingInProgress = false;
        _buildingTimeRemaining = -1;
        this.callback = callback;
    }

    public Building(bool built, bool buildingInProgress, int buildingTimeRemaining, Building.Callback callback) {
        _built = built;
        _buildingInProgress = buildingInProgress;
        _buildingTimeRemaining = buildingTimeRemaining;
        this.callback = callback;
    }

    public abstract int getOID();
    public abstract string getName();
    public abstract string getDescription();
    public abstract List<Task> getAvailableTasks();
    public abstract int buildingTotalTime();
    public abstract Reward getReward();
    public abstract int getPrice();
    public abstract Sprite getImage();
    public abstract bool isVisible();

    public bool isBuilt() {
        return _built;
    }
    public bool buildingInProgress() {
        return _buildingInProgress;
    }

    public int buildingTimeRemaining() {
        return _buildingTimeRemaining;
    }

    public void secondTick() {
        if (!_built && _buildingInProgress) {
            if (_buildingTimeRemaining > 1) {
                _buildingTimeRemaining--;
            } else {
                _built = true;
                _buildingInProgress = false;
                if (callback != null) {
                    callback.onBuildingCompleted(this, getReward());
                }

            }

        }
    }

    public bool startBuilding() {
        //If already built return false
        if (_built || _buildingInProgress) return false;

        //Start building and set the timer
        _buildingTimeRemaining = buildingTotalTime();
        _buildingInProgress = true;

        Debug.Log($"Building started: {getName()} - {getOID()}");

        return true;
    }
}
