using UnityEngine.Events;

public class S_Boot : S_SceneGameMain
{
    public UnityEvent m_OnBoot;

    private void Start()
    {
        m_OnBoot.Invoke();
    }
}
