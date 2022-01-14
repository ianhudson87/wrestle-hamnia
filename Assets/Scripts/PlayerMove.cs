using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMove : NetworkBehaviour
{

    private CharacterController char_controller;
    private CharacterAnimations player_animations;

    public float max_movement_Speed = 3f;
    public float jump_force = 9f;
    public const float gravity = 9.81f;
    public float movement_force = 15f;
    public float rotation_speed = 0.15f;
    public float rotate_degrees_per_second = 180f;
    public float friction = 1000000f;

    GameObject wheel = null;
    // WheelAnimations wheel_anim;

    public Camera cam;

    private Vector3 world_velocity; // velocity of player with respect to world space

    private Vector2 mouse_direction = Vector2.zero;

    // Start is called before the first frame update
    void Awake() {
        char_controller = GetComponent<CharacterController>();
        player_animations = GetComponent<CharacterAnimations>();
        world_velocity = Vector3.zero;
        
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
    }

    void SetVelocity() {
        // gravity
        if(char_controller.isGrounded){
            world_velocity.y = -0.1f * Time.deltaTime;
        }
        else{
            world_velocity.y -= gravity * Time.deltaTime;
        }

        // jump
        if(Input.GetKeyDown(KeyCode.Space)){
            // print("jump");
            if (char_controller.isGrounded)
                world_velocity.y = jump_force;
        }




        float delta_velocity = (movement_force) * Time.deltaTime;

        if (char_controller.isGrounded) {
        // w, s
            if(Input.GetKey(KeyCode.W)){
                world_velocity += delta_velocity * transform.TransformVector(Vector3.forward);
            }
            else if(Input.GetKey(KeyCode.S)){
                world_velocity += delta_velocity * transform.TransformVector(Vector3.back);
            }
            // else{
            //     velocity.z *= Mathf.Pow(1/friction, Time.deltaTime);
            //     if(Math.Abs(velocity.z) < 0.1f){
            //         velocity.z = 0;
            //     }
            // }

            // a, d
            if(Input.GetKey(KeyCode.D)){
                world_velocity += delta_velocity * transform.TransformVector(Vector3.right);
            }
            else if(Input.GetKey(KeyCode.A)){
                world_velocity += delta_velocity * transform.TransformVector(Vector3.left);
            }
        }



        // friction
        // print(char_controller.velocity);
        if(char_controller.isGrounded){
            Vector2 horizontal_local_velocity = new Vector2(char_controller.velocity.x, char_controller.velocity.z);
            float current_speed = Mathf.Sqrt(Vector2.SqrMagnitude(horizontal_local_velocity));
            float frictional_force = (current_speed / max_movement_Speed) * movement_force;
            float frictional_delta_velocity = frictional_force * Time.deltaTime;
            // print(frictional_delta_velocity);
            
            if(current_speed != 0){
                // Vector2 horizontal_local_direction = horizontal_local_velocity / current_speed;
                // Vector3 local_direction = new Vector3(horizontal_local_direction.x, 0, horizontal_local_direction.y);
                // Vector3 world_direction = transform.TransformVector(local_direction);
                // print("local" + local_direction);
                // print(world_direction);

                // world_velocity -= new Vector3(world_direction.x * frictional_delta_velocity, 0 , world_direction.z * frictional_delta_velocity);

                Vector2 horizontal_world_direction = horizontal_local_velocity / current_speed;

                world_velocity -= new Vector3(horizontal_world_direction.x * frictional_delta_velocity, 0 , horizontal_world_direction.y * frictional_delta_velocity);
            }
        }

    }

    void Move() {
        // print("here" + Input.GetAxisRaw(Axis.VERTICAL_AXIS));

        if(Input.GetKey(KeyCode.F)){
            if (wheel == null) {
                // wheel = wheel.transform;
                wheel = GameObject.Find("HamsterWheel");
                // wheel_animwheel.GetComponent<WheelAnimations>();
            }
            
            // wheel is reel
            if ((wheel.transform.position - transform.position).sqrMagnitude < 17f) {
                // transform.position = wheel.position;
                Vector3 move_direction;
                move_direction = wheel.transform.position - transform.position + new Vector3(0.5f,-0.5f,1f);
                char_controller.Move(move_direction);
                // movement_Speed = 1f;
                player_animations.Walk(true);
                wheel.GetComponent<WheelAnimations>().Spin();
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

        if(Input.GetKeyUp(KeyCode.F) && ((wheel.transform.position - transform.position).sqrMagnitude < 10f)){
            Vector3 move_direction;
            move_direction = new Vector3(3f,1f,1f);
            char_controller.Move(move_direction);
            
            wheel.GetComponent<WheelAnimations>().StopSpin();
            return;
        }

        char_controller.Move(world_velocity * Time.deltaTime);

    }

    void Rotate(){
        Vector2 mouse_delta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        mouse_direction += mouse_delta;

        cam.transform.localRotation = Quaternion.AngleAxis(mouse_direction.y, Vector3.left); // rotate camera up and down. don't rotate model
        this.transform.localRotation = Quaternion.AngleAxis(mouse_direction.x, Vector3.up); // rotate model left to right, also rotates camera

    }

    void AnimateWalk(){
        if(char_controller.velocity.sqrMagnitude != 0f && char_controller.isGrounded) {
            // print("walk animate");
            player_animations.Walk(true);
        }
        else{
            // print("walk stop");
            player_animations.Walk(false);
        }
    }


}
