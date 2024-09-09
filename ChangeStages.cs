using HG;
using RoR2;
using UnityEngine.AddressableAssets;

namespace IEye.StageRearrange
{
    public class ChangeStages()
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
        private static SceneDef temple = Addressables.LoadAssetAsync<SceneDef>("RoR2/DLC2/lemuriantemple/lemuriantemple.asset").WaitForCompletion();
        private static SceneDef habitat = Addressables.LoadAssetAsync<SceneDef>("RoR2/DLC2/habitat/habitat.asset").WaitForCompletion();
        private static SceneDef habitatnight = Addressables.LoadAssetAsync<SceneDef>("RoR2/DLC2/habitatfall/habitatfall.asset").WaitForCompletion();
        private static SceneDef lakes;
        private static SceneDef lakesnight;
        public void Init(int villageStage, int templeStage, int habitatStage, int lakesStage, bool nightAsAlt)
        {
            if (villageStage == 1)
                return;

            /*
            village.shouldUpdateSceneCollectionAfterLooping = false;
            villagenight.shouldUpdateSceneCollectionAfterLooping = false;
            temple.shouldUpdateSceneCollectionAfterLooping = false;
            habitat.shouldUpdateSceneCollectionAfterLooping = false;
            habitatnight.shouldUpdateSceneCollectionAfterLooping = false;
            lakes.shouldUpdateSceneCollectionAfterLooping = false;
            lakesnight.shouldUpdateSceneCollectionAfterLooping = false;
            */

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
            SceneCollection.SceneEntry Temple = new SceneCollection.SceneEntry
            {
                sceneDef = temple,
                weight = 1,
                weightMinusOne = 0
            };
            SceneCollection.SceneEntry Habitat = new SceneCollection.SceneEntry
            {
                sceneDef = habitat,
                weight = 1,
                weightMinusOne = 0
            };
            SceneCollection.SceneEntry HabitatNight = new SceneCollection.SceneEntry
            {
                sceneDef = habitatnight,
                weight = 1,
                weightMinusOne = 0
            };
            SceneCollection.SceneEntry Lakes = new SceneCollection.SceneEntry
            {
                sceneDef = lakes,
                weight = 1,
                weightMinusOne = 0,
            };
            SceneCollection.SceneEntry LakesNight = new SceneCollection.SceneEntry
            {
                sceneDef = lakes,
                weight = 1,
                weightMinusOne = 0,
            };

            switch (villageStage)
            {
                case 2:
                    village.stageOrder = 2;
                    villagenight.stageOrder = 2;
                    ArrayUtils.ArrayAppend(ref sgStage2._sceneEntries, VillageDay);
                    ArrayUtils.ArrayAppend(ref loopSgStage2._sceneEntries, VillageNight);
                    if (nightAsAlt)
                    {
                        ArrayUtils.ArrayAppend(ref sgStage2._sceneEntries, VillageNight);
                        ArrayUtils.ArrayAppend(ref loopSgStage2._sceneEntries, VillageDay);
                    }
                    village.destinationsGroup = sgStage3;
                    villagenight.destinationsGroup = loopSgStage3;
                    break;
                case 3:
                    village.stageOrder = 3;
                    villagenight.stageOrder = 3;
                    ArrayUtils.ArrayAppend(ref sgStage3._sceneEntries, VillageDay);
                    ArrayUtils.ArrayAppend(ref loopSgStage3._sceneEntries, VillageNight);
                    if (nightAsAlt)
                    {
                        ArrayUtils.ArrayAppend(ref sgStage3._sceneEntries, VillageNight);
                        ArrayUtils.ArrayAppend(ref loopSgStage3._sceneEntries, VillageDay);
                    }
                    village.destinationsGroup = sgStage4;
                    villagenight.destinationsGroup = loopSgStage4;
                    break;
                case 4:
                    village.stageOrder = 4;
                    villagenight.stageOrder = 4;
                    ArrayUtils.ArrayAppend(ref sgStage4._sceneEntries, VillageDay);
                    ArrayUtils.ArrayAppend(ref loopSgStage4._sceneEntries, VillageNight);
                    if (nightAsAlt)
                    {
                        ArrayUtils.ArrayAppend(ref sgStage4._sceneEntries, VillageNight);
                        ArrayUtils.ArrayAppend(ref loopSgStage4._sceneEntries, VillageDay);
                    }
                    village.destinationsGroup = sgStage5;
                    villagenight.destinationsGroup = loopSgStage5;
                    break;
                case 5:
                    village.stageOrder = 5;
                    villagenight.stageOrder = 5;
                    ArrayUtils.ArrayAppend(ref sgStage5._sceneEntries, VillageDay);
                    ArrayUtils.ArrayAppend(ref loopSgStage5._sceneEntries, VillageNight);
                    if (nightAsAlt)
                    {
                        ArrayUtils.ArrayAppend(ref sgStage5._sceneEntries, VillageNight);
                        ArrayUtils.ArrayAppend(ref loopSgStage5._sceneEntries, VillageDay);
                    }
                    village.destinationsGroup = sgStage1;
                    villagenight.destinationsGroup = loopSgStage1;
                    break;
                default:
                    Log.Error("End user: it said 1-5. I'm disappointed.");
                    village.stageOrder = 2;
                    villagenight.stageOrder = 2;
                    ArrayUtils.ArrayAppend(ref sgStage2._sceneEntries, VillageDay);
                    ArrayUtils.ArrayAppend(ref loopSgStage2._sceneEntries, VillageNight);
                    if (nightAsAlt)
                    {
                        ArrayUtils.ArrayAppend(ref sgStage2._sceneEntries, VillageNight);
                        ArrayUtils.ArrayAppend(ref loopSgStage2._sceneEntries, VillageDay);
                    }
                    village.destinationsGroup = sgStage3;
                    villagenight.destinationsGroup = loopSgStage3;
                    break;
            }

            switch (templeStage)
            {
                case 1:
                    temple.stageOrder = 1;
                    ArrayUtils.ArrayAppend(ref sgStage1._sceneEntries, Temple);
                    ArrayUtils.ArrayAppend(ref loopSgStage1._sceneEntries, Temple);
                    temple.destinationsGroup = sgStage2;
                    temple.destinationsGroup = loopSgStage2;
                    break;
                case 2:
                    temple.stageOrder = 2;
                    ArrayUtils.ArrayAppend(ref sgStage2._sceneEntries, Temple);
                    ArrayUtils.ArrayAppend(ref loopSgStage2._sceneEntries, Temple);
                    temple.destinationsGroup = sgStage3;
                    temple.destinationsGroup = loopSgStage3;
                    break;
                case 3:
                    temple.stageOrder = 3;
                    ArrayUtils.ArrayAppend(ref sgStage3._sceneEntries, Temple);
                    ArrayUtils.ArrayAppend(ref loopSgStage3._sceneEntries, Temple);
                    temple.destinationsGroup = sgStage4;
                    temple.destinationsGroup = loopSgStage4;
                    break;
                case 4:
                    temple.stageOrder = 4;
                    ArrayUtils.ArrayAppend(ref sgStage4._sceneEntries, Temple);
                    ArrayUtils.ArrayAppend(ref loopSgStage4._sceneEntries, Temple);
                    temple.destinationsGroup = sgStage5;
                    temple.destinationsGroup = loopSgStage5;
                    break;
                case 5:
                    temple.stageOrder = 5;
                    ArrayUtils.ArrayAppend(ref sgStage5._sceneEntries, Temple);
                    ArrayUtils.ArrayAppend(ref loopSgStage5._sceneEntries, Temple);
                    temple.destinationsGroup = sgStage1;
                    temple.destinationsGroup = loopSgStage1;
                    break;
                default:
                    break;
            }

            switch (habitatStage)
            {
                case 1:
                    habitat.stageOrder = 1;
                    habitatnight.stageOrder = 1;
                    ArrayUtils.ArrayAppend(ref sgStage1._sceneEntries, Habitat);
                    ArrayUtils.ArrayAppend(ref loopSgStage1._sceneEntries, HabitatNight);
                    if (nightAsAlt)
                    {
                        ArrayUtils.ArrayAppend(ref sgStage1._sceneEntries, HabitatNight);
                        ArrayUtils.ArrayAppend(ref loopSgStage1._sceneEntries, Habitat);
                    }
                    habitat.destinationsGroup = sgStage2;
                    habitatnight.destinationsGroup = loopSgStage2;
                    break;
                case 2:
                    habitat.stageOrder = 2;
                    habitatnight.stageOrder = 2;
                    ArrayUtils.ArrayAppend(ref sgStage2._sceneEntries, Habitat);
                    ArrayUtils.ArrayAppend(ref loopSgStage2._sceneEntries, HabitatNight);
                    if (nightAsAlt)
                    {
                        ArrayUtils.ArrayAppend(ref sgStage2._sceneEntries, HabitatNight);
                        ArrayUtils.ArrayAppend(ref loopSgStage2._sceneEntries, Habitat);
                    }
                    habitat.destinationsGroup = sgStage3;
                    habitatnight.destinationsGroup = loopSgStage3;
                    break;
                case 3:
                    habitat.stageOrder = 3;
                    habitatnight.stageOrder = 3;
                    ArrayUtils.ArrayAppend(ref sgStage3._sceneEntries, Habitat);
                    ArrayUtils.ArrayAppend(ref loopSgStage3._sceneEntries, HabitatNight);
                    if (nightAsAlt)
                    {
                        ArrayUtils.ArrayAppend(ref sgStage3._sceneEntries, HabitatNight);
                        ArrayUtils.ArrayAppend(ref loopSgStage3._sceneEntries, Habitat);
                    }
                    habitat.destinationsGroup = sgStage4;
                    habitatnight.destinationsGroup = loopSgStage4;
                    break;
                case 4:
                    habitat.stageOrder = 4;
                    habitatnight.stageOrder = 4;
                    ArrayUtils.ArrayAppend(ref sgStage4._sceneEntries, Habitat);
                    ArrayUtils.ArrayAppend(ref loopSgStage4._sceneEntries, HabitatNight);
                    if (nightAsAlt)
                    {
                        ArrayUtils.ArrayAppend(ref sgStage4._sceneEntries, HabitatNight);
                        ArrayUtils.ArrayAppend(ref loopSgStage4._sceneEntries, Habitat);
                    }
                    habitat.destinationsGroup = sgStage5;
                    habitatnight.destinationsGroup = loopSgStage5;
                    break;
                case 5:
                    habitat.stageOrder = 5;
                    habitatnight.stageOrder = 5;
                    ArrayUtils.ArrayAppend(ref sgStage5._sceneEntries, Habitat);
                    ArrayUtils.ArrayAppend(ref loopSgStage5._sceneEntries, HabitatNight);
                    if (nightAsAlt)
                    {
                        ArrayUtils.ArrayAppend(ref sgStage5._sceneEntries, HabitatNight);
                        ArrayUtils.ArrayAppend(ref loopSgStage5._sceneEntries, Habitat);
                    }
                    habitat.destinationsGroup = sgStage1;
                    habitatnight.destinationsGroup = loopSgStage1;
                    break;
                default:
                    break;
            }
            switch (lakesStage)
            {
                case 1:
                    if (nightAsAlt)
                    {
                        ArrayUtils.ArrayAppend(ref sgStage1._sceneEntries, LakesNight);
                        ArrayUtils.ArrayAppend(ref loopSgStage1._sceneEntries, Lakes);
                    }
                    break;
                case 2:
                    lakes.stageOrder = 2;
                    lakesnight.stageOrder = 2;
                    ArrayUtils.ArrayAppend(ref sgStage2._sceneEntries, Lakes);
                    ArrayUtils.ArrayAppend(ref loopSgStage2._sceneEntries, LakesNight);
                    if (nightAsAlt)
                    {
                        ArrayUtils.ArrayAppend(ref sgStage2._sceneEntries, LakesNight);
                        ArrayUtils.ArrayAppend(ref loopSgStage2._sceneEntries, Lakes);
                    }
                    lakes.destinationsGroup = sgStage3;
                    lakesnight.destinationsGroup = loopSgStage3;
                    break;
                case 3:
                    lakes.stageOrder = 3;
                    lakesnight.stageOrder = 3;
                    ArrayUtils.ArrayAppend(ref sgStage3._sceneEntries, Lakes);
                    ArrayUtils.ArrayAppend(ref loopSgStage3._sceneEntries, LakesNight);
                    if (nightAsAlt)
                    {
                        ArrayUtils.ArrayAppend(ref sgStage3._sceneEntries, LakesNight);
                        ArrayUtils.ArrayAppend(ref loopSgStage3._sceneEntries, Lakes);
                    }
                    lakes.destinationsGroup = sgStage4;
                    lakesnight.destinationsGroup = loopSgStage4;
                    break;
                case 4:
                    lakes.stageOrder = 4;
                    lakesnight.stageOrder = 4;
                    ArrayUtils.ArrayAppend(ref sgStage4._sceneEntries, Lakes);
                    ArrayUtils.ArrayAppend(ref loopSgStage4._sceneEntries, LakesNight);
                    if (nightAsAlt)
                    {
                        ArrayUtils.ArrayAppend(ref sgStage4._sceneEntries, LakesNight);
                        ArrayUtils.ArrayAppend(ref loopSgStage4._sceneEntries, Lakes);
                    }
                    lakes.destinationsGroup = sgStage5;
                    lakesnight.destinationsGroup = loopSgStage5;
                    break;
                case 5:
                    lakes.stageOrder = 5;
                    lakesnight.stageOrder = 5;
                    ArrayUtils.ArrayAppend(ref sgStage5._sceneEntries, Lakes);
                    ArrayUtils.ArrayAppend(ref loopSgStage5._sceneEntries, LakesNight);
                    if (nightAsAlt)
                    {
                        ArrayUtils.ArrayAppend(ref sgStage5._sceneEntries, LakesNight);
                        ArrayUtils.ArrayAppend(ref loopSgStage5._sceneEntries, Lakes);
                    }
                    lakes.destinationsGroup = sgStage1;
                    lakesnight.destinationsGroup = loopSgStage1;
                    break;
                default:
                    break;
            }


        }
        


    }
}



