using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public int fireHazardLevel = 1;

    [SerializeField]
    private int fireHazardModifier = 1000;

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

    bool firstSuccessfulGen = false;

    void Update()
    {
        if (Random.Range(0, fireHazardModifier/fireHazardLevel) == Random.Range(0, fireHazardModifier/fireHazardLevel)) {
            fireList[Random.Range(1,fireList.Count)].SetActive(true);
            //TODO: Find better way to control randomness of fire for each level
            if (firstSuccessfulGen) fireHazardModifier = fireHazardModifier * 5;
        }
    }
}
