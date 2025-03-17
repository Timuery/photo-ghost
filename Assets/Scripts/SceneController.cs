using UnityEngine;

public class SceneController : MonoBehaviour
{
    private GameObject player;
    private GameObject ghost;

    [HideInInspector] public UIController UIcontroller;
    private void Start()
    {
        FindPlayer();
        FindCanvas();
    }
    private void FindPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player not found in the scene");
        }
        player.GetComponent<PlayerController>()._mainController = this;
    }

    private void FindGhost()
    {
        ghost = GameObject.FindGameObjectWithTag("Ghost");
        if (ghost == null)
        {
            Debug.LogError("Ghost not found in the scene");
        }
    }

    private void FindCanvas()
    {
        GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");
        if (canvas == null)
        {
            Debug.LogError("Canvas not found in the scene");
        }
        UIcontroller = canvas.GetComponent<UIController>();
        if (UIcontroller == null)
        {
            Debug.LogError("UIController not found on the Canvas");
        }
        Debug.Log(canvas.name);
        Debug.Log(UIcontroller.transform.name + "UI");
    }


}
