using UnityEngine;

namespace SupermarketSim {
    public sealed class FirstPersonPlayer : MonoBehaviour {
        public float WalkSpeed = 3.2f, RunSpeed = 5.2f, Gravity = -18f;
        public Camera PlayerCamera;
        public float LookSensitivity = 2f;
        CharacterController controller; float pitch; Vector3 velocity;
        void Awake(){ controller=GetComponent<CharacterController>(); if(PlayerCamera==null) PlayerCamera=GetComponentInChildren<Camera>(); }
        void Update(){
            float x=Input.GetAxisRaw("Horizontal"), z=Input.GetAxisRaw("Vertical");
            Vector3 move=(transform.right*x+transform.forward*z).normalized;
            bool run=Input.GetKey(KeyCode.LeftShift);
            controller.Move(move*(run?RunSpeed:WalkSpeed)*Time.deltaTime);
            if(controller.isGrounded && velocity.y<0) velocity.y=-2;
            velocity.y+=Gravity*Time.deltaTime; controller.Move(velocity*Time.deltaTime);
            float mx=Input.GetAxis("Mouse X")*LookSensitivity, my=Input.GetAxis("Mouse Y")*LookSensitivity;
            transform.Rotate(Vector3.up*mx); pitch=Mathf.Clamp(pitch-my,-80,80); if(PlayerCamera) PlayerCamera.transform.localEulerAngles=new Vector3(pitch,0,0);
        }
    }
}