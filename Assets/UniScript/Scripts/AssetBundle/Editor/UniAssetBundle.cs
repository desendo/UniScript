﻿using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public partial class UniAssetBundle
{
    public static void BuildUniScriptScene(string sceneBundleName, string assetBundleName)
    {
        //if (Directory.Exists("Packs"))
        //    Directory.Delete("Packs", true);

        Directory.CreateDirectory("Packs");

        var path = "Assets/UniScript/Resources/uniscript_monolith.json";
        MergeCsx.CreateMonolith(path, assetBundleName);

        var randId = System.DateTime.Now.GetHashCode().ToString();
        var mf = BuildPipeline.BuildAssetBundles("./Packs/", BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);

        foreach (var name in mf.GetAllAssetBundles())
        {
            var hash = mf.GetAssetBundleHash(name);
            var id = name;
            var bundleData = new BundleData() { hash = hash.ToString(), id = id };
            File.WriteAllText("./Packs/" + name + ".json", JsonUtility.ToJson(bundleData));
        }

        File.Delete(path);
    }

    public class BundleData
    {
        public string id;
        public string hash;
    }
}
