using BepInEx;
using BepInEx.Configuration;
using HG;
using R2API;
using RoR2;
using RoR2.ExpansionManagement;
using EntityStates;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace IEye.StageRearrange
{
    [BepInPlugin(PLUGINGUID, PluginName, PluginVersion)]
    public class StageChangerPlugin : BaseUnityPlugin
    {

        private ConfigEntry<int> villageStage;
        private ConfigEntry<bool> changePathOfColossus;

        public const string PLUGINGUID = "IEye.StageRearrange";
        public const string PluginName = "StageRearrange";
        public const string PluginVersion = "0.1.0";

        public void Awake()
        {
            villageStage = Config.Bind("General",
                "Shattered Abodes Stage",
                3,
                "Which stage Shattered Abodes should be(default 3)(only set from 1-5)");

            changePathOfColossus = Config.Bind("General",
                "Change Path of Colossus",
                true,
                "Change Path of Colossus to start from Reformed Altar regards of point in stages");
            


            Log.Init(Logger);

            //Log.Message(villageStage.Value);
            new ChangeAbodes().Init(villageStage.Value);
            if (changePathOfColossus.Value)
            {
                new ChangePathOfCollosus().Init();
            }
            

            
        }
    }

    public class ChangeAbodes()
    {
        private static SceneCollection loopSgStage1 = Addressables.LoadAssetAsync<SceneCollection>("RoR2/Base/SceneGroups/loopSgStage1.asset").WaitForCompletion();
        private static SceneCollection loopSgStage2 = Addressables.LoadAssetAsync<SceneCollection>("RoR2/Base/SceneGroups/loopSgStage2.asset").WaitForCompletion();
        private static SceneCollection loopSgStage3 = Addressables.LoadAssetAsync<SceneCollection>("RoR2/Base/SceneGroups/loopSgStage3.asset").WaitForCompletion();
        private static SceneCollection loopSgStage4 = Addressables.LoadAssetAsync<SceneCollection>("RoR2/Base/SceneGroups/loopSgStage4.asset").WaitForCompletion();
        private static SceneCollection loopSgStage5 = Addressables.LoadAssetAsync<SceneCollection>("RoR2/Base/SceneGroups/loopSgStage5.asset").WaitForCompletion();

        private static SceneCollection sgStage1 = Addressables.LoadAssetAsync<SceneCollection>("RoR2/Base/SceneGroups/sgStage1.asset").WaitForCompletion();
        private static SceneCollection sgStage2 = Addressables.LoadAssetAsync<SceneCollection>("RoR2/Base/SceneGroups/sgStage2.asset").WaitForCompletion();
        private static SceneCollection sgStage3 = Addressables.LoadAssetAsync<SceneCollection>("RoR2/Base/SceneGroups/sgStage3.asset").WaitForCompletion();
        private static SceneCollection sgStage4 = Addressables.LoadAssetAsync<SceneCollection>("RoR2/Base/SceneGroups/sgStage4.asset").WaitForCompletion();
        private static SceneCollection sgStage5 = Addressables.LoadAssetAsync<SceneCollection>("RoR2/Base/SceneGroups/sgStage5.asset").WaitForCompletion();

        private static SceneDef village = Addressables.LoadAssetAsync<SceneDef>("RoR2/DLC2/village/village.asset").WaitForCompletion();
        private static SceneDef villagenight = Addressables.LoadAssetAsync<SceneDef>("RoR2/DLC2/villagenight/villagenight.asset").WaitForCompletion();

        public void Init(int stageChoice)
        {
            if (stageChoice == 1)
                return;

            village.shouldUpdateSceneCollectionAfterLooping = false;
            villagenight.shouldUpdateSceneCollectionAfterLooping = false;

            ArrayUtils.ArrayRemoveAtAndResize(ref loopSgStage1._sceneEntries, 6);
            ArrayUtils.ArrayRemoveAtAndResize(ref sgStage1._sceneEntries, 6);

            SceneCollection.SceneEntry VillageDay = new SceneCollection.SceneEntry
            {
                sceneDef = village,
                weight = 1,
                weightMinusOne = 0
            };
            SceneCollection.SceneEntry VillageNight = new SceneCollection.SceneEntry
            {
                sceneDef = villagenight,
                weight = 1,
                weightMinusOne = 0
            };
            switch (stageChoice)
            {
                case 2:
                    village.stageOrder = 2;
                    ArrayUtils.ArrayAppend(ref sgStage2._sceneEntries, VillageDay);
                    ArrayUtils.ArrayAppend(ref loopSgStage2._sceneEntries, VillageNight);
                    village.destinationsGroup = sgStage3;
                    villagenight.destinationsGroup = loopSgStage3;
                    break;
                case 3:
                    village.stageOrder = 3;
                    ArrayUtils.ArrayAppend(ref sgStage3._sceneEntries, VillageDay);
                    ArrayUtils.ArrayAppend(ref loopSgStage3._sceneEntries, VillageNight);
                    village.destinationsGroup = sgStage4;
                    villagenight.destinationsGroup = loopSgStage4;
                    break;
                case 4:
                    village.stageOrder = 4;
                    ArrayUtils.ArrayAppend(ref sgStage4._sceneEntries, VillageDay);
                    ArrayUtils.ArrayAppend(ref loopSgStage4._sceneEntries, VillageNight);
                    village.destinationsGroup = sgStage5;
                    villagenight.destinationsGroup = loopSgStage5;
                    break;
                case 5:
                    village.stageOrder = 5;
                    ArrayUtils.ArrayAppend(ref sgStage5._sceneEntries, VillageDay);
                    ArrayUtils.ArrayAppend(ref loopSgStage5._sceneEntries, VillageNight);
                    village.destinationsGroup = sgStage1;
                    villagenight.destinationsGroup = loopSgStage1;
                    break;
                default:
                    Log.Error("End user: it said 1-5. I'm disappointed.");
                    village.stageOrder = 3;
                    ArrayUtils.ArrayAppend(ref sgStage3._sceneEntries, VillageDay);
                    ArrayUtils.ArrayAppend(ref loopSgStage3._sceneEntries, VillageNight);
                    village.destinationsGroup = sgStage4;
                    villagenight.destinationsGroup = loopSgStage4;
                    break;
            }
            

        }
        


    }
    public class ChangePathOfCollosus()
    {
        private static GameObject basePortal = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC2/PortalColossus.prefab").WaitForCompletion();
        private static GameObject path2Portal = R2API.PrefabAPI.InstantiateClone(basePortal, "path2");
        private static GameObject path3Portal = R2API.PrefabAPI.InstantiateClone(basePortal, "path3");

        private static SceneDef templeDef = Addressables.LoadAssetAsync<SceneDef>("RoR2/DLC2/lemuriantemple/lemuriantemple.asset").WaitForCompletion();
        private static SceneDef habitatDef = Addressables.LoadAssetAsync<SceneDef>("RoR2/DLC2/habitat/habitat.asset").WaitForCompletion();
        private static SceneDef meridian = Addressables.LoadAssetAsync<SceneDef>("RoR2/DLC2/meridian/meridian.asset").WaitForCompletion();

        private static GameObject teleporter = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Teleporters/Teleporter1.prefab").WaitForCompletion();

        private static InteractableSpawnCard iscPortal = Addressables.LoadAssetAsync<InteractableSpawnCard>("RoR2/DLC2/iscColossusPortal.asset").WaitForCompletion();
        private static InteractableSpawnCard iscPortal2;
        public void Init()
        {
            SceneExitController portal1Controller = basePortal.GetComponent<SceneExitController>();
            portal1Controller.tier2AlternateDestinationScene = null;
            portal1Controller.tier3AlternateDestinationScene = null;
            portal1Controller.tier4AlternateDestinationScene = null;
            portal1Controller.destinationScene = templeDef;

            SceneExitController portal2Controller = path2Portal.GetComponent<SceneExitController>();
            portal2Controller.tier2AlternateDestinationScene = null;
            portal2Controller.tier3AlternateDestinationScene = null;
            portal2Controller.tier4AlternateDestinationScene = null;
            portal2Controller.destinationScene = habitatDef;

            SceneExitController portal3Controller = path3Portal.GetComponent<SceneExitController>();
            portal3Controller.tier2AlternateDestinationScene = null;
            portal3Controller.tier3AlternateDestinationScene = null;
            portal3Controller.tier4AlternateDestinationScene = null;
            portal3Controller.destinationScene = meridian;

            // portal 2
            PortalSpawner colossusSpawner = new PortalSpawner();
            foreach(PortalSpawner spawner in teleporter.GetComponents<PortalSpawner>()){
                if (spawner.spawnMessageToken == "PORTAL_STORM_OPEN")
                    colossusSpawner = spawner;
            }
            string[] lemuriantempleArray = { "lemuriantemple" };
            colossusSpawner.validStages = lemuriantempleArray;

            iscPortal2 = new InteractableSpawnCard
            {
                prefab = path2Portal,
                sendOverNetwork = iscPortal.sendOverNetwork,
                hullSize = iscPortal.hullSize,
                nodeGraphType = iscPortal.nodeGraphType,
                requiredFlags = iscPortal.requiredFlags,
                forbiddenFlags = iscPortal.forbiddenFlags,
                directorCreditCost = iscPortal.directorCreditCost,
                occupyPosition = iscPortal.occupyPosition,
                eliteRules = iscPortal.eliteRules,
                orientToFloor = iscPortal.orientToFloor,
                slightlyRandomizeOrientation = iscPortal.slightlyRandomizeOrientation,
                skipSpawnWhenSacrificeArtifactEnabled = iscPortal.skipSpawnWhenSacrificeArtifactEnabled,
                skipSpawnWhenDevotionArtifactEnabled = iscPortal.skipSpawnWhenDevotionArtifactEnabled,
                maxSpawnsPerStage = iscPortal.maxSpawnsPerStage,
                prismaticTrialSpawnChance = iscPortal.prismaticTrialSpawnChance
            };
            colossusSpawner.portalSpawnCard = iscPortal2;


            // portal 3
            string[] habitatArray = { "habitat", "habitatfall" };

            InteractableSpawnCard iscPortal3 = new InteractableSpawnCard
            {
                prefab = path3Portal,
                sendOverNetwork = iscPortal.sendOverNetwork,
                hullSize = iscPortal.hullSize,
                nodeGraphType = iscPortal.nodeGraphType,
                requiredFlags = iscPortal.requiredFlags,
                forbiddenFlags = iscPortal.forbiddenFlags,
                directorCreditCost = iscPortal.directorCreditCost,
                occupyPosition = iscPortal.occupyPosition,
                eliteRules = iscPortal.eliteRules,
                orientToFloor = iscPortal.orientToFloor,
                slightlyRandomizeOrientation = iscPortal.slightlyRandomizeOrientation,
                skipSpawnWhenSacrificeArtifactEnabled = iscPortal.skipSpawnWhenSacrificeArtifactEnabled,
                skipSpawnWhenDevotionArtifactEnabled = iscPortal.skipSpawnWhenDevotionArtifactEnabled,
                maxSpawnsPerStage = iscPortal.maxSpawnsPerStage,
                prismaticTrialSpawnChance = iscPortal.prismaticTrialSpawnChance
            };

            
            var newComp = teleporter.AddComponent<PortalSpawner>();
            newComp.spawnChance = colossusSpawner.spawnChance;
            newComp.minSpawnDistance = colossusSpawner.minSpawnDistance;
            newComp.maxSpawnDistance = colossusSpawner.maxSpawnDistance;
            newComp.spawnPreviewMessageToken = colossusSpawner.spawnPreviewMessageToken;
            newComp.spawnMessageToken = colossusSpawner.spawnMessageToken;
            newComp.modelChildLocator = colossusSpawner.modelChildLocator;
            newComp.previewChildName = colossusSpawner.previewChildName;
            newComp.requiredExpansion = colossusSpawner.requiredExpansion;
            newComp.bannedEventFlag = colossusSpawner.bannedEventFlag;
            newComp.validStages = habitatArray;
            newComp.portalSpawnCard = iscPortal3;

            ArrayUtils.ArrayAppend(ref teleporter.GetComponent<TeleporterInteraction>().portalSpawners, newComp);

            On.EntityStates.Missions.Goldshores.Exit.IsValidStormTier += Exit_IsValidStormTier;

            On.EntityStates.Missions.Goldshores.Exit.OnEnter += Exit_OnEnter;
            
        }

        private void Exit_OnEnter(On.EntityStates.Missions.Goldshores.Exit.orig_OnEnter orig, EntityStates.Missions.Goldshores.Exit self)
        {

            if ((string.IsNullOrEmpty("FalseSonBossComplete") || !Run.instance.GetEventFlag("FalseSonBossComplete")) && IsValidStormTierGood() && (bool)DirectorCore.instance.TrySpawnObject(new DirectorSpawnRequest(iscPortal2, new DirectorPlacementRule
            {
                maxDistance = 30f,
                minDistance = 10f,
                placementMode = DirectorPlacementRule.PlacementMode.NearestNode,
                position = self.transform.position
            }, Run.instance.stageRng)))
            {
                Chat.SendBroadcastChat(new Chat.SimpleChatMessage
                {
                    baseToken = "PORTAL_STORM_OPEN"
                });
            }
            orig(self);
        }

        private bool Exit_IsValidStormTier(On.EntityStates.Missions.Goldshores.Exit.orig_IsValidStormTier orig, EntityStates.Missions.Goldshores.Exit self)
        {
            return false;
        }

        private bool IsValidStormTierGood()
        {
            Run instance = Run.instance;
            int stageOrder = instance.nextStageScene.stageOrder;
            ExpansionDef requiredExpansion = GoldshoresMissionController.instance.requiredExpansion;
            if (instance.IsExpansionEnabled(requiredExpansion))
            {
                return true;
            }
            return false;
        }
    }
}



