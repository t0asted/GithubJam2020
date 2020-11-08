using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_CharacterSpawner : MonoBehaviour
{
    private CL_Character CharacterData;

    public bool SpawnCharacter(CL_Character CharacterDataPass)
    {
        CharacterData = CharacterDataPass;

        return true;
    }
}
