using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    private GameObject player;
    private GameObject ghost;

    public UIController UIcontroller;
    public ItemManager _ItemManager;

    public static SceneController Instance {  get; private set; }

    private void Awake()
    {
        if (Instance == null)
        Instance = this;
    }

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
            Debug.Log("Ghost not found in the scene");
        }
    }

    private void FindCanvas()
    {
        Debug.Log(UIcontroller.name);
        Debug.Log(UIcontroller.transform.name + "UI");
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
