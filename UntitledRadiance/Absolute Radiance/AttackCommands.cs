namespace UntitledRadiance;
public partial class AttackCommands : Module
{
    public AttackCommands(UntitledRadiance untitledRadiance) : base(untitledRadiance)
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
        if (IsAbsoluteRadiance(fsm.gameObject) && fsm.FsmName == "Attack Commands")
        {
            (fsm.GetState("EB 1").Actions[8] as SendEventByName).delay = 0.3f;
            (fsm.GetState("EB 1").Actions[9] as SendEventByName).delay = 0.55f;
            (fsm.GetState("EB 1").Actions[10] as Wait).time.Value = 0.6f;
            (fsm.GetState("EB 2").Actions[8] as SendEventByName).delay = 0.3f;
            (fsm.GetState("EB 2").Actions[9] as SendEventByName).delay = 0.55f;
            (fsm.GetState("EB 2").Actions[10] as Wait).time.Value = 0.6f;
            (fsm.GetState("EB 3").Actions[8] as SendEventByName).delay = 0.3f;
            (fsm.GetState("EB 3").Actions[9] as SendEventByName).delay = 0.55f;
            (fsm.GetState("EB 3").Actions[10] as Wait).time.Value = 0.6f;
        }
    }
}