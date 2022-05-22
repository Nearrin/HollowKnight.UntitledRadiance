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
                if (fsm.AccessIntVariable("Type").Value == 3)
                {
                    fsm.SetState("RG3");
                }
            }, 1);
            fsm.InsertCustomAction("RG3", () =>
            {
                if (fsm.AccessIntVariable("Type").Value == 3)
                {
                    fsm.SetState("Spawn R");
                }
            }, 7);
            fsm.InsertCustomAction("Spawn L", () =>
            {
                if (fsm.AccessIntVariable("Type").Value == 3)
                {
                    fsm.SetState("LG3");
                }
            }, 1);
            fsm.InsertCustomAction("LG3", () =>
            {
                if (fsm.AccessIntVariable("Type").Value == 3)
                {
                    fsm.SetState("Spawn L");
                }
            }, 7);
        }
    }
}