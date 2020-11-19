#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class S_FollowGameObjectInSceneView : MonoBehaviour
{
    void Awake()
    {
        Debug.Log("Editor causes this Awake");
    }

    void Update()
    {
        //Debug.Log("Framing");
        SceneView.FrameLastActiveSceneView();
    }
}
#endif