using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Scenes
{
    [MenuItem("Scenes/Root Scene #z")]
    private static void OpenRootScene()
    {
        EditorSceneManager.OpenScene(Application.dataPath + "/Scenes/RootScene.unity");
    }

    [MenuItem("Scenes/Story Scene #x")]
    private static void OpenStoryScene()
    {
        EditorSceneManager.OpenScene(Application.dataPath + "/Scenes/Story.unity");
    }

    [MenuItem("Scenes/Run Scene #c")]
    private static void OpenRunScene()
    {
        EditorSceneManager.OpenScene(Application.dataPath + "/Scenes/Main.unity");
    }
}
