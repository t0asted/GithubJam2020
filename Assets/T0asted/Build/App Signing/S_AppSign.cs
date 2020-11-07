#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class S_AppSign : MonoBehaviour
{
    [MenuItem("Joshua's Tools/Build/Development")]
    public static void BuildDevelopment()
    {
        AppSign(BuildType.Development);
    }

    [MenuItem("Joshua's Tools/Build/Release")]
    public static void BuildRelease()
    {
        AppSign(BuildType.Release);
    }

    public static void AppSign(BuildType m_BuildType)
    {
        switch (m_BuildType)
        {
            case BuildType.Development:
                EditorUserBuildSettings.buildAppBundle = false;
                PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.Mono2x);
                PlayerSettings.Android.keystoreName = null;
                PlayerSettings.Android.keyaliasName = null;
                PlayerSettings.Android.keystorePass = null;
                PlayerSettings.Android.keyaliasPass = null;
                break;
            case BuildType.Release:
                EditorUserBuildSettings.buildAppBundle = true;
                PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);
                PlayerSettings.Android.keystoreName = "G:/Projects/Rumba/Key/RumbaKeyStore.keystore";
                PlayerSettings.Android.keyaliasName = "rumba";
                PlayerSettings.Android.keystorePass = "J0shua321";
                PlayerSettings.Android.keyaliasPass = "J0shua321";

                PlayerSettings.Android.bundleVersionCode++;
                PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARMv7 | AndroidArchitecture.ARM64;
                break;
        };
    }
}

public enum BuildType
{
    Development,
    Release
}
#endif