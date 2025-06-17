using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DoorScript
{
    [RequireComponent(typeof(AudioSource))]
    public class Door : Opened
    {
        public float smooth = 1.0f;
        public float directionMultiplier = 1.0f; // 1 – открывается вправо, -1 – влево
        float DoorOpenAngle = -90.0f;
        float DoorCloseAngle = 0.0f;
        public AudioSource asource;
        public AudioClip openDoor, closeDoor;
        [SerializeField] AudioClip CantOpen;

        public virtual void Start()
        {
            //state = true;
            asource = GetComponent<AudioSource>();
        }
        public virtual void Update()
        {
            float openAngle = DoorOpenAngle * directionMultiplier;
            float closeAngle = DoorCloseAngle;

            if (active)
            {
                var target = Quaternion.Euler(0, openAngle, 0);
                transform.localRotation = Quaternion.Slerp(transform.localRotation, target, Time.deltaTime * 5 * smooth);
            }
            else
            {
                var target1 = Quaternion.Euler(0, closeAngle, 0);
                transform.localRotation = Quaternion.Slerp(transform.localRotation, target1, Time.deltaTime * 5 * smooth);
            }
        }        
        public override void Toggle()
        {
            if (state)
            {
                active = !active;
            }
            asource.clip = state ? (active ? openDoor : closeDoor) : CantOpen;
            asource.Play();
        }
        public override void SetState(bool state)
        {
            this.state = state;
        }
    }
}
