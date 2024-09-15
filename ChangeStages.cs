using HG;
using RoR2;
using System.Linq;
using UnityEngine.AddressableAssets;

namespace IEye.StageRearrange
{

    public class StageVars
    {
        public SceneDef def { get; set; }
        public SceneCollection.SceneEntry sceneEntry { get; set; }

        public StageVars(SceneDef newDef)
        {
            this.def = newDef;
            sceneEntry = new SceneCollection.SceneEntry
            {
                sceneDef = def,
                weight = 1,
                weightMinusOne = 0,
            };
        }
    }
    public class ChangeStages
    {
        private static SceneCollection[] sceneCollections = new SceneCollection[5];
        private static SceneCollection[] loopSceneCollections = new SceneCollection[5];
        

        private static StageVars village = new StageVars(Addressables.LoadAssetAsync<SceneDef>("RoR2/DLC2/village/village.asset").WaitForCompletion());
        private static StageVars villagenight = new StageVars(Addressables.LoadAssetAsync<SceneDef>("RoR2/DLC2/villagenight/villagenight.asset").WaitForCompletion());
        private static StageVars temple = new StageVars(Addressables.LoadAssetAsync<SceneDef>("RoR2/DLC2/lemuriantemple/lemuriantemple.asset").WaitForCompletion());
        private static StageVars habitat = new StageVars(Addressables.LoadAssetAsync<SceneDef>("RoR2/DLC2/habitat/habitat.asset").WaitForCompletion());
        private static StageVars habitatnight = new StageVars(Addressables.LoadAssetAsync<SceneDef>("RoR2/DLC2/habitatfall/habitatfall.asset").WaitForCompletion());
        private static StageVars lakes = new StageVars(Addressables.LoadAssetAsync<SceneDef>("RoR2/DLC2/lakes/lakes.asset").WaitForCompletion());
        private static StageVars lakesnight = new StageVars(Addressables.LoadAssetAsync<SceneDef>("RoR2/DLC2/lakesnight/lakesnight.asset").WaitForCompletion());



        private void AddNightAltsInternal(int stageNum, StageVars dayVar, StageVars nightVar)
        {
            ArrayUtils.ArrayAppend(ref sceneCollections[stageNum - 1]._sceneEntries, nightVar.sceneEntry);
            ArrayUtils.ArrayAppend(ref loopSceneCollections[stageNum - 1]._sceneEntries, dayVar.sceneEntry);
        }
        private void ChangeStageInternal(int stageNum, StageVars dayVar)
        {
            dayVar.def.stageOrder = stageNum;
            ArrayUtils.ArrayAppend(ref sceneCollections[stageNum - 1]._sceneEntries, dayVar.sceneEntry);
            ArrayUtils.ArrayAppend(ref loopSceneCollections[stageNum - 1]._sceneEntries, dayVar.sceneEntry);
            if(stageNum == 5)
            {
                dayVar.def.destinationsGroup = sceneCollections[0];
            } else
            {
                dayVar.def.destinationsGroup = sceneCollections[stageNum];
            }
            
        }
        private void ChangeStageInternal(int stageNum, StageVars dayVar, StageVars nightVar)
        {
            dayVar.def.stageOrder = stageNum;
            nightVar.def.stageOrder = stageNum;
            ArrayUtils.ArrayAppend(ref sceneCollections[stageNum - 1]._sceneEntries, dayVar.sceneEntry);
            ArrayUtils.ArrayAppend(ref loopSceneCollections[stageNum - 1]._sceneEntries, nightVar.sceneEntry);
            if (stageNum == 5)
            {
                dayVar.def.destinationsGroup = sceneCollections[0];
                nightVar.def.destinationsGroup = sceneCollections[0];
            }
            else
            {
                dayVar.def.destinationsGroup = sceneCollections[stageNum];
                nightVar.def.destinationsGroup = sceneCollections[stageNum];
            }
        }

        void ChangeStage(int stageNum, StageVars dayVar)
        {
            if(stageNum < 0 || stageNum > 5)
            {
                return;
            }
            ChangeStageInternal(stageNum, dayVar);
        }
        void ChangeStage(int stageNum, StageVars dayVar, StageVars nightVars, bool nightAlt)
        {
            if (stageNum < 0 || stageNum > 5)
            {
                return;
            }
            ChangeStageInternal(stageNum, dayVar, nightVars);
            if (nightAlt)
            {
                AddNightAltsInternal(stageNum, dayVar, nightVars);
            }
        }

        public void Init()
        {
            
        }

        private void PurgeStages(StageVars stage)
        {

            foreach(SceneCollection collection in sceneCollections)
            {
                for (int i = 0; i < collection._sceneEntries.Length; i++)
                {
                    if (collection.sceneEntries[i].sceneDef == stage.def)
                    {
                        ArrayUtils.ArrayRemoveAtAndResize(ref collection._sceneEntries, i);
                        break;
                    }
                }
            }
            foreach (SceneCollection collection in loopSceneCollections)
            {
                for (int i = 0; i < collection._sceneEntries.Length; i++)
                {
                    if (collection.sceneEntries[i].sceneDef == stage.def)
                    {
                        ArrayUtils.ArrayRemoveAtAndResize(ref collection._sceneEntries, i);
                        break;
                    }
                }
            }
        }
        public void EditStages(int villageStage, int templeStage, int habitatStage, int lakesStage, bool nightAsAlt)
        {
            for (int i = 0; i < 5; i++)
            {
                string preLoop = "RoR2/Base/SceneGroups/sgStage" + (i + 1) + ".asset";
                string postLoop = "RoR2/Base/SceneGroups/loopSgStage" + (i + 1) + ".asset";
                sceneCollections[i] = Addressables.LoadAssetAsync<SceneCollection>(preLoop).WaitForCompletion();
                loopSceneCollections[i] = Addressables.LoadAssetAsync<SceneCollection>(postLoop).WaitForCompletion();
            }

            PurgeStages(village);
            PurgeStages(villagenight);
            PurgeStages(temple);
            PurgeStages(habitat);
            PurgeStages(habitatnight);
            PurgeStages(lakes);
            PurgeStages(lakesnight);

            ChangeStage(villageStage, village, villagenight,nightAsAlt);
            ChangeStage(templeStage, temple);
            ChangeStage(habitatStage, habitat, habitatnight, nightAsAlt);
            ChangeStage(lakesStage,lakes,lakesnight, nightAsAlt);


        }
    }
}



