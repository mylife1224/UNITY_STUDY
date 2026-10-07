using System;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

// StudyLab 씬 일괄 생성기
// 메뉴: StudyLab > Build All Scenes
// 각 씬에 Main Camera + Directional Light + Lab(학습 스크립트 부착) 자동 구성.
// 스크립트 클래스명은 문자열+리플렉션으로 찾아서 GUID 문제를 피한다.
public static class StudyLabBuilder
{
    // [씬 저장 경로, 부착할 MonoBehaviour 클래스명]
    private static readonly string[][] Scenes = new string[][]
    {
        new string[] { "Assets/_StudyLab/Stage00_Setup/Scenes/S00_01_CSharpInUnity/S00_01_CSharpInUnity.unity", "HelloCSharp" },
        new string[] { "Assets/_StudyLab/Stage00_Setup/Scenes/S00_02_VariablesMemory/S00_02_VariablesMemory.unity", "VariablesMemory" },
        new string[] { "Assets/_StudyLab/Stage01_CSharpBasic/Scenes/S01_01_Movement_IfLoop/S01_01_Movement_IfLoop.unity", "PlayerMover_Basic" },
        new string[] { "Assets/_StudyLab/Stage01_CSharpBasic/Scenes/S01_02_ArrayList_Bullet/S01_02_ArrayList_Bullet.unity", "BulletManager_Array" },
        new string[] { "Assets/_StudyLab/Stage01_CSharpBasic/Scenes/S01_03_ClassStruct/S01_03_ClassStruct.unity", "EnemyData" },
        new string[] { "Assets/_StudyLab/Stage01_CSharpBasic/Scenes/S01_04_InputNewSystem/S01_04_InputNewSystem.unity", "InputReader" },
        new string[] { "Assets/_StudyLab/Stage01_CSharpBasic/Scenes/S01_05_PrefabInstantiate/S01_05_PrefabInstantiate.unity", "PrefabSpawner" },
        new string[] { "Assets/_StudyLab/Stage02_UnityMid/Scenes/S02_01_Physics_Collision/S02_01_Physics_Collision.unity", "Bouncer" },
        new string[] { "Assets/_StudyLab/Stage02_UnityMid/Scenes/S02_02_Coroutine_Time/S02_02_Coroutine_Time.unity", "TimerCoroutine" },
        new string[] { "Assets/_StudyLab/Stage02_UnityMid/Scenes/S02_03_UI_Event/S02_03_UI_Event.unity", "UIManager_Basic" },
        new string[] { "Assets/_StudyLab/Stage02_UnityMid/Scenes/S02_04_ScriptableObject/S02_04_ScriptableObject.unity", "Inventory_Basic" },
        new string[] { "Assets/_StudyLab/Stage02_UnityMid/Scenes/S02_05_ObjectPool_Basic/S02_05_ObjectPool_Basic.unity", "SimpleObjectPool" },
        new string[] { "Assets/_StudyLab/Stage03_CSharpAdv/Scenes/S03_01_Generic_Delegate/S03_01_Generic_Delegate.unity", "EventBus" },
        new string[] { "Assets/_StudyLab/Stage03_CSharpAdv/Scenes/S03_02_LINQ_Lambda/S03_02_LINQ_Lambda.unity", "EnemyFilter_LINQ" },
        new string[] { "Assets/_StudyLab/Stage03_CSharpAdv/Scenes/S03_03_Interface_Polymorphism/S03_03_Interface_Polymorphism.unity", "DamageSystem" },
        new string[] { "Assets/_StudyLab/Stage03_CSharpAdv/Scenes/S03_04_Async_Await/S03_04_Async_Await.unity", "SceneLoaderAsync" },
        new string[] { "Assets/_StudyLab/Stage04_DataStructure/Scenes/S04_01_Stack_Undo/S04_01_Stack_Undo.unity", "Stack_Undo" },
        new string[] { "Assets/_StudyLab/Stage04_DataStructure/Scenes/S04_02_Queue_WaveSpawn/S04_02_Queue_WaveSpawn.unity", "Queue_Spawner" },
        new string[] { "Assets/_StudyLab/Stage04_DataStructure/Scenes/S04_03_LinkedList_Train/S04_03_LinkedList_Train.unity", "LinkedList_Train" },
        new string[] { "Assets/_StudyLab/Stage04_DataStructure/Scenes/S04_04_Dictionary_Inventory/S04_04_Dictionary_Inventory.unity", "Dict_Inventory" },
        new string[] { "Assets/_StudyLab/Stage04_DataStructure/Scenes/S04_05_Tree_SkillTree/S04_05_Tree_SkillTree.unity", "Tree_SkillNode" },
        new string[] { "Assets/_StudyLab/Stage04_DataStructure/Scenes/S04_06_Heap_PriorityTarget/S04_06_Heap_PriorityTarget.unity", "Heap_PriorityTarget" },
        new string[] { "Assets/_StudyLab/Stage05_Algorithm/Scenes/S05_01_Sort_Bar/S05_01_Sort_Bar.unity", "SortVisualizer" },
        new string[] { "Assets/_StudyLab/Stage05_Algorithm/Scenes/S05_02_BFSDFS_Maze/S05_02_BFSDFS_Maze.unity", "Maze_BFSDFS" },
        new string[] { "Assets/_StudyLab/Stage05_Algorithm/Scenes/S05_03_BinarySearch/S05_03_BinarySearch.unity", "BinarySearchDemo" },
        new string[] { "Assets/_StudyLab/Stage05_Algorithm/Scenes/S05_04_RecursionDP_Fibonacci/S05_04_RecursionDP_Fibonacci.unity", "DP_Fibonacci" },
        new string[] { "Assets/_StudyLab/Stage05_Algorithm/Scenes/S05_05_AStar_GridMove/S05_05_AStar_GridMove.unity", "AStar_Pathfinding" },
        new string[] { "Assets/_StudyLab/Stage06_MiniGame/Scenes/S06_01_TowerDefense/S06_01_TowerDefense.unity", "TowerDefense_Main" },
        new string[] { "Assets/_StudyLab/Stage06_MiniGame/Scenes/S06_02_Roguelike_Inventory/S06_02_Roguelike_Inventory.unity", "Roguelike_Inventory" },
        new string[] { "Assets/_StudyLab/Stage06_MiniGame/Scenes/S06_03_Puzzle_BFS/S06_03_Puzzle_BFS.unity", "SlidingPuzzle_BFS" },
        new string[] { "Assets/_StudyLab/Stage07_UnityAdv/Scenes/S07_01_URP_Optimization/S07_01_URP_Optimization.unity", "DrawCallProfiler" },
        new string[] { "Assets/_StudyLab/Stage07_UnityAdv/Scenes/S07_02_Addressables_Concept/S07_02_Addressables_Concept.unity", "AddressableLoader_Concept" },
        new string[] { "Assets/_StudyLab/Stage07_UnityAdv/Scenes/S07_02_ex_Addressables/S07_02_ex_Addressables.unity", "AddressableLoader_Real" },
        new string[] { "Assets/_StudyLab/Stage07_UnityAdv/Scenes/S07_03_Camera_Direction/S07_03_Camera_Direction.unity", "CameraDirector" },
        new string[] { "Assets/_StudyLab/Stage07_UnityAdv/Scenes/S07_03_ex_Cinemachine/S07_03_ex_Cinemachine.unity", "CameraDirector_Real" },
        new string[] { "Assets/_StudyLab/Stage07_UnityAdv/Scenes/S07_04_Parallel_Basic/S07_04_Parallel_Basic.unity", "ParallelMoveDemo" },
        new string[] { "Assets/_StudyLab/Stage07_UnityAdv/Scenes/S07_04_ex_JobBurst/S07_04_ex_JobBurst.unity", "ParallelMoveJob_Real" },
        new string[] { "Assets/_StudyLab/Stage07_UnityAdv/Scenes/S07_05_Profiler_GC/S07_05_Profiler_GC.unity", "GCFreeDemo" },
        new string[] { "Assets/_StudyLab/Stage08_Practice/Scenes/S08_01_Patterns/S08_01_Patterns.unity", "PatternsDemo" },
        new string[] { "Assets/_StudyLab/Stage08_Practice/Scenes/S08_02_SaveSystem/S08_02_SaveSystem.unity", "SaveSystem_JSON" },
        new string[] { "Assets/_StudyLab/Stage08_Practice/Scenes/S08_03_Testing_Readme/S08_03_Testing_Readme.unity", "InventoryDemo" },
        new string[] { "Assets/_StudyLab/Stage08_Practice/Scenes/S08_04_FinalBuild/S08_04_FinalBuild.unity", "BuildChecklist" },
    };

    [MenuItem("StudyLab/Build All Scenes")]
    public static void BuildAll()
    {
        int ok = 0, fail = 0;
        foreach (string[] entry in Scenes)
        {
            try
            {
                BuildOne(entry[0], entry[1]);
                ok++;
            }
            catch (Exception e)
            {
                fail++;
                Debug.LogError("[StudyLab] " + entry[0] + " 생성 실패: " + e.Message);
            }
        }
        AssetDatabase.Refresh();
        Debug.Log("[StudyLab] 씬 생성 완료: 성공 " + ok + ", 실패 " + fail + " / 전체 " + Scenes.Length);
    }

    private static void BuildOne(string scenePath, string componentName)
    {
        string dir = Path.GetDirectoryName(scenePath);
        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);

        var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

        // WHY: 매 씬 동일한 최소 관찰 환경 (카메라+빛) 보장
        GameObject cam = new GameObject("Main Camera");
        cam.AddComponent<Camera>();
        cam.tag = "MainCamera";
        cam.transform.position = new Vector3(0, 5, -10);
        cam.transform.LookAt(Vector3.zero);

        GameObject light = new GameObject("Directional Light");
        Light l = light.AddComponent<Light>();
        l.type = LightType.Directional;
        light.transform.rotation = Quaternion.Euler(50f, -30f, 0f);

        GameObject lab = new GameObject("Lab");
        Type t = FindMonoType(componentName);
        if (t != null)
            lab.AddComponent(t);
        else
            Debug.LogWarning("[StudyLab] 클래스 미발견(부착 생략): " + componentName + " -> " + scenePath);

        EditorSceneManager.SaveScene(scene, scenePath);
    }

    [MenuItem("StudyLab/Diagnose Tests")]
    public static void DiagnoseTests()
    {
        foreach (var a in UnityEditor.Compilation.CompilationPipeline.GetAssemblies())
        {
            if (a.name == "StudyLab.Tests" || a.name == "Assembly-CSharp")
            {
                Debug.Log("[StudyLab] asm=" + a.name + " files=" + a.sourceFiles.Length +
                    " refs=[" + string.Join(",", a.allReferences) + "]");
                foreach (string f in a.sourceFiles)
                    if (f.EndsWith("InventoryLogic.cs") || f.EndsWith("InventoryTests.cs"))
                        Debug.Log("[StudyLab] contains: " + f + " in " + a.name);
            }
        }
    }

    [MenuItem("StudyLab/Diagnose Missing")]
    public static void Diagnose()
    {
        string[] paths = new string[]
        {
            "Assets/_StudyLab/Stage05_Algorithm/Scripts/AStar_Pathfinding.cs",
            "Assets/_StudyLab/Stage06_MiniGame/Scripts/TowerDefense_Main.cs",
        };
        foreach (string p in paths)
        {
            UnityEngine.Object obj = AssetDatabase.LoadMainAssetAtPath(p);
            Debug.Log("[StudyLab] 경로: " + p + " / 로드: " + (obj == null ? "NULL" : obj.GetType().Name));
            var mono = AssetDatabase.LoadAssetAtPath<MonoScript>(p);
            Debug.Log("[StudyLab] MonoScript: " + (mono == null ? "NULL" : "OK") +
                " / GetClass: " + (mono == null || mono.GetClass() == null ? "NULL" : mono.GetClass().FullName));
        }
        int count = 0;
        foreach (var t in TypeCache.GetTypesDerivedFrom<MonoBehaviour>()) count++;
        Debug.Log("[StudyLab] TypeCache MonoBehaviour 수: " + count);
    }

    private static Type FindMonoType(string className)
    {
        // [빌더] WHY: AppDomain 리플렉션은 로드 타이밍에 따라 못 찾는 경우가 있어
        // 에디터 표준 방식(TypeCache + MonoScript 애셋 조회) 2단계로 찾는다
        foreach (var t in TypeCache.GetTypesDerivedFrom<MonoBehaviour>())
        {
            if (t.Name == className)
                return t;
        }
        foreach (string guid in AssetDatabase.FindAssets(className + " t:MonoScript"))
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            var mono = AssetDatabase.LoadAssetAtPath<MonoScript>(path);
            if (mono != null)
            {
                Type c = mono.GetClass();
                if (c != null && c.Name == className)
                    return c;
            }
        }
        Debug.LogWarning("[StudyLab] 진단: '" + className + "' 스크립트 애셋/컴파일 확인 요망. Project 창에 파일 있는지, Console에 빨간 에러 있는지 확인.");
        return null;
    }
}
