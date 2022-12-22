using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_MoveBehavior : MonoBehaviour
{

    public float mouseX;
    public float slowMouseX;

    private float vertical;
    private float horizontal;
    private Animator animator;
    private Rigidbody rb;
    public LayerMask layer;
    public Vector3 inputs;
    Quaternion rotation;

    [Range(0f, 1f)]
    public float DistanceToGround;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    
    void FixedUpdate()
    {
        //mouseX += Input.GetAxis("Mouse X");
        //mouseX = Mathf.Clamp(mouseX,-1,1);
        //slowMouseX = Mathf.Lerp(slowMouseX,mouseX,5 * Time.deltaTime);
        //animator.SetFloat("MouseX", slowMouseX);

        
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        inputs.Set(horizontal, 0f, vertical);
        inputs.Normalize();

        Vector3 directionRotate = Vector3.RotateTowards(transform.forward, inputs, 20f * Time.deltaTime, 0.0f);
        rotation = Quaternion.LookRotation(directionRotate);
        animator.SetFloat("Vertical", vertical);
        animator.SetFloat("Horizontal", horizontal);
    }
    private void OnAnimatorMove()
    {
       
        rb.MovePosition(rb.position + inputs * 2f  * animator.deltaPosition.magnitude );
        rb.MoveRotation(rotation);
    }
    private void OnAnimatorIK(int layerIndex)
    {
        if (animator)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1f);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1f);

            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1f);
            animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1f);

            //Left foot

            RaycastHit hit;
            Ray ray = new Ray(animator.GetIKPosition(AvatarIKGoal.LeftFoot) + Vector3.up, Vector3.down);
            if (Physics.Raycast(ray, out hit, DistanceToGround + 1f, layer))
            {
                if(hit.transform.tag == "Walkable")
                {
                    Vector3 footPosition = hit.point;
                    footPosition.y += DistanceToGround;
                    animator.SetIKPosition(AvatarIKGoal.LeftFoot, footPosition);
                    animator.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.LookRotation(transform.forward, hit.normal));
                }
            }

            //Right foot

           
             ray = new Ray(animator.GetIKPosition(AvatarIKGoal.RightFoot) + Vector3.up, Vector3.down);
            if (Physics.Raycast(ray, out hit, DistanceToGround + 1f, layer))
            {
                if (hit.transform.tag == "Walkable")
                {
                    Vector3 footPosition = hit.point;
                    footPosition.y += DistanceToGround;
                    animator.SetIKPosition(AvatarIKGoal.RightFoot, footPosition);
                    animator.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.LookRotation(transform.forward, hit.normal));
                }
            }
        }
    }
}
