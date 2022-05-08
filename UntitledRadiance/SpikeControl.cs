namespace UntitledRadiance;
public partial class SpikeControl : Module
{
    public SpikeControl(UntitledRadiance untitledRadiance) : base(untitledRadiance)
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
        if (gameObject.scene.name == "GG_Radiance" && gameObject.name == "Spike Control" && fsm.FsmName == "Control")
        {
            fsm.AddState("Spike Waves Full");
            fsm.AddGlobalTransition("SPIKE WAVES FULL", "Spike Waves Full");
            fsm.AddCustomAction("Spike Waves Full", () =>
            {
                var beamSweeper = (fsm.GetState("Wave L").Actions[0] as SetFsmBool).gameObject.GameObject.Value;
                beamSweeper.LocateMyFSM("Control").AccessBoolVariable("Force Left").Value = false;
                beamSweeper.LocateMyFSM("Control").AccessBoolVariable("Force Right").Value = false;
                for (int i = 2; i <= 6; ++i)
                {
                    var spikes = (fsm.GetState("Wave L").Actions[i] as SendEventByName).eventTarget.gameObject.GameObject.Value;
                    foreach (var fsm_ in spikes.GetComponentsInChildren<PlayMakerFSM>())
                    {
                        fsm_.SendEvent("UP");
                    }
                }
            });
        }
    }
}