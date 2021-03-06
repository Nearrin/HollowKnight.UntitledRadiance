namespace UntitledRadiance;
public partial class PhaseControl : Module
{
    public PhaseControl(UntitledRadiance untitledRadiance) : base(untitledRadiance)
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
        if (IsAbsoluteRadiance(fsm.gameObject) && fsm.FsmName == "Phase Control")
        {
            fsm.AddCustomAction("Init", () =>
            {
                fsm.gameObject.GetComponent<HealthManager>().hp = 1600;
                fsm.gameObject.RefreshHPBar();
                fsm.AccessStringVariable("phase").Value = "1.1";
            });
            fsm.AddAction("Init", fsm.CreateGeneralAction(() =>
            {
                var hp = fsm.gameObject.GetComponent<HealthManager>().hp;
                var phase = fsm.AccessStringVariable("phase").Value;
                var spikeControl = fsm.gameObject.transform.parent.Find("Spike Control").gameObject;
                if (phase == "1.1")
                {
                    if (hp <= 1500)
                    {
                        fsm.AccessStringVariable("phase").Value = "1.2";
                        phase = fsm.AccessStringVariable("phase").Value;
                        Log("Switching phase to: " + phase.ToString());
                        spikeControl.LocateMyFSM("Control").SendEvent("SPIKE WAVES");
                        Log("Switched phase to: " + phase.ToString());
                    }
                }
                else if (phase == "1.2")
                {
                    if (hp <= 1400)
                    {
                        fsm.AccessStringVariable("phase").Value = "1.3";
                        phase = fsm.AccessStringVariable("phase").Value;
                        Log("Switching phase to: " + phase.ToString());
                        spikeControl.LocateMyFSM("Control").SendEvent("SPIKE WAVES FULL");
                        Log("Switched phase to: " + phase.ToString());
                    }
                }
                else if (phase == "1.3")
                {
                    if (hp <= 1300)
                    {
                        fsm.AccessStringVariable("phase").Value = "1.4";
                        phase = fsm.AccessStringVariable("phase").Value;
                        Log("Switching phase to: " + phase.ToString());
                        Log("Switched phase to: " + phase.ToString());
                    }
                }
                else if (phase == "1.4")
                {
                    if (hp <= 1200)
                    {
                        if(fsm.gameObject.LocateMyFSM("Attack Commands").ActiveStateName== "Rotating Beam")
                        {
                            fsm.AccessStringVariable("phase").Value = "2.1";
                            phase = fsm.AccessStringVariable("phase").Value;
                            Log("Switching phase to: " + phase.ToString());
                            fsm.gameObject.LocateMyFSM("Attack Commands").SendEvent("CW");
                            foreach (var myFSM in untitledRadiance_.attackCommands.myRotatingBurst.GetComponentsInChildren<PlayMakerFSM>())
                            {
                                myFSM.SendEvent("FIRE");
                                myFSM.SendEvent("END");
                            }
                            fsm.SetState("Stun 1");
                            Log("Switched phase to: " + phase.ToString());
                        }
                    }
                }
                else if (phase == "2.1")
                {
                }
                else
                {
                    Log("Unknown phase: " + phase.ToString());
                }
            }));
            fsm.RemoveTransition("Init", "FINISHED");
        }
    }
}