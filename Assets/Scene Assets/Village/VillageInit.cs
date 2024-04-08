using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageInit : MonoBehaviour
{
    private int state;

    [SerializeField] GameObject GolemPortal;
    [SerializeField] BoxCollider2D TownHallDoor;
    [SerializeField] BoxCollider2D ForestDoor;
    [SerializeField] BoxCollider2D BlackSmithDoor;
    [SerializeField] BoxCollider2D AduoForpDoor;
    [SerializeField] BoxCollider2D LibraryDoor;

    [SerializeField] GameObject ForestRubble;
    [SerializeField] GameObject BridgeRubble;

    [SerializeField] GameObject GolemSpawner;
    [SerializeField] GameObject GolemDoor;

    private float t;

    private void Start()
    {
        state = PlayerAttributes.GlobalGameState;
        switch (state)
        {
            case 1:
                GolemPortal.SetActive(true);
                TownHallDoor.enabled = false;
                ForestDoor.enabled = false;
                BlackSmithDoor.enabled = false;
                AduoForpDoor.enabled = false;
                LibraryDoor.enabled = false;
                ForestRubble.SetActive(true);
                BridgeRubble.SetActive(true);
                break;
            case 2:
                
                GolemDoor.SetActive(false);
                TownHallDoor.enabled = true;
                ForestDoor.enabled = true;
                BlackSmithDoor.enabled = false;
                AduoForpDoor.enabled = false;
                LibraryDoor.enabled = false;
                ForestRubble.SetActive(false);
                BridgeRubble.SetActive(true);
                break;
            case 3:
                GolemDoor.SetActive(false);
                GolemPortal.SetActive(false);
                TownHallDoor.enabled = true;
                ForestDoor.enabled = true;
                BlackSmithDoor.enabled = true;
                AduoForpDoor.enabled = false;
                LibraryDoor.enabled = false;
                ForestRubble.SetActive(false);
                BridgeRubble.SetActive(true);
                break;
            case 4:
                GolemDoor.SetActive(false);
                GolemPortal.SetActive(false);
                TownHallDoor.enabled = true;
                ForestDoor.enabled = true;
                BlackSmithDoor.enabled = false;
                AduoForpDoor.enabled = true;
                LibraryDoor.enabled = false;
                ForestRubble.SetActive(false);
                BridgeRubble.SetActive(false);
                break;
            case 5:
                GolemDoor.SetActive(false);
                GolemPortal.SetActive(false);
                TownHallDoor.enabled = true;
                ForestDoor.enabled = true;
                BlackSmithDoor.enabled = false;
                AduoForpDoor.enabled = true;
                LibraryDoor.enabled = false;
                ForestRubble.SetActive(false);
                BridgeRubble.SetActive(false);
                break;
            case 6:
                GolemDoor.SetActive(false);
                GolemPortal.SetActive(false);
                TownHallDoor.enabled = true;
                ForestDoor.enabled = true;
                BlackSmithDoor.enabled = false;
                AduoForpDoor.enabled = true;
                LibraryDoor.enabled = true;
                ForestRubble.SetActive(false);
                BridgeRubble.SetActive(false);
                break;
            default:
                GolemDoor.SetActive(true);
                GolemPortal.SetActive(true);
                TownHallDoor.enabled = true;
                ForestDoor.enabled = true;
                BlackSmithDoor.enabled = true;
                AduoForpDoor.enabled = true;
                LibraryDoor.enabled = true;
                ForestRubble.SetActive(false);
                BridgeRubble.SetActive(false);
                break;
        }
    }
    private void Update()
    {
        if (state == 2)
        {
            
            GolemPortal.transform.localScale = Mathf.Lerp(1, 0, t) * new Vector3(0.3333333f, 0.3333333f, 0.3333333f);
            t += Time.deltaTime;
            if (GolemPortal.transform.localScale.magnitude == 0)
            {
                GolemPortal.SetActive(false);
            }
        }
    }
}