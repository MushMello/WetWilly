using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public int fireHazardLevel = 1;

    [SerializeField]
    private float fireHazardModifier = .5f;

    GameObject fireObjects;
    List<GameObject> fireList = new List<GameObject>();

    void Start()
    {
        fireObjects = GameObject.Find("FireObjects");

        foreach (Transform child in fireObjects.transform)
        {
            fireList.Add(child.gameObject);
            fireList[fireList.Count-1].SetActive(false);
        }

        
    }

    private bool hasLost = false;

    private void CheckFires()
    {
        int fireCount = 0;
        foreach(GameObject eachObject in fireList)
        {
            if(eachObject.activeSelf)
            {
                fireCount++;
            }
        }

        if(fireCount >= 4)
        {
            StateHandler state = StateHandler.GetStateHandler();
            if(state && !hasLost)
            {
                hasLost = true;
                state.DisplayAnnouncementAndWarp(false, GameScene.Overworld, true);
            }
        }
    }

    bool firstSuccessfulGen = false;

    void Update()
    {
        if(Random.value < fireHazardModifier || !firstSuccessfulGen)
        {
            fireList[Random.Range(0, fireList.Count)].SetActive(true);
            firstSuccessfulGen = true;
        }
        CheckFires();
    }
}
