using UnityEngine;

public class S_OpenCloseGameScene : S_UISceneManager
{
    [SerializeField]
    private Scenes_Level sceneEnum = Scenes_Level.None;

    public void OpenScene()
    {
        OpenScene(sceneEnum, true);
    }

    public void CloseScene()
    {
        CloseScene(sceneEnum);
    }

    public void SetScene(Scenes_Level scene)
    {
        sceneEnum = scene;
    }
}

public enum Scenes_Level
{
    None
}