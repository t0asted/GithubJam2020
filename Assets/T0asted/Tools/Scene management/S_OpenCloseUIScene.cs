using UnityEngine;

public class S_OpenCloseUIScene : S_UISceneManager
{
    [SerializeField]
    private Scenes_UI sceneEnum = Scenes_UI.None;

    public void OpenScene()
    {
        OpenScene(sceneEnum, true);
    }

    public void CloseScene()
    {
        CloseScene(sceneEnum);
    }

    public void SetScene(Scenes_UI scene)
    {
        sceneEnum = scene;
    }

}
