using UnityEngine;
using UnityEngine.UI;

public class DrawHealth : MonoBehaviour {

    public Canvas healthProt;
    public GameObject playerPos;
    public GameObject enemyPos;

    private Collider head;
    private Canvas playerHealth;
    private float timeExp = 0f;

    private Collider enemyHead;
    private Canvas enemyHealth;

    void Start()
    {
        //attaching health-text on player
        foreach(Collider temp in playerPos.GetComponentsInChildren<Collider>())
        {
            if (temp.tag.Equals("Head"))
                head = temp;
        }

        Vector3 tempPos = playerPos.transform.position;
        tempPos.y += 2;
        tempPos = head.ClosestPoint(tempPos);
        tempPos.y += 0.2f;

        playerHealth = Instantiate(healthProt, tempPos, Quaternion.LookRotation(transform.position,transform.up), head.transform);
        playerHealth.GetComponentInChildren<Text>().color = Color.green;
        playerHealth.transform.localScale *= 2.5f;


        //attaching health-text on enemy
        foreach (Collider temp in enemyPos.GetComponentsInChildren<Collider>())
        {
            if (temp.tag.Equals("Head"))
                enemyHead = temp;
        }

        Vector3 tEnemyPos = enemyPos.transform.position;
        tEnemyPos.y += 2;
        tEnemyPos = enemyHead.ClosestPoint(tEnemyPos);
        tEnemyPos.y += 0.2f;

        enemyHealth = Instantiate(healthProt, tEnemyPos, Quaternion.LookRotation(transform.position, transform.up), enemyHead.transform);
        enemyHealth.GetComponentInChildren<Text>().color = Color.red;
    }


    void Update()
    {
        timeExp += Time.deltaTime;
        playerHealth.transform.rotation = Quaternion.LookRotation(transform.position, transform.up);
        playerHealth.transform.Rotate(0, 180, 0);


        timeExp += Time.deltaTime;
        enemyHealth.transform.rotation = Quaternion.LookRotation(transform.position, transform.up);
        enemyHealth.transform.Rotate(0, 180, 0);
    }
	
    public void DrawPlayerHealth(string dig)
    {
        playerHealth.GetComponentInChildren<Text>().text = dig.ToString();
    }

    public void DrawEnemyHealth(string dig)
    {
        enemyHealth.GetComponentInChildren<Text>().text = dig.ToString();
    }

    public void UpTextOnDeath()
    {
        Vector3 temp = enemyHealth.transform.position;
        temp.y -= 1f;
        temp.z -= 1f;
        enemyHealth.transform.position = temp;
    }
}
