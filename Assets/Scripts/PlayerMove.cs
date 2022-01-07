using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMove : NetworkBehaviour
{

    private CharacterController char_controller;
    private CharacterAnimations player_animations;
    private GameObject melee_hit_detector;

    public float movement_Speed = 3f;
    public const float gravity = 9.81f;
    public const float acceleration = 6f;
    public float rotation_speed = 0.15f;
    public float rotate_degrees_per_second = 180f;

    Transform wheel = null;
    // WheelAnimations wheel_anim;

    public Camera cam;

    private Vector3 velocity;

    private Vector2 mouse_direction = Vector2.zero;

    // Start is called before the first frame update
    void Awake() {
        char_controller = GetComponent<CharacterController>();
        player_animations = GetComponent<CharacterAnimations>();
        velocity = Vector3.zero;
        melee_hit_detector = transform.Find("MeleeHitDetector").gameObject;
        // wheel_anim = new WheelAnimations();
    }

    // Update is called once per frame
    void Update()
    {
        // print("here");
        if(isLocalPlayer) {
            SetVelocity();
            Move();
            Rotate();
            AnimateWalk();
        }
        else {
            cam.enabled = false;
            cam.GetComponent<AudioListener>().enabled = false;
        }
    }

    void SetVelocity() {
        // gravity
        if(char_controller.isGrounded){
            velocity.y = 0;
        }
        else{
            velocity.y -= gravity * Time.deltaTime;
        }

        // jump
        if(Input.GetKeyDown(KeyCode.Space) && char_controller.isGrounded){
            velocity.y = movement_Speed;
        }


        float delta_velocity = acceleration * Time.deltaTime;

        // w, s
        if(Input.GetKey(KeyCode.W)){
            velocity.z = Math.Min(movement_Speed, velocity.z + delta_velocity);
        }
        else if(Input.GetKey(KeyCode.S)){
            velocity.z = Math.Max(-movement_Speed, velocity.z - delta_velocity);
        }
        else{
            velocity.z *= Mathf.Pow(0.1f, Time.deltaTime);
            if(Math.Abs(velocity.z) < 0.1f){
                velocity.z = 0;
            }
        }

        // a, d
        if(Input.GetKey(KeyCode.D)){
            velocity.x = Math.Min(movement_Speed, velocity.x + delta_velocity);
        }
        else if(Input.GetKey(KeyCode.A)){
            velocity.x = Math.Max(-movement_Speed, velocity.x - delta_velocity);
        }
        else{
            velocity.x *= Mathf.Pow(0.1f, Time.deltaTime);
            if(Math.Abs(velocity.x) < 0.1f){
                velocity.x = 0;
            }
        }
    }

    void Move() {
        // print("here" + Input.GetAxisRaw(Axis.VERTICAL_AXIS));

        if(Input.GetKey(KeyCode.F)){
            if (wheel == null) {
                // wheel = wheel.transform;
                wheel = GameObject.Find("HamsterWheel").transform;
            }
            
            // wheel is reel
            if ((wheel.position - transform.position).sqrMagnitude < 17f) {
                // transform.position = wheel.position;
                Vector3 move_direction;
                move_direction = wheel.position - transform.position + new Vector3(0.5f,-0.5f,1f);
                char_controller.Move(move_direction);
                // movement_Speed = 1f;
                player_animations.Walk(true);
                WheelAnimations.Spin();
                return;
            }
        }
        // else if (Input.GetAxisRaw(Axis.VERTICAL_AXIS) > 0) {
        //     move_direction += transform.forward;

        //     // char_controller.Move(move_direction * movement_Speed * Time.deltaTime);
        // }
        // else if (Input.GetAxisRaw(Axis.VERTICAL_AXIS) < 0) {
        //     move_direction += -transform.forward;

        //     // char_controller.Move(move_direction * movement_Speed * Time.deltaTime);
        // }

        if(Input.GetKeyUp(KeyCode.F) && ((wheel.position - transform.position).sqrMagnitude < 10f)){
            Vector3 move_direction;
            move_direction = new Vector3(3f,1f,1f);
            char_controller.Move(move_direction);
            
            WheelAnimations.StopSpin();
            return;
        }


        Vector3 world_velocity = transform.TransformVector(velocity);
        char_controller.Move(world_velocity * Time.deltaTime);

    }

    void Rotate(){
        //get the mouse position
        // Vector3 mousePosition = Input.mousePosition;
        // mousePosition.z = 10.0f;
        // Vector3 lookPos = cam.ScreenToWorldPoint(mousePosition);
        // print(mousePosition);
        // print(lookPos);
        // Quaternion targetRotation = Quaternion.LookRotation(lookPos - transform.position, Vector3.forward);
        // transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 3f * Time.deltaTime);

        Vector2 mouse_delta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        mouse_direction += mouse_delta;

        cam.transform.localRotation = Quaternion.AngleAxis(mouse_direction.y, Vector3.left);
        this.transform.localRotation = Quaternion.AngleAxis(mouse_direction.x, Vector3.up);








        // Vector3 rotation_direction = Vector3.zero;
        // // print("The values is" + Input.GetAxis(Axis.HORIZONTAL_AXIS));

        // if(Input.GetAxis(Axis.HORIZONTAL_AXIS) < 0) {
        //     // rotation_direction = transform.TransformPoint()
        //     // print("here1");
        //     rotation_direction = transform.TransformDirection(Vector3.left);
        // }
        // else if(Input.GetAxis(Axis.HORIZONTAL_AXIS) > 0) {
        //     // print("here2");
        //     rotation_direction = transform.TransformDirection(Vector3.right);
        // }

        // if(rotation_direction != Vector3.zero){
        //     // print("here3");
        //     // transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(rotation_direction), rotate_degrees_per_second * Time.deltaTime);
        //     transform.rotation = Quaternion.RotateTowards(
        //         transform.rotation,
        //         Quaternion.LookRotation(rotation_direction),
        //         rotate_degrees_per_second * Time.deltaTime);

        // }
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
