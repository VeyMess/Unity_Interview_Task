using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ChoosFighter : MonoBehaviour {

    public byte currChar = 0;
    public byte arena = 0;
    public GameObject netLuncher;
    public Button strtBut;

    public Button scene1Btn;
    public Button scene2Btn;
    public Button scene3Btn;

    public Text win;
    public Text losts;

    private UnityAction strtDeleg;

    void Start()
    {
        Testings tempte = FindObjectOfType<Testings>();

        if (tempte == null)
        {
            GameObject tempTe = Instantiate(netLuncher);

            strtDeleg += tempTe.GetComponent<Testings>().ButonStart;
            strtBut.onClick.AddListener(strtDeleg);
        }
        else
        {
            strtDeleg += tempte.ButonStart;
            strtBut.onClick.AddListener(strtDeleg);
        }
        StatsUpdate();
    }

    public void NextChar()
    {
        if (currChar < 2)
        {
            currChar++;
            ChangeListPos(true);
            StatsUpdate();
        }
    }

    public void PrevChar()
    {
        if (currChar > 0)
        {
            currChar--;
            ChangeListPos(false);
            StatsUpdate();
        }
    }

    void ChangeListPos(bool next)
    {
        Vector3 tempPos = transform.position;

        if (next)
            tempPos.x += -4f;
        else
            tempPos.x += 4f;

        transform.position = tempPos;
    }

    public void ScenePressed(int numb)
    {
        switch(numb)
        {
            case 0:
                arena = 0;
                break;

            case 1:
                arena = 1;
                break;

            case 2:
                arena = 2;
                break;
        }
    }

    void StatsUpdate()
    {
        List<int> temp = FindObjectOfType<FileSave>().LoadStats(currChar);
        win.text = temp[0].ToString();
        losts.text = temp[1].ToString();
    }
}
