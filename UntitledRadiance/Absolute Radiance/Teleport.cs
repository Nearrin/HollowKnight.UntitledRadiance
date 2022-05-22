namespace UntitledRadiance;
public partial class Teleport : Module
{
    public Teleport(UntitledRadiance untitledRadiance) : base(untitledRadiance)
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
        if (IsAbsoluteRadiance(fsm.gameObject) && fsm.FsmName == "Teleport")
        {
            fsm.InsertCustomAction("Arrive", () =>
            {
                var phase = fsm.gameObject.LocateMyFSM("Phase Control").AccessStringVariable("phase").Value;
                if (phase == "1.4")
                {
                    fsm.AccessVector3Variable("Destination").Value = new Vector3(60.63f, 29, 0.006f);
                }
            }, 0);
        }
    }
}