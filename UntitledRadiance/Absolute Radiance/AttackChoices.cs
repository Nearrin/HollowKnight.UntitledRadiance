﻿namespace UntitledRadiance;
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

            (fsm.GetState("Nail Top Sweep").Actions[1] as SendEventByName).delay = 0.4f;
            (fsm.GetState("Nail Top Sweep").Actions[2] as SendEventByName).delay = 0.8f;
            (fsm.GetState("Nail Top Sweep").Actions[3] as SendEventByName).delay = 1.2f;
            (fsm.GetState("Nail Top Sweep").Actions[4] as Wait).time.Value = 2.95f;
        }
    }
}