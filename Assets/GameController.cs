using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    float elapsed = 0f;
    GameSave game;

    void Start()
    {
        game = new GameSave();


    }

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed >= 1f)
        {
            elapsed = elapsed % 1f;
            secondTick();
        }
        game.frameTick();
    }

    public GameSave getCurrentGame()
    {
        return game;
    }

    public void test()
    {
        Debug.Log($"Test");
    }
    
    void secondTick()
    {
        game.secondTick();
    }

    /*
    public void onBuildingCompleted(Building building, Reward reward)
    {
        Debug.Log($"Completed: {building.getName()} - {building.getOID()}");
    }

    public void onTaskCompleted(Task task, Reward reward)
    {
        Debug.Log($"Completed: {task.getName()} - {task.getOID()}");

        if(this.task.isCompleted() && this.task2.isCompleted())
        {
            project.startMission();
        }
    }

    public void onLaunchCompleted(Project project, bool success, Reward reward)
    {
        if (success)
        {
            Debug.Log($"Completed: {project.getName()} - {project.getOID()}");
        }
        else
        {
            Debug.Log($"Failed: {project.getName()} - {project.getOID()}");
        }
    }*/
}
