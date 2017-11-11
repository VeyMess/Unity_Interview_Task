
using UnityEngine;

public class AnimationHit : MonoBehaviour {

    ServerContacts servCon;

    void Start()
    {
        servCon = FindObjectOfType<ServerContacts>();
    }

	public void HitAttack()
    {
        if (tag.Equals("Player"))
        {
            servCon.DrawEnemyHP();
        }
    }

    public void AttackEnd()
    {
        FindObjectOfType<PlayerAttack>().SetList(false);
    }
}
