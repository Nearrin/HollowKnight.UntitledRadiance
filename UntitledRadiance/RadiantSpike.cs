namespace UntitledRadiance;
public partial class RadiantSpike : Module
{
    public RadiantSpike(UntitledRadiance untitledRadiance) : base(untitledRadiance)
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
        if (gameObject.scene.name == "GG_Radiance" && gameObject.name.StartsWith("Radiant Spike") && fsm.FsmName == "Hero Saver")
        {
            fsm.InsertCustomAction("Send", () =>
            {
                HeroController.instance.damageMode = GlobalEnums.DamageMode.FULL_DAMAGE;
                fsm.SendEvent("FINISHED");
            }, 0);
        }
    }
}