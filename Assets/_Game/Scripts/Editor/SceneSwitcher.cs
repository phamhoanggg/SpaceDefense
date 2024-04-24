using UnityEditor;
using UnityEditor.SceneManagement;

public class SceneSwitcher
{
    private const string ScenesFolderPath = "Assets/_Game/Scenes/";

    [MenuItem("Scenes/Load #1")]
    public static void OpenLoad()
    {
        OpenScene("Load");
    }

    [MenuItem("Scenes/Load #1", true)]
    public static bool OpenLoadValidate()
    {
        return OpenSceneValidate("Load");
    }

    [MenuItem("Scenes/Menu #2")]
    public static void OpenMenu()
    {
        OpenScene("Menu");
    }

    [MenuItem("Scenes/Menu #2", true)]
    public static bool OpenMenuValidate()
    {
        return OpenSceneValidate("Menu");
    }

    [MenuItem("Scenes/SelectLevel #3")]
    public static void OpenSelectLevel()
    {
        OpenScene("SelectLevel");
    }

    [MenuItem("Scenes/SelectLevel #3", true)]
    public static bool OpenSelectLevelValidate()
    {
        return OpenSceneValidate("SelectLevel");
    }

    [MenuItem("Scenes/GamePlay #4")]
    public static void OpenGamePlay()
    {
        OpenScene("GamePlay");
    }

    [MenuItem("Scenes/GamePlay #4", true)]
    public static bool OpenGamePlayValidate()
    {
        return OpenSceneValidate("GamePlay");
    }

    [MenuItem("Scenes/LevelDesign #5")]
    public static void OpenLevelDesign()
    {
        OpenScene("LevelDesign");
    }

    [MenuItem("Scenes/LevelDesign #5", true)]
    public static bool OpenLevelDesignValidate()
    {
        return OpenSceneValidate("LevelDesign");
    }

    [MenuItem("Scenes/Test #T")]
    public static void OpenTest()
    {
        OpenScene("Test");
    }

    [MenuItem("Scenes/Test #T", true)]
    public static bool OpenTestValidate()
    {
        return OpenSceneValidate("Test");
    }

    private static void OpenScene(string sceneName)
    {
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            EditorSceneManager.OpenScene(ScenesFolderPath + sceneName + ".unity");
        }
    }

    private static bool OpenSceneValidate(string sceneName)
    {
        return System.IO.File.Exists(ScenesFolderPath + sceneName + ".unity");
    }
}