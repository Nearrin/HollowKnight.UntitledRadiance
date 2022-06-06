namespace UntitledRadiance;
public partial class RadiantNail : Module
{
    public RadiantNail(UntitledRadiance untitledRadiance) : base(untitledRadiance)
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
        if (gameObject.scene.name == "DontDestroyOnLoad" && gameObject.name.StartsWith("Radiant Nail") && !gameObject.name.StartsWith("Radiant Nail Comb") && fsm.FsmName == "Control")
        {
            (fsm.GetState("Fire CCW").Actions[2] as FloatAdd).add.Value = 90;
            (fsm.GetState("Fire CW").Actions[2] as FloatAdd).add.Value = -90;
            fsm.InsertCustomAction("Set Collider", () =>
            {
                var absoluteRadiance = untitledRadiance_.absoluteRadiance;
                var phase = absoluteRadiance.LocateMyFSM("Phase Control").AccessStringVariable("phase").Value;
                if (phase == "2.1")
                {
                    var y = HeroController.instance.transform.position.y + random.Next(16, 32);
                    var position = fsm.gameObject.transform.position;
                    position.y = y;
                    fsm.gameObject.transform.position = position;
                }
            }, 0);
        }
    }
}