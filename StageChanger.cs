using BepInEx;
using BepInEx.Configuration;

namespace IEye.StageRearrange
{
    [BepInPlugin(PLUGINGUID, PluginName, PluginVersion)]
    public class StageChangerPlugin : BaseUnityPlugin
    {

        private ConfigEntry<int> villageStage;
        private ConfigEntry<int> templeStage;
        private ConfigEntry<int> habitatStage;
        private ConfigEntry<int> lakesStage;
        private ConfigEntry<bool> changePathOfColossus;
        private ConfigEntry<bool> nightStageAsAlt;

        public const string PLUGINGUID = "IEye.StageRearrange";
        public const string PluginName = "StageRearrange";
        public const string PluginVersion = "0.3.2";

        public void Awake()
        {
            villageStage = Config.Bind("General",
                "Shattered Abodes Stage",
                2,
                "Which stage Shattered Abodes/Disturbed Impact should be(1-5)");
            templeStage = Config.Bind("General",
                "Reformed Altar Stage",
                2,
                "Which regular stage Reformed Altar should be(1-5 for addition)(0 for no addition)");
            habitatStage = Config.Bind("General",
                "Treeborn Colony Stage",
                3,
                "Which regular stage Treeborn Colony/Goldan Dieback should be(1-5 for addition)(0 for no addition)");
            lakesStage = Config.Bind("General",
                "Verdant Falls Stage",
                1,
                "Which stage Verdant/Vicious Falls should be(1-5)");
            changePathOfColossus = Config.Bind("General",
                "Change Path of Colossus",
                true,
                "Change Path of Colossus to start from Reformed Altar regards of point in stages");
            nightStageAsAlt = Config.Bind("General",
                "Night stages as alt",
                false,
                "Night stages become alternate stages instead of looped stages");


            Log.Init(Logger);

            //Log.Message(villageStage.Value);

            new ChangeStages().EditStages(villageStage.Value,templeStage.Value,habitatStage.Value, lakesStage.Value, nightStageAsAlt.Value);
            if (changePathOfColossus.Value)
            {
                new ChangePathOfCollosus().Init();
            }
            
        }
    }
}



