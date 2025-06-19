using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
[RequireComponent(typeof(AudioSource))]
public class PC : ScriptToUse
{
    


    [Header("PC UI Settings")]
    [SerializeField] private GameObject canvas;
    public List<TMP_InputField> textsUGUI;
    public List<string> needTexts;

    private AudioSource source;
    public AudioClip clip;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        source.clip = clip;
    }
    public override void Toggle()
    {
        active = !active;
        canvas.SetActive(active);


        PlayerController controller = FindFirstObjectByType<PlayerController>();
        if (active) controller.effectController.AddEffect(PlayerEffect.InUI);
        controller.effectController.RemoveEffect(PlayerEffect.Photo);

        if (!active)
        {
            controller.effectController.RemoveEffect(PlayerEffect.InUI);
        }


        // Можно добавить здесь логику блокировки управления игроком
        Debug.Log($"PC UI {(active ? "opened" : "closed")}");
    }
    public void CheckData()
    {
        for (int i = 0; i < textsUGUI.Count; i++)
        {
            if (textsUGUI[i].text != needTexts[i])
            {
                source.Play();
                foreach(var t in textsUGUI)
                {
                    t.text = null;
                }
                break;
            }
        }
        Debug.Log("YOU ARE FUCKING LUCKING WIIIINE");
    }
    public void CloseUI()
    {
        active = false;
        canvas.SetActive(false);
    }
}
