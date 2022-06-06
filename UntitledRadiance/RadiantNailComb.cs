namespace UntitledRadiance;
public partial class RadiantNailComb : Module
{
    public RadiantNailComb(UntitledRadiance untitledRadiance) : base(untitledRadiance)
    {
    }
    public override List<(string, string)> GetPreloadNames()
    {
        return new List<(string, string)>
        {
        };
    }
    public override void LoadPrefabs(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects)
    {
    }
    public override void UpdateHitInstance(HealthManager healthManager, HitInstance hitInstance)
    {
    }
    public override void UpdateFSM(PlayMakerFSM fsm)
    {
        var gameObject = fsm.gameObject;
        if ((gameObject.scene.name == "DontDestroyOnLoad" || gameObject.scene.name == "GG_Radiance") && gameObject.name.StartsWith("Radiant Nail Comb") && fsm.FsmName == "Control")
        {
            (fsm.GetAction("Top", 0) as RandomFloat).min = 58.7f;
            (fsm.GetAction("Top", 0) as RandomFloat).max = 62.7f;
            fsm.InsertCustomAction("Spawn R", () =>
            {
                var absoluteRadiance = untitledRadiance_.absoluteRadiance;
                var phase = absoluteRadiance.LocateMyFSM("Phase Control").AccessStringVariable("phase").Value;
                if (fsm.AccessIntVariable("Type").Value == 3 && phase!="2.1")
                {
                    fsm.SetState("RG3");
                }
            }, 1);
            fsm.InsertCustomAction("RG3", () =>
            {
                var absoluteRadiance = untitledRadiance_.absoluteRadiance;
                var phase = absoluteRadiance.LocateMyFSM("Phase Control").AccessStringVariable("phase").Value;
                if (fsm.AccessIntVariable("Type").Value == 3 && phase != "2.1")
                {
                    fsm.SetState("Spawn R");
                }
            }, 7);
            fsm.InsertCustomAction("Spawn L", () =>
            {
                var absoluteRadiance = untitledRadiance_.absoluteRadiance;
                var phase = absoluteRadiance.LocateMyFSM("Phase Control").AccessStringVariable("phase").Value;
                if (fsm.AccessIntVariable("Type").Value == 3 && phase != "2.1")
                {
                    fsm.SetState("LG3");
                }
            }, 1);
            fsm.InsertCustomAction("LG3", () =>
            {
                var absoluteRadiance = untitledRadiance_.absoluteRadiance;
                var phase = absoluteRadiance.LocateMyFSM("Phase Control").AccessStringVariable("phase").Value;
                if (fsm.AccessIntVariable("Type").Value == 3 && phase != "2.1")
                {
                    fsm.SetState("Spawn L");
                }
            }, 7);
            fsm.InsertCustomAction("Tween", () =>
            {
                var absoluteRadiance = untitledRadiance_.absoluteRadiance;
                var phase = absoluteRadiance.LocateMyFSM("Phase Control").AccessStringVariable("phase").Value;
                if (phase == "1.3")
                {
                    fsm.AccessFloatVariable("Nail Speed").Value = 13f;
                }
            }, 0);
        }
    }
}