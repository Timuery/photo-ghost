using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DoorScript
{
    [RequireComponent(typeof(AudioSource))]
    public class Drawer : MonoBehaviour
    {
        public bool open;
        public float smooth = 1.0f;
        public float drawerDistance = 0.5f; // Насколько выдвигается ящик
        public float directionMultiplier = 1.0f; // 1 – вперед, -1 – назад
        private Vector3 closedPosition;
        private Vector3 openPosition;
        public AudioSource asource;
        public AudioClip openDrawer, closeDrawer;

        void Start()
        {
            asource = GetComponent<AudioSource>();
            closedPosition = transform.localPosition;

            // Используем локальную ось родителя (шкафа), а не transform.forward
            Vector3 drawerDirection = transform.parent.TransformDirection(Vector3.forward);
            openPosition = closedPosition + drawerDirection * drawerDistance * directionMultiplier;
        }

        void Update()
        {
            Vector3 targetPosition = open ? openPosition : closedPosition;
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * 5 * smooth);
        }

        public void ToggleDrawer()
        {
            open = !open;
            asource.clip = open ? openDrawer : closeDrawer;
            asource.Play();
        }
    }
}


