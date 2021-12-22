using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : NetworkBehaviour
{

    private CharacterController char_controller;
    private CharacterAnimations player_animations;

    public float movement_Speed = 3f;
    public float gravity = 49f;
    public float rotation_speed = 0.15f;
    public float rotate_degrees_per_second = 180f;

    public Camera cam;

    // Start is called before the first frame update
    void Awake() {
        char_controller = GetComponent<CharacterController>();
        player_animations = GetComponent<CharacterAnimations>();
    }

    // Update is called once per frame
    void Update()
    {
        // print("here");
        if(isLocalPlayer) {
            Move();
            Rotate();
            AnimateWalk();
        }
        else {
            cam.enabled = false;
            cam.GetComponent<AudioListener>().enabled = false;
        }
    }

    void Move() {
        // print("here" + Input.GetAxisRaw(Axis.VERTICAL_AXIS));

        if (Input.GetAxisRaw(Axis.VERTICAL_AXIS) > 0) {
            Vector3 move_direction = transform.forward;
            move_direction.y -= gravity * Time.deltaTime;

            char_controller.Move(move_direction * movement_Speed * Time.deltaTime);
        
        }
        else if (Input.GetAxisRaw(Axis.VERTICAL_AXIS) < 0) {
            Vector3 move_direction = -transform.forward;
            move_direction.y -= gravity * Time.deltaTime;

            char_controller.Move(move_direction * movement_Speed * Time.deltaTime);
        }
        else{
            char_controller.Move(Vector3.zero);
        }
    }

    void Rotate(){
        Vector3 rotation_direction = Vector3.zero;
        // print("The values is" + Input.GetAxis(Axis.HORIZONTAL_AXIS));

        if(Input.GetAxis(Axis.HORIZONTAL_AXIS) < 0) {
            // rotation_direction = transform.TransformPoint()
            // print("here1");
            rotation_direction = transform.TransformDirection(Vector3.left);
        }
        else if(Input.GetAxis(Axis.HORIZONTAL_AXIS) > 0) {
            // print("here2");
            rotation_direction = transform.TransformDirection(Vector3.right);
        }

        if(rotation_direction != Vector3.zero){
            // print("here3");
            // transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(rotation_direction), rotate_degrees_per_second * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                Quaternion.LookRotation(rotation_direction),
                rotate_degrees_per_second * Time.deltaTime);

        }
    }

    void AnimateWalk(){
        if(char_controller.velocity.sqrMagnitude != 0f){
            // print("walk animate");
            player_animations.Walk(true);
        }
        else{
            // print("walk stop");
            player_animations.Walk(false);
        }
    }
}
