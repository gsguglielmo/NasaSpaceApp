using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickAction : MonoBehaviour
{
    

    public GameSave.ElementType option;
    public int OID;

    // Start is called before the first frame update
    void Start()
    {
        Button btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(onClick);

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void onClick()
    {
        Debug.Log($"Project: {(int) option}");
        GameController gameController = GameObject.Find("GameController").GetComponent<GameController>();
        GameSave game = gameController.getCurrentGame();

        if ((int)option == 1) {
            Building building = game.findBuilding(OID);

            if (building.isBuilt()) {
                GameObject finestra = FindObject(GameObject.Find("MainCanvas"), "FINESTRA");

                finestra.SetActive(true);
                GameObject template = GameObject.Find("TaskElementTemplate");
                if (template != null) {
                    Debug.Log($"Hiding");
                    template.SetActive(false);
                }

                GameObject mainContainer = GameObject.Find("TaskMainContainer");

                foreach (Transform child in mainContainer.transform) {
                    if (child.gameObject.name == "TaskElement") {
                        GameObject.Destroy(child.gameObject);
                    }

                }


                template = FindObject(mainContainer, "TaskElementTemplate"); ;


                List<Task> tasks = building.getAvailableTasks();

                bool first = true;

                foreach (Task task in tasks) {
                    if (task.isCompleted()) continue;

                    GameObject taskContainer = Instantiate(template);
                    taskContainer.name = "TaskElement";
                    taskContainer.transform.parent = mainContainer.transform;
                    taskContainer.transform.position = template.transform.position;
                    taskContainer.transform.localScale = template.transform.localScale;

                    GameObject canvas = FindObject(taskContainer, "Canvas");

                    Text name = FindObject(taskContainer, "TaskName").GetComponent<Text>();
                    name.text = task.getName();

                    Text cost = FindObject(taskContainer, "TaskCost").GetComponent<Text>();
                    cost.text = $"Cost: {task.getPrice()}";

                    GameObject button = FindObject(taskContainer, "Button");

                    if (task.isAssigned()) {
                        button.SetActive(false);
                    }

                    button.AddComponent<ClickAction>();

                    ClickAction buttonClick = button.GetComponent<ClickAction>();
                    buttonClick.option = GameSave.ElementType.Task;
                    buttonClick.OID = task.getOID();

                    if (!first) {
                        taskContainer.transform.Translate(Vector3.down * 2);
                    }
                    first = false;

                    taskContainer.SetActive(true);


                }



            } else {
                game.researchOrBuild(option, OID);
            }
        } else if ((int) option == 2) {
            Task task = game.findTask(OID);
            gameObject.SetActive(false);
            game.researchOrBuild(option, OID);
        } else {
            game.researchOrBuild(option, OID);
        }

        
    }

    public static GameObject FindObject(GameObject parent, string name) {
        Transform[] trs = parent.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in trs) {
            if (t.name == name) {
                return t.gameObject;
            }
        }
        return null;
    }

}
