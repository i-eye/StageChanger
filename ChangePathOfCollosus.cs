using HG;
using RoR2;
using RoR2.ExpansionManagement;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace IEye.StageRearrange
{
    public class ChangePathOfCollosus()
    {
        private static GameObject basePortal = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC2/PortalColossus.prefab").WaitForCompletion();
        private static GameObject path2Portal = R2API.PrefabAPI.InstantiateClone(basePortal, "path2");
        private static GameObject path3Portal = R2API.PrefabAPI.InstantiateClone(basePortal, "path3");

        private static SceneDef templeDef = Addressables.LoadAssetAsync<SceneDef>("RoR2/DLC2/lemuriantemple/lemuriantemple.asset").WaitForCompletion();
        private static SceneDef habitatDef = Addressables.LoadAssetAsync<SceneDef>("RoR2/DLC2/habitat/habitat.asset").WaitForCompletion();
        private static SceneDef meridian = Addressables.LoadAssetAsync<SceneDef>("RoR2/DLC2/meridian/meridian.asset").WaitForCompletion();
        private static SceneDef habitatNight;

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
            
            SceneExitController.onBeginExit += SceneExitController_onBeginExit;

            PortalSpawner colossusSpawner = new PortalSpawner();
            foreach(PortalSpawner spawner in teleporter.GetComponents<PortalSpawner>()){
                if (spawner.spawnMessageToken == "PORTAL_STORM_OPEN")
                    colossusSpawner = spawner;
            }
            string[] lemuriantempleArray = { "lemuriantemple" };
            colossusSpawner.validStages = lemuriantempleArray;
            colossusSpawner.requiredEventFlag = "colossusStarted";
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
            newComp.requiredEventFlag = "colossusStarted";                    
            newComp.validStages = habitatArray;
            newComp.portalSpawnCard = iscPortal3;





            ArrayUtils.ArrayAppend(ref teleporter.GetComponent<TeleporterInteraction>().portalSpawners, newComp);

            On.EntityStates.Missions.Goldshores.Exit.IsValidStormTier += Exit_IsValidStormTier;

            On.EntityStates.Missions.Goldshores.Exit.OnEnter += Exit_OnEnter;
            
        }

        private void SceneExitController_onBeginExit(SceneExitController obj)
        {
            if(obj.isColossusPortal && !Run.instance.GetEventFlag("colossusStarted"))
            {
                Run.instance.SetEventFlag("colossusStarted");
            } else if (!obj.isColossusPortal && Run.instance.GetEventFlag("colossusStarted"))
            {
                Run.instance.ResetEventFlag("colossusStarted");
            }
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



