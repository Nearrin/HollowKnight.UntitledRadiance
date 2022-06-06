namespace UntitledRadiance;
public partial class Control : Module
{
    public Control(UntitledRadiance untitledRadiance) : base(untitledRadiance)
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
        if (IsAbsoluteRadiance(fsm.gameObject) && fsm.FsmName == "Control")
        {
            untitledRadiance_.absoluteRadiance = fsm.gameObject;
            fsm.InsertCustomAction("Tele First?", () =>
            {
                var phase = fsm.gameObject.LocateMyFSM("Phase Control").AccessStringVariable("phase").Value;
                if (phase == "1.4")
                {
                    fsm.SendEvent("SHIFT");
                }
            }, 0);
            fsm.InsertCustomAction("A1 Tele 2", () =>
            {
                var teleport = fsm.gameObject.LocateMyFSM("Teleport");
                if (teleport.ActiveStateName != "Idle")
                {
                    teleport.SetState("Antic");
                }
            }, 1);
            fsm.RemoveAction("Plat Setup", 2);
        }
    }
}