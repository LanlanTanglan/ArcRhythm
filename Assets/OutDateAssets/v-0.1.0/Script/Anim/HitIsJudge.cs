using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitIsJudge : StateMachineBehaviour
{
    public float hitLen;
    public bool hitLenFlag = false;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //播放完毕结束
        if (!hitLenFlag)
        {
            hitLen = Util.AnimatorUtil.GetAnimatorLength(animator, animator.GetCurrentAnimatorClipInfo(layerIndex)[0].clip.name);
            Debug.Log(hitLen);
            hitLenFlag = true;
        }
        if (stateInfo.normalizedTime > hitLen)
        {
            Destroy(animator.gameObject);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
