using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    public LayerMask layerMask; // Select all layers that foot placement applies to.

    [Range (0, 1f)]
    public float DistanceToGround; // Distance from where the foot transform is to the lowest possible position of the foot.
    [Range (0, 1f)]
    public float DistanceToFront; // Distance from where the foot transform is to the lowest possible position of the foot.

    private void Start() {

        animator = GetComponentInChildren<Animator>();

    }

    private void Update() {
    }

    private void OnAnimatorIK(int layerIndex) {
        Debug.Log("called");
        if (animator) { // Only carry out the following code if there is an Animator set.

            // Set the weights of left and right feet to the current value defined by the curve in our animations.
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1f);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1f);
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1f);
            animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1f);

     
            // We cast our ray from above the foot in case the current terrain/floor is above the foot position.
            Ray ray = new Ray(animator.GetIKPosition(AvatarIKGoal.LeftFoot) + Vector3.up, Vector3.down);

        Debug.Log(animator.GetBoneTransform(HumanBodyBones.LeftFoot));
                    Vector3 footPosition = animator.GetBoneTransform(HumanBodyBones.LeftFoot).position;
                    Quaternion footRotation = animator.GetBoneTransform(HumanBodyBones.LeftFoot).rotation; // The target foot position is where the raycast hit a walkable object...
                    footPosition.y += DistanceToGround; // ... taking account the distance to the ground we added above.
                    footPosition.z += DistanceToFront;
                    animator.SetIKPosition(AvatarIKGoal.LeftFoot, footPosition);
                    animator.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.Euler(0, 0, 0));

            

            // Right Foot
            ray = new Ray(animator.GetIKPosition(AvatarIKGoal.RightFoot) + Vector3.up, Vector3.down);

          

                    footPosition = animator.GetBoneTransform(HumanBodyBones.RightFoot).position;
                    footRotation = animator.GetBoneTransform(HumanBodyBones.RightFoot).rotation;
                    footPosition.y += DistanceToGround;
                    footPosition.z += DistanceToFront;
                    animator.SetIKPosition(AvatarIKGoal.RightFoot, footPosition);
                    animator.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.Euler(0, 0, 0));
                    //animator.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.LookRotation(transform.forward, hit.normal));

                

            


        }

    }

}