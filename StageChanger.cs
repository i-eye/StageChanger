using BepInEx;
using BepInEx.Configuration;
using RiskOfOptions;
using RiskOfOptions.OptionConfigs;
using RiskOfOptions.Options;
namespace IEye.StageRearrange
{
    [BepInPlugin(PLUGINGUID, PluginName, PluginVersion)]
    [BepInDependency("com.rune580.riskofoptions")]
    public class StageChangerPlugin : BaseUnityPlugin
    {

        public ConfigEntry<int> villageStage;
        public ConfigEntry<int> templeStage;
        public ConfigEntry<int> habitatStage;
        public ConfigEntry<int> lakesStage;
        public ConfigEntry<int> hatcheryStage;
        //public ConfigEntry<bool> changePathOfColossus;
        public ConfigEntry<bool> nightStageAsAlt;

        public const string PLUGINGUID = "IEye.StageRearrange";
        public const string PluginName = "StageRearrange";
        public const string PluginVersion = "0.4.1";
        public static StageChangerPlugin instance;
        public static ChangeStages changer;

        public void Awake()
        {
            instance = this;
            CreateConfigs();

            Log.Init(Logger);

            //Log.Message(villageStage.Value);
            changer = new ChangeStages();
            changer.Init();
            changer.EditStages();
            //if (changePathOfColossus.Value) THIS FUCKING FUCKER DIE DIE DIE DIE DIE
            //{
                new ChangePathOfCollosus().Init();
            //}
            On.RoR2.Run.Start += Run_Start;
            

        }

        private void Run_Start(On.RoR2.Run.orig_Start orig, RoR2.Run self)
        {
            changer.EditStages();
            orig.Invoke(self);
        }


        private void CreateConfigs()
        {
            villageStage = base.Config.Bind("General",
                            "Shattered Abodes Stage",
                            2,
                            "Which stage Shattered Abodes/Disturbed Impact should be(1-5)");
            templeStage = base.Config.Bind("General",
                "Reformed Altar Stage",
                2,
                "Which regular stage Reformed Altar should be(1-5 for addition)(0 for no addition)");
            habitatStage = base.Config.Bind("General",
                "Treeborn Colony Stage",
                3,
                "Which regular stage Treeborn Colony/Goldan Dieback should be(1-5 for addition)(0 for no addition)");
            lakesStage = base.Config.Bind("General",
                "Verdant Falls Stage",
                1,
                "Which stage Verdant/Vicious Falls should be(1-5)");
            //hatcheryStage = base.Config.Bind("General",
            //    "Helminth Hatchery Stager",
            //    5,
            //    "Why would you change this");
            /*changePathOfColossus = base.Config.Bind("General",
                "Change Path of Colossus",
                true,
                "Change Path of Colossus to start from Reformed Altar regards of point in stages");*/
            nightStageAsAlt = base.Config.Bind("General",
                "Night stages as alt",
                false,
                "Night stages become alternate stages instead of looped stages");
            ModSettingsManager.AddOption(new IntSliderOption(villageStage, new IntSliderConfig() { max = 5, min = 0 }));
            ModSettingsManager.AddOption(new IntSliderOption(templeStage, new IntSliderConfig() { max = 5, min = 0 }));
            ModSettingsManager.AddOption(new IntSliderOption(habitatStage, new IntSliderConfig() { max = 5, min = 0 }));
            ModSettingsManager.AddOption(new IntSliderOption(lakesStage, new IntSliderConfig() { max = 5, min = 0 }));
            //ModSettingsManager.AddOption(new CheckBoxOption(changePathOfColossus, true));
            ModSettingsManager.AddOption(new CheckBoxOption(nightStageAsAlt));
        }
    }
}



