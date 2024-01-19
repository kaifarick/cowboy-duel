using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Builder
{
    [MenuItem("Tools/Builder/Development")]
    private static void DevelopmentBuild()
    {
        CreateBuild(true,false);
    }
    
    [MenuItem("Tools/Builder/Development Device")]
    private static void DevelopmentBuildDevice()
    {
        CreateBuild(true,true);
    }
    
    [MenuItem("Tools/Builder/Rc")]
    private static void DevelopmentRc()
    {
        CreateBuild(false,false);
    }
    
    [MenuItem("Tools/Builder/Rc Device")]
    private static void DevelopmentRcDevice()
    {
        CreateBuild(false,true);
    }
    
    
    
    private static void CreateBuild(bool development, bool device)
    {
        List<string> define = new List<string>();
        
        if (development) {
            define.Add("DEBUG_LOGIC");
        }
        else {
            if(define.Contains("DEBUG_LOGIC")){
                define.Remove("DEBUG_LOGIC");
            }
        }
        
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, define.ToArray());
        
        PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android,
            development ? ScriptingImplementation.Mono2x : ScriptingImplementation.IL2CPP);
        

        PlayerSettings.Android.targetArchitectures = development ? AndroidArchitecture.ARMv7 :
            PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARM64;
        
        EditorUserBuildSettings.development = development;
        EditorUserBuildSettings.allowDebugging = development;
        
        var autoRunPlayer = device ? BuildOptions.AutoRunPlayer : BuildOptions.None;
        
        string buildType = development ? "_dev" : "_rc";
        string buildPath = "/Builds/";
        string fileName = "Mortal Battle" + buildType + ".apk";
        
        
        var scenes = new string[EditorBuildSettings.scenes.Length];
        for (int i = 0; i < scenes.Length; i++)
            scenes[i] = EditorBuildSettings.scenes[i].path;


        var pathString = Application.dataPath.Split("/");
        pathString = pathString.SkipLast(1).ToArray();
        var path = string.Join("/", pathString);
        string apkPath = path + buildPath + fileName;
        BuildPipeline.BuildPlayer(scenes, apkPath, BuildTarget.Android, autoRunPlayer);
    }
}
