using UnityEngine;
using UnityEngine.Events;

public class S_OrientationModifiers : MonoBehaviour
{
    [SerializeField]
    private UnityEvent m_Landscape = null;
    [SerializeField]
    private UnityEvent m_Portrait = null;
    [SerializeField]
    private Transform transformToModify;

    private ScreenOrientation m_orientation = ScreenOrientation.Unknown;

    private void Update()
    {
        if(m_orientation != Screen.orientation)
        {
            m_orientation = Screen.orientation;
            if (Screen.orientation == ScreenOrientation.Portrait)
            {
                m_Portrait.Invoke();
            }
            else
            {
                m_Landscape.Invoke();
            }
        }
    }
}

public class Transforms
{

}