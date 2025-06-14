using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace DoorScript
{
    [RequireComponent(typeof(AudioSource))]
    public class IronDoor : Door
    {
        [Header("Electric settings")]
        [SerializeField] bool energy;
        [SerializeField] AudioClip noneEnergy;


        public override void Start()
        {
            base.Start();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Toggle()
        {

            open = energy ? !open : open && false;
            asource.clip = energy ? (open ? openDoor : closeDoor) : noneEnergy;
            asource.Play();

            /*
            if (energy)
            {
                open = !open;
                asource.clip = open ? openDoor : closeDoor;
            }
            else
            {
                asource.clip = noneEnergy;
                if (open) open = !open;
            }
            asource.Play();
            */
        }
    }
}
