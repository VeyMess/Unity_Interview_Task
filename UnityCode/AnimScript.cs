using UnityEngine;

public class AnimScript : StateMachineBehaviour {

	override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Attack", false);
	}
}
