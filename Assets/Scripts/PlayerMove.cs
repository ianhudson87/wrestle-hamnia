using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : NetworkBehaviour
{

    private CharacterController char_controller;
    private CharacterAnimations player_animations;

    public float movement_Speed = 3f;
    public const float gravity = 500f;
    public float rotation_speed = 0.15f;
    public float rotate_degrees_per_second = 180f;

    Transform wheel = null;
    // WheelAnimations wheel_anim;
    

    public Camera cam;

    // Start is called before the first frame update
    void Awake() {
        char_controller = GetComponent<CharacterController>();
        player_animations = GetComponent<CharacterAnimations>();
        // wheel_anim = new WheelAnimations();
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
        Vector3 move_direction = Vector3.zero;

        if(Input.GetKey(KeyCode.Space)){
            if (wheel == null) {
                // wheel = wheel.transform;
                wheel = GameObject.Find("HamsterWheel").transform;
            }
            
            // wheel is reel
            if ((wheel.position - transform.position).sqrMagnitude < 17f) {
                // transform.position = wheel.position;
                move_direction = wheel.position - transform.position + new Vector3(0.5f,-0.5f,1f);
                char_controller.Move(move_direction);
                // movement_Speed = 1f;
                player_animations.Walk(true);
                WheelAnimations.Spin();
                return;
            }
        }
        else if (Input.GetAxisRaw(Axis.VERTICAL_AXIS) > 0) {
            move_direction += transform.forward;

            // char_controller.Move(move_direction * movement_Speed * Time.deltaTime);
        }
        else if (Input.GetAxisRaw(Axis.VERTICAL_AXIS) < 0) {
            move_direction += -transform.forward;

            // char_controller.Move(move_direction * movement_Speed * Time.deltaTime);
        }

        if(Input.GetKeyUp(KeyCode.Space) && ((wheel.position - transform.position).sqrMagnitude < 10f)){

            move_direction = new Vector3(3f,1f,1f);
            char_controller.Move(move_direction);
            
            WheelAnimations.StopSpin();
            return;
        }
        move_direction.y -= gravity * Time.deltaTime;
        char_controller.Move(move_direction * movement_Speed * Time.deltaTime);

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
