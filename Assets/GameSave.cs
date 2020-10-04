using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSave : Building.Callback, Project.Callback, Task.Callback {
    private List<Building> buildings;
    private List<Project> projects;
    private List<Task> tasks;

    public enum ElementType {
        Project = 0, Building = 1, Task = 2
    }

    public string[] months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

    private static int secondsPerDay = 5;
    private static int maxAwareness = 1000;

    private int money;
    private int awareness;

    private int currentDay; //Days since 01/01/1958
    private int secondsRemaining;
    private int inProgressCount;


    //Create new game
    public GameSave() {
        buildings = new List<Building>();
        projects = new List<Project>();
        tasks = new List<Task>();

        tasks.Add(JamesVanAllen.createInstance(this));
        tasks.Add(ThorAble.createInstance(this));

        buildings.Add(new DollyMadisonHouse(this));
        projects.Add(new Pioneer1(this));

        money = 100000000;
        awareness = 500;
        currentDay = 0;
        secondsRemaining = secondsPerDay;
        inProgressCount = 0;
    }

    private bool canRun() {
        int scientists = countScientists();
        if(inProgressCount+1 > scientists) {
            return false;
        }
        return true;
    }

    private int countScientists() {
        int scientists = 0;
        foreach (Building building in buildings) {
            if (building.isBuilt()) {
                scientists++;
            }
        }
        return scientists;
    }

    public void onBuildingCompleted(Building building, Reward reward) {
        Debug.Log($"Completed: {building.getName()} - {building.getOID()}");
        GameObject.Find("EventCompletedController").GetComponent<EventCompletedController>().buildingCompleted(building, reward);

        awareness += reward.getAwareness();
        money += reward.getMoney();
    }

    public void onTaskCompleted(Task task, Reward reward) {
        Debug.Log($"Completed: {task.getName()} - {task.getOID()}");
        inProgressCount--;
        GameObject.Find("EventCompletedController").GetComponent<EventCompletedController>().taskCompleted(task, reward);

        awareness += reward.getAwareness();
        money += reward.getMoney();
    }

    public void onLaunchCompleted(Project project, bool success, Reward reward) {
        if (success) {
            Debug.Log($"Completed: {project.getName()} - {project.getOID()}");
            GameObject.Find("EventCompletedController").GetComponent<EventCompletedController>().launchSuccessful(project, reward);
        } else {
            Debug.Log($"Failed: {project.getName()} - {project.getOID()}");
            GameObject.Find("EventCompletedController").GetComponent<EventCompletedController>().launchFailed(project, reward);

        }

        awareness += reward.getAwareness();
        money += reward.getMoney();
    }

    public void researchOrBuild(ElementType type, int OID) {
        switch ((int)type) {
            case 0://Project
                Project project = findProject(OID);
                if (project != null) {
                    if(money >= project.getPrice()) {
                        money -= project.getPrice();
                        project.startMission(currentDay);
                    }
                }
                break;
            case 1://Building
                Building building = findBuilding(OID);
                if (building != null) {
                    if(money >= building.getPrice()) {
                        money -= building.getPrice();
                        building.startBuilding();
                    }
                    
                }
                break;
            case 2://Task
                Task task = findTask(OID);
                if (task != null) {
                    if(money >= task.getPrice()) {
                        if (canRun()) {
                            money -= task.getPrice();
                            inProgressCount++;
                            task.assignTask();
                        } else {
                            Debug.Log($"Not enough scientists");
                        }
                        
                    }
                } else {
                    Debug.Log($"Task not found!");
                }
                break;
        }
    }

    public Building findBuilding(int OID) {
        foreach (Building building in buildings) {
            if (building.getOID() == OID) {
                return building;
            }
        }

        return null;
    }

    private Project findProject(int OID) {
        foreach (Project project in projects) {
            if (project.getOID() == OID) {
                return project;
            }
        }

        return null;
    }

    public Task findTask(int OID) {
        foreach (Task task in tasks) {
            if (task.getOID() == OID) {
                return task;
            }
        }

        return null;
    }

    //Notify all elements that a second has passed
    public void secondTick() {
        if (secondsRemaining > 0) {
            secondsRemaining--;
        } else {
            currentDay++;
            secondsRemaining = secondsPerDay;
        }

        buildings.ForEach(delegate (Building building) {
            building.secondTick();
        });

        projects.ForEach(delegate (Project project) {
            project.secondTick();
        });

        tasks.ForEach(delegate (Task task) {
            task.secondTick();
        });
    }

    public void frameTick() {
        QuantityFiller filler = GameObject.Find("awarenessProgress").GetComponent<QuantityFiller>();

        if (awareness > maxAwareness) awareness = maxAwareness;

        int percentage = (int)(((double)awareness / (double)maxAwareness) * 100);

        filler.percentage = percentage;

        TextFormatter money = GameObject.Find("TotalMoney").GetComponent<TextFormatter>();
        money.number = this.money;

        TextFormatter scientists = GameObject.Find("Level").GetComponent<TextFormatter>();
        scientists.number = countScientists();

        Text date = GameObject.Find("CurrentDate").GetComponent<Text>();

        DateTime referenceDate = new DateTime(1958, 10, 01);
        DateTime currentDate = referenceDate.AddDays(currentDay);

        date.text = $"{currentDate.Day} {months[currentDate.Month-1]} {currentDate.Year}";

        TextFormatter taskTime = GameObject.Find("Task time").GetComponent<TextFormatter>();
        taskTime.number = getNearestCompletionTime();
    }

    public int getNearestCompletionTime() {

        int smallest = 99999;

        foreach (Building building in buildings) {
            
            if(smallest > building.buildingTimeRemaining() && building.buildingInProgress()) {
                smallest = building.buildingTimeRemaining();
            }

        }

        foreach (Project project in projects) {
            if (smallest > project.getRemainingTime() && project.isMissionStarted()) {
                smallest = project.getRemainingTime();
            }
        }

        foreach (Task task in tasks) {
            if (smallest > task.getRemainingTime() && task.isAssigned()) {
                smallest = task.getRemainingTime();
            }
        }

        return smallest == 99999 ? 0:smallest;
    }
}
