using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventCompletedController : MonoBehaviour
{

    private class TaskToDisplay {
        public string name;
        public string description;
        public Sprite image;
        public Reward reward;
        public bool success;

        public TaskToDisplay(string name, string description, Sprite image,bool success, Reward reward) {
            this.name = name;
            this.description = description;
            this.image = image;
            this.reward = reward;
            this.success = success;
        }
    }

    private List<TaskToDisplay> toDisplay = new List<TaskToDisplay>();
    private bool displaying = false;

    public GameObject windowToClose;
    public GameObject taskCompletedWindow;
    public GameObject taskFailedWindow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!displaying && toDisplay.Count > 0) {
            display(toDisplay[0]);
            toDisplay.RemoveAt(0);
        }
    }

    public void taskCompleted(Task task, Reward reward) {
        toDisplay.Add( new TaskToDisplay(task.getName(),task.getDescription(),task.getImage(),true,reward) );
    }

    public void buildingCompleted(Building building, Reward reward) {
        toDisplay.Add(new TaskToDisplay(building.getName(), building.getDescription(), building.getImage(), true, reward));
    }

    public void launchSuccessful(Project project, Reward reward) {
        toDisplay.Add(new TaskToDisplay(project.getName(), project.getDescription(), project.getImage(), true, reward));

    }

    public void launchFailed(Project project, Reward reward) {
        toDisplay.Add(new TaskToDisplay(project.getName(), project.getDescription(), project.getImage(), false, reward));

    }

    private void display(TaskToDisplay toDisplay) {
        hideTaskWindow();
        displaying = true;

        GameObject subject = taskCompletedWindow;
        if (!toDisplay.success) {
            subject = taskFailedWindow;
        }

        GameObject canvas = ClickAction.FindObject(subject, "Canvas");

        Text taskName = ClickAction.FindObject(canvas, "TaskName").GetComponent<Text>();
        Text description = ClickAction.FindObject(canvas, "TaskDescription").GetComponent<Text>();
        Text reward = ClickAction.FindObject(canvas, "TaskReward").GetComponent<Text>();
        Image image = ClickAction.FindObject(canvas, "TaskImage").GetComponent<Image>();

        taskName.text = toDisplay.name;
        description.text = toDisplay.description;
        reward.text = $"+ {toDisplay.reward.getMoney()}";
        image.sprite = toDisplay.image;

        subject.SetActive(true);
    }

    public void hideCompleted() {
        taskCompletedWindow.SetActive(false);
        taskFailedWindow.SetActive(false);
        displaying = false;
    }

    private void hideTaskWindow() {
        windowToClose.SetActive(false);
    }
}
