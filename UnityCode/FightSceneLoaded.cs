using UnityEngine;

public class FightSceneLoaded : MonoBehaviour {

    public GameObject[] models;
    private Testings tempLaunch;

	void Awake () {
        tempLaunch = FindObjectOfType<Testings>();

        switch (tempLaunch.fighter)
        {
            case 0:
                Instantiate(models[0], transform);
                break;
            case 1:
                Instantiate(models[1], transform);
                break;
            case 2:
                Instantiate(models[2], transform);
                break;
        }
	}
}
