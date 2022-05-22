namespace UntitledRadiance;
public partial class AttackChoices : Module
{
    public AttackChoices(UntitledRadiance untitledRadiance) : base(untitledRadiance)
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
        if (IsAbsoluteRadiance(fsm.gameObject) && fsm.FsmName == "Attack Choices")
        {
            var eventTarget = (fsm.GetState("Nail L Sweep").Actions[0] as SendEventByName).eventTarget;
            (fsm.GetState("Nail L Sweep").Actions[3] as Wait).time.Value = 5.6f;
            fsm.InsertAction("Nail L Sweep", fsm.CreateSendEventByName(eventTarget, "COMB R", 4.1f), 3);
            fsm.InsertAction("Nail L Sweep", fsm.CreateSendEventByName(eventTarget, "COMB R", 2.9f), 2);
            fsm.InsertAction("Nail L Sweep", fsm.CreateSendEventByName(eventTarget, "COMB R", 1.7f), 1);
            (fsm.GetState("Nail R Sweep").Actions[3] as Wait).time.Value = 5.6f;
            fsm.InsertAction("Nail R Sweep", fsm.CreateSendEventByName(eventTarget, "COMB L", 4.1f), 3);
            fsm.InsertAction("Nail R Sweep", fsm.CreateSendEventByName(eventTarget, "COMB L", 2.9f), 2);
            fsm.InsertAction("Nail R Sweep", fsm.CreateSendEventByName(eventTarget, "COMB L", 1.7f), 1);

            (fsm.GetState("Nail Top Sweep").Actions[1] as SendEventByName).delay = 0.6f;
            (fsm.GetState("Nail Top Sweep").Actions[2] as SendEventByName).delay = 1.2f;
            (fsm.GetState("Nail Top Sweep").Actions[3] as SendEventByName).delay = 1.8f;
            (fsm.GetState("Nail Top Sweep").Actions[4] as Wait).time.Value = 3.55f;

            (fsm.GetState("A1 Choice").Actions[1] as SendRandomEventV3).weights[3] = 2.5f;
            fsm.InsertCustomAction("A1 Choice", () =>
            {
                var phase = fsm.gameObject.LocateMyFSM("Phase Control").AccessStringVariable("phase").Value;
                if (phase == "1.3")
                {
                    (fsm.GetState("Nail L Sweep").Actions[0] as SendEventByName).delay.Value = 1;
                    (fsm.GetState("Nail L Sweep").Actions[1] as SendEventByName).delay.Value = 3.4f;
                    (fsm.GetState("Nail L Sweep").Actions[2] as SendEventByName).delay.Value = 3.4f;
                    (fsm.GetState("Nail L Sweep").Actions[3] as SendEventByName).delay.Value = 5.8f;
                    (fsm.GetState("Nail L Sweep").Actions[4] as SendEventByName).delay.Value = 5.8f;
                    (fsm.GetState("Nail L Sweep").Actions[5] as SendEventByName).delay.Value = 8.2f;
                    (fsm.GetState("Nail L Sweep").Actions[6] as Wait).time.Value = 11.2f;
                    (fsm.GetState("Nail R Sweep").Actions[0] as SendEventByName).delay.Value = 1;
                    (fsm.GetState("Nail R Sweep").Actions[1] as SendEventByName).delay.Value = 3.4f;
                    (fsm.GetState("Nail R Sweep").Actions[2] as SendEventByName).delay.Value = 3.4f;
                    (fsm.GetState("Nail R Sweep").Actions[3] as SendEventByName).delay.Value = 5.8f;
                    (fsm.GetState("Nail R Sweep").Actions[4] as SendEventByName).delay.Value = 5.8f;
                    (fsm.GetState("Nail R Sweep").Actions[5] as SendEventByName).delay.Value = 8.2f;
                    (fsm.GetState("Nail R Sweep").Actions[6] as Wait).time.Value = 11.2f;
                }
                else if (phase == "1.4")
                {
                    fsm.SendEvent("EYE BEAMS");
                }
            }, 0);

            fsm.AddTransition("Nail Top Sweep", "END", "A1 Choice");
            fsm.InsertCustomAction("Nail Top Sweep", () =>
            {
                var phase = fsm.gameObject.LocateMyFSM("Phase Control").AccessStringVariable("phase").Value;
                if (phase == "1.3")
                {
                    fsm.SendEvent("END");
                }
            }, 0);
        }
    }
}