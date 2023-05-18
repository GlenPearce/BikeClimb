using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCollision : MonoBehaviour
{
    //This code called the fallen method from PlayerMvmt when the collider hits another object.

    public PlayerMvmt playerMvmt;
    void OnTriggerEnter(Collider other)
    {
        if (other.name != "Checkpoint1" & other.name != "Checkpoint2" & other.name != "Checkpoint3" & other.name != "Checkpoint4" & other.name != "GameAndMenuMan")
        {
            playerMvmt.fallen();
        }

    }
}
