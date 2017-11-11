using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ServerContacts : MonoBehaviour
{
    private Testings launch;

    float exitTimer = 3f;

    public float playerHP;
    public float enemyHP;

    private bool enemyAlive = true;

    public PlayerAttack atccl;
    public DrawHealth drawHP;

    delegate void tryse(Dictionary<byte, object> x);

    void Update()
    {
        if(!enemyAlive)
        {
            if (exitTimer <= 0f)
            {
                SceneManager.LoadScene("ChooseScene");
            }
            else
                exitTimer -= Time.deltaTime;
        }
    }

    void Start()
    {
        launch = FindObjectOfType<Testings>();
        FightInic();
    }

    void FightInic()
    {
        launch.FightStarting(this);
    }

    public void Inicil(Dictionary<byte, object> strt)
    {
        playerHP = (float)strt[0];
        enemyHP = (float)strt[1];
        atccl.SetFighting(true);

        drawHP.DrawPlayerHealth(playerHP.ToString());
        drawHP.DrawEnemyHealth(enemyHP.ToString());
    }

    public void PlayerWantsAttack()
    {
        launch.WantsAttack(this);
    }

    public void ReciveAtcReq(Dictionary<byte, object> attack)
    {
        enemyHP = (float)attack[1];
        atccl.RecivedAnswer(attack);
    }

    public void DrawEnemyHP()
    {
        if (enemyHP > 0)
            drawHP.DrawEnemyHealth(enemyHP.ToString());
        else
        {
            drawHP.DrawEnemyHealth("Враг Мертв!");
            //проверка противника на проигранную анимацию смерти
            if(enemyAlive)
            {
                foreach(AnimationHit temp in FindObjectsOfType<AnimationHit>())
                {
                    if (temp.tag.Equals("Enemy"))
                    {
                        temp.GetComponent<Animator>().SetTrigger("Dead");
                        drawHP.UpTextOnDeath();
                        enemyAlive = false;
                        FindObjectOfType<FileSave>().PlayerWin();
                        break;
                    }
                }
            }
        }
    }
}
	
