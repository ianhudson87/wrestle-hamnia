using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMove : NetworkBehaviour
{

    private CharacterController char_controller;
    private CharacterAnimations player_animations;
    [SerializeField] private PlayerState playerState;

    public float max_movement_Speed = 3f;
    public float jump_force = 9f;
    public const float gravity = 9.81f;
    public float movement_force = 15f;
    public float air_movement_force = 15f;
    public float rotation_speed = 0.15f;
    public float rotate_degrees_per_second = 180f;
    public float friction = 1000000f;

    GameObject wheel = null;
    // WheelAnimations wheel_anim;

    public Camera cam;

    private Vector3 world_velocity; // velocity of player with respect to world space

    // [SyncVar] Vector3 delta_velocity_from_server = Vector3.zero; // server changes this (world) velocity vector when it wants to change the velocity of the player

    private Vector2 mouse_direction = Vector2.zero;

    // Start is called before the first frame update
    void Awake() {
        char_controller = GetComponent<CharacterController>();
        player_animations = GetComponent<CharacterAnimations>();
        playerState = GetComponent<PlayerState>();
        world_velocity = Vector3.zero;
        
        // wheel_anim = new WheelAnimations();
    }

    // Update is called once per frame
    void Update()
    {
        // print("here");
        if(isLocalPlayer && playerState.isAlive) {
            SetVelocity();
            Move();
            Rotate();
            AnimateWalk();
        }
    }

    void SetVelocity() {

        // gravity
        if(char_controller.isGrounded){
            // world_velocity.y = -0.1f * Time.deltaTime;
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




        // ground control
        float delta_velocity = (movement_force) * Time.deltaTime;

        if (char_controller.isGrounded) {
            // w, s
            if(Input.GetKey(KeyCode.W)){
                world_velocity += delta_velocity * transform.TransformVector(Vector3.forward);
            }
            if(Input.GetKey(KeyCode.S)){
                world_velocity += delta_velocity * transform.TransformVector(Vector3.back);
            }

            // a, d
            if(Input.GetKey(KeyCode.D)){
                world_velocity += delta_velocity * transform.TransformVector(Vector3.right);
            }
            if(Input.GetKey(KeyCode.A)){
                world_velocity += delta_velocity * transform.TransformVector(Vector3.left);
            }
        }

        // ground friction
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


        // air control
        
        if(!char_controller.isGrounded){
            Vector3 desiredVelocityChange = Vector3.zero;

            float delta_air_velocity = air_movement_force * Time.deltaTime;

            // w, s
            if(Input.GetKey(KeyCode.W)){
                desiredVelocityChange += delta_air_velocity * transform.TransformVector(Vector3.forward);
            }
            if(Input.GetKey(KeyCode.S)){
                desiredVelocityChange += delta_air_velocity * transform.TransformVector(Vector3.back);
            }

            // a, d
            if(Input.GetKey(KeyCode.D)){
                desiredVelocityChange += delta_air_velocity * transform.TransformVector(Vector3.right);
            }
            if(Input.GetKey(KeyCode.A)){
                desiredVelocityChange += delta_air_velocity * transform.TransformVector(Vector3.left);
            }


            // print("move" + desiredVelocityChange / Time.deltaTime);
            world_velocity += desiredVelocityChange;

            // rescale desiredDirection to have the same magnitude as the horizontal velocity vector.
            // then project desiredDirection onto direction of horizontal velocity. Magnitude of that is "air velocity" kind of meaningless value.
            Vector2 desiredDirection2D = new Vector2(desiredVelocityChange.x, desiredVelocityChange.z);
            Vector2 moveDirection2D = new Vector2(char_controller.velocity.x, char_controller.velocity.z);

            float angleBetweenDesiredAndActual = Vector2.Angle(desiredDirection2D, moveDirection2D);
            // print(angleBetweenDesiredAndActual);
            float cosOfAngle = Mathf.Cos(Mathf.Deg2Rad * angleBetweenDesiredAndActual);
            // print(Mathf.Cos(90));
            // print(cosOfAngle);

            float airVelocity = cosOfAngle * Mathf.Sqrt(Vector2.SqrMagnitude(moveDirection2D));
            // print(airVelocity);

            float maxAirVelocity = max_movement_Speed; // try changing this to current horizontal velocity later

            float airFrictionForce = (airVelocity / maxAirVelocity) * air_movement_force;
            // print("frac" + airVelocity);
            // print(airFrictionForce);


            // print("friction" + new Vector3(airFrictionForce * desiredDirection2D.x, 0, airFrictionForce * desiredDirection2D.y));
            float desiredDirectionMagnitude = Mathf.Sqrt(Vector2.SqrMagnitude(desiredDirection2D));
            if(desiredDirectionMagnitude != 0){
                world_velocity -= new Vector3(airFrictionForce * Time.deltaTime * desiredDirection2D.x, 0, airFrictionForce * Time.deltaTime * desiredDirection2D.y) / desiredDirectionMagnitude;
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

    public void ApplyBoop(Vector3 world_boop){
        world_velocity += world_boop;
    }

    public void SetPosition(Vector3 position){
        char_controller.Move(position - transform.position);
    }

}
