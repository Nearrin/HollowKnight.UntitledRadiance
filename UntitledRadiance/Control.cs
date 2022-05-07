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
        if (IsAbsoluteRadiance(fsm.gameObject) && fsm.FsmName == "Attack Commands")
        {
            Log("Upgrading Eye Beams 1.");
            (fsm.GetState("EB 1").Actions[9] as SendEventByName).delay.Value += 3;
            (fsm.GetState("EB 1").Actions[10] as Wait).time.Value += 3;
            fsm.ChangeTransition("EB 1", "FINISHED", "EB Glow End");
            (fsm.GetState("EB 7").Actions[8] as SendEventByName).delay.Value += 3;
            (fsm.GetState("EB 7").Actions[9] as Wait).time.Value += 3;
            fsm.ChangeTransition("EB 7", "FINISHED", "EB Glow End");
            var burst1 = fsm.gameObject.transform.Find("Eye Beam Glow").transform.Find("Burst 1").gameObject;
            burst1.AddComponent<EyeBeamsRotator>();
            Log("Upgraded Eye Beams 1.");
        }
        if (fsm.gameObject.scene.name == "GG_Radiance" && fsm.gameObject.name.StartsWith("Radiant Beam") && fsm.FsmName == "Control")
        {
            Log("Upgrading Eye Beams 1.");
            fsm.AddCustomAction("Fire", () =>
            {
                var eyeBeamsRotator = fsm.gameObject.transform.parent.gameObject.GetComponent<EyeBeamsRotator>();
                if (eyeBeamsRotator != null)
                {
                    eyeBeamsRotator.rotating = true;
                }
            });
            fsm.AddCustomAction("End", () =>
            {
                var eyeBeamsRotator = fsm.gameObject.transform.parent.gameObject.GetComponent<EyeBeamsRotator>();
                if (eyeBeamsRotator != null)
                {
                    eyeBeamsRotator.rotating = false;
                }
            });
            Log("Upgraded Eye Beams 2.");
        }
    }
}