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
        }
    }
}