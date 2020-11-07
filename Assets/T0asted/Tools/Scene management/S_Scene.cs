using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Scene : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> EditorOnlyGameObjects = new List<GameObject>();

    private void Start()
    {
#if UNITY_EDITOR
        foreach (var item in EditorOnlyGameObjects)
        {
            item.SetActive(true);
        }
#else   
        foreach (var item in EditorOnlyGameObjects)
        {
            item.SetActive(false);
        }
#endif
    }
}
