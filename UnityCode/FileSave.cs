using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileSave : MonoBehaviour {

    Testings test;

    void Start()
    {
        test = GetComponent<Testings>();
    }

    public void PlayerWin()
    {
        string hero = GetCurrHeroName(test.fighter);

        PlayerPrefs.SetInt(hero + "Wins", PlayerPrefs.GetInt(hero+"Wins") + 1);
        PlayerPrefs.Save();
        Debug.Log("Saved win for " + hero);
    }

    public List<int> LoadStats(int fight)
    {
        string hero = GetCurrHeroName(fight);

        List<int> temp = new List<int>();
        temp.Add(PlayerPrefs.GetInt(hero + "Wins"));
        temp.Add(PlayerPrefs.GetInt(hero + "Losts"));

        return temp;
    }

    public void ResetStat()
    {
        for(int i=0;i<3;i++)
        {
            PlayerPrefs.SetInt(GetCurrHeroName(i) + "Wins", 0);
            PlayerPrefs.SetInt(GetCurrHeroName(i) + "Losts", 0);
        }
        Debug.Log("Сохранения удалены");
    }

    string GetCurrHeroName(int fight)
    {
        switch(fight)
        {
            case 0:
                return "IceGolem";
            case 1:
                return "Robot";
            case 2:
                return "Zombie";
            default:
                return "Error";
        }
    }
}
