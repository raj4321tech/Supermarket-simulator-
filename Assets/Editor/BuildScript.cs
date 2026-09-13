using UnityEditor;

public static class BuildScript
{
    public static void BuildAndroid()
    {
        BuildPipeline.BuildPlayer(
            new[] { "Assets/Scenes/Main.unity" },
            "Build/SupermarketSimulator.apk",
            BuildTarget.Android,
            BuildOptions.None
        );
    }
}
