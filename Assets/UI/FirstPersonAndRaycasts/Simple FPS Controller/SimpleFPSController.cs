
using System;
using UnityEngine;


[RequireComponent (typeof (CharacterController))]
public class SimpleFPSController : MoveController
{
   [SerializeField] private float headRotationMinAngle;
   [SerializeField] private float headRotationMaxAngle;
   [SerializeField] private Transform head;

   private float _headRotation;
   private Camera _camera;
   private AudioListener _audioListener;

   private void Start()
   {
      _camera = head.GetComponent<Camera>();
      _audioListener = head.GetComponent<AudioListener>();
   }


   public void UpdateRegular()
   {
      base.UpdateController();
   }

   public void UpdatePhotoCamera()
   {
      MouseLook();
   }

  

   public override void OnPlayerActivate(bool isActive)
   {
      _camera.enabled = isActive;
      _audioListener.enabled = isActive;
   }

   protected override void MouseLook()
   {
      base.MouseLook();
              
      _headRotation += RotationAxe * mouseSensetivityY * Time.deltaTime;
      _headRotation = Mathf.Clamp(_headRotation, headRotationMinAngle, headRotationMaxAngle);
      head.localRotation = Quaternion.Euler(Vector3.left * _headRotation);
   }
}
