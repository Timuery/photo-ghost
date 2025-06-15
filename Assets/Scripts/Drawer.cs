using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Drawer : ScriptToUse
{
    public float smooth = 1.0f;
    public float drawerDistance = 0.5f; // Насколько выдвигается ящик
    public Vector3 openDirection = Vector3.zero; // Теперь public, доступно в Inspector

    private Vector3 closedPosition;
    private Vector3 openPosition;
    public AudioSource asource;
    public AudioClip openDrawer, closeDrawer;

    void Awake()
    {
        // Если в Inspector не задано направление (нулевой вектор), ставим вперед
        if (openDirection == Vector3.zero)
        {
            openDirection = Vector3.forward;
        }
    }

    void Start()
    {
        asource = GetComponent<AudioSource>();
        closedPosition = transform.localPosition;
        openPosition = closedPosition + openDirection.normalized * drawerDistance;
    }

    void Update()
    {
        Vector3 targetPosition = active ? openPosition : closedPosition;
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * 5 * smooth);
    }

    public override void Toggle()
    {
        active = !active;
        asource.clip = active ? openDrawer : closeDrawer;
        asource.Play();
    }
}

