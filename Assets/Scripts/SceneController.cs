using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    private GameObject player;
    private GameObject ghost;

    public UIController UIcontroller;
    public ItemManager _ItemManager;



    private void Start()
    {
        _ItemManager = GetComponent<ItemManager>();
        FindPlayer();
        FindCanvas();
        FindGhost();
    }
    private void FindPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player not found in the scene");
        }
        PlayerController pl = player.GetComponent<PlayerController>();
        pl._mainController = this;

        
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

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
