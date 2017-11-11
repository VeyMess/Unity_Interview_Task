using UnityEngine;
using System.Collections.Generic;

public class PlayerAttack : MonoBehaviour {

    Camera cam;
    bool fighting = false;
    bool recSend = false;

    private GameObject player;
    private ServerContacts servCon;

    void Start()
    {
        cam = GetComponent<Camera>();
        servCon = FindObjectOfType<ServerContacts>();
    }

	void Update ()
    {
        if(Input.GetMouseButtonDown(0) && fighting)
        {
            Ray rayT = cam.ScreenPointToRay(Input.mousePosition);

            RaycastHit RayH;
            if(Physics.Raycast(rayT,out RayH))
                if(RayH.collider.tag.Equals("Player") && !RayH.collider.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("attack1") && !recSend)
                {
                    recSend = true;
                    servCon.PlayerWantsAttack();
                    player = RayH.collider.gameObject;
                }
        }
	}

    public void SetFighting(bool tmp)
    {
        fighting = tmp;
    }

    public void RecivedAnswer(Dictionary<byte, object> attc)
    {
        if((bool)attc[0] == true)
        {
            player.GetComponent<Animator>().SetBool("Attack", true);
        }
        else
        {
            recSend = false;
            servCon.DrawEnemyHP();
        }
    }

    public void SetList(bool end)
    {
        recSend = end;
    }
}
