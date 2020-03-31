using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlumnoController : MonoBehaviour {

    private Animator animComp;
    private int prevMovementState;
    public Transform mainCamera;
    private int movementState;
    private enum CharacterMovement {IDLE = 0, WALK = 1, JUMP = 2};

    // Start is called before the first frame update
    void Start()
    {
        animComp = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.Space)){
            prevMovementState = movementState;
            movementState = (int) CharacterMovement.JUMP;
        }
        else if(movementState == (int) CharacterMovement.JUMP){
            if(getCharacterAnimatorState() != (int)CharacterMovement.JUMP){
                movementState = prevMovementState;
            } 
        }
        else if(Input.GetKeyDown(KeyCode.W)) {
            movementState = (int) CharacterMovement.WALK;
        }
        else if(Input.GetKeyUp(KeyCode.W)) {
            movementState = (int) CharacterMovement.IDLE;
        }

        animComp.SetInteger("AlumnoState", movementState);

        this.transform.rotation = mainCamera.rotation;
    }

    private int getCharacterAnimatorState()
    {
        int jumpState = Animator.StringToHash("Base.Jumping");
        int walkState = Animator.StringToHash("Base.Walking");
        int IdleState = Animator.StringToHash("Base.Idle");

        AnimatorStateInfo currentBaseState = animComp.GetCurrentAnimatorStateInfo(0);

        if(currentBaseState.fullPathHash == jumpState) {
            return (int)CharacterMovement.JUMP;
        }
        else if(currentBaseState.fullPathHash == walkState) {
            return (int)CharacterMovement.WALK;
        }
        else if(currentBaseState.fullPathHash == IdleState) {
            return (int)CharacterMovement.IDLE;
        }   
        return 0;
    }

}
