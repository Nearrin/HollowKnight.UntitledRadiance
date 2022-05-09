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
            ("GG_Radiance", "Boss Control"),
        };
    }
    public override void LoadPrefabs(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects)
    {
        var bossControl = preloadedObjects["GG_Radiance"]["Boss Control"];
        var absoluteRadiance = bossControl.transform.Find("Absolute Radiance").gameObject;
        var eyeBeamGlow = absoluteRadiance.transform.Find("Eye Beam Glow").gameObject;
        prefabs["trackingEyeBeamGlow"] = eyeBeamGlow;
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

            fsm.InsertCustomAction("EB 1", () =>
            {
                if (UnityEngine.Random.Range(0, 2.5f) < 1.5f)
                {
                    fsm.SendEvent("END");
                }
            }, 4);
            fsm.AddState("Tracking Beam");
            fsm.AddTransition("EB 1", "END", "Tracking Beam");
            var trackingEyeBeamGlow = UnityEngine.Object.Instantiate((prefabs["trackingEyeBeamGlow"] as GameObject), fsm.gameObject.transform);
            trackingEyeBeamGlow.SetActive(true);
            var trackingBurst = trackingEyeBeamGlow.transform.Find("Burst 1").gameObject;
            GameObject trackingBeam = null;
            foreach (var fsm_ in trackingBurst.GetComponentsInChildren<PlayMakerFSM>())
            {
                if (fsm_.gameObject.name != "Radiant Beam")
                {
                    UnityEngine.Object.Destroy(fsm_.gameObject);
                }
                else
                {
                    trackingBeam = fsm_.gameObject;
                }
            }
            UnityEngine.Object.Destroy(trackingEyeBeamGlow.transform.Find("Burst 2").gameObject);
            UnityEngine.Object.Destroy(trackingEyeBeamGlow.transform.Find("Burst 3").gameObject);
            UnityEngine.Object.Destroy(trackingEyeBeamGlow.transform.Find("Ascend Beam").gameObject);
            UnityEngine.Object.Destroy(trackingEyeBeamGlow.transform.Find("Sprite").gameObject);
            trackingBurst.transform.Rotate(0, 0, -90);
            trackingBurst.AddComponent<TrackingBurstRotator>();
            var fsmOwnerDefault = new FsmOwnerDefault
            {
                OwnerOption = OwnerDefaultOption.SpecifyGameObject,
                GameObject = trackingBeam,
            };
            var fSMEventTarget = new FsmEventTarget
            {
                target = FsmEventTarget.EventTarget.GameObject,
                gameObject = fsmOwnerDefault,
            };
            fsm.AddAction("Tracking Beam", fsm.CreateSendEventByName(fSMEventTarget, "ANTIC", 0));
            fsm.AddAction("Tracking Beam", fsm.CreateSendEventByName(fSMEventTarget, "FIRE", 0.2f));
            fsm.AddAction("Tracking Beam", fsm.CreateSendEventByName(fSMEventTarget, "END", 1.4f));
            fsm.AddAction("Tracking Beam", fsm.CreateWait(2.5f, fsm.GetFSMEvent("FINISHED")));
            fsm.AddTransition("Tracking Beam", "FINISHED", "EB Glow End");

            fsm.InsertCustomAction("Dir", () =>
            {
                fsm.AccessIntVariable("nailFanIndex").Value = 0;
            }, 0);
            var cWFireSendEventByName = fsm.GetState("CW Fire").Actions[0] as SendEventByName;
            fsm.InsertCustomAction("CW Fire", () =>
            {
                if (fsm.AccessIntVariable("nailFanIndex").Value % 2 == 0)
                {
                    cWFireSendEventByName.sendEvent = "FAN ATTACK CW";
                }
                else
                {
                    cWFireSendEventByName.sendEvent = "FAN ATTACK CCW";
                }
            }, 0);
            fsm.InsertCustomAction("CW Double", () =>
            {
                fsm.AccessIntVariable("nailFanIndex").Value += 1;
                if (fsm.AccessIntVariable("nailFanIndex").Value != 4)
                {
                    fsm.SetState("CW Restart");
                }
                else
                {
                    fsm.SetState("End");
                }
            }, 0);
            var cCWFireSendEventByName = fsm.GetState("CCW Fire").Actions[0] as SendEventByName;
            fsm.InsertCustomAction("CCW Fire", () =>
            {
                if (fsm.AccessIntVariable("nailFanIndex").Value % 2 == 0)
                {
                    cCWFireSendEventByName.sendEvent = "FAN ATTACK CCW";
                }
                else
                {
                    cCWFireSendEventByName.sendEvent = "FAN ATTACK CW";
                }
            }, 0);
            fsm.InsertCustomAction("CCW Double", () =>
            {
                fsm.AccessIntVariable("nailFanIndex").Value += 1;
                if (fsm.AccessIntVariable("nailFanIndex").Value != 4)
                {
                    fsm.SetState("CCW Restart");
                }
                else
                {
                    fsm.SetState("End");
                }
            }, 0);

            (fsm.GetState("Orb Antic").Actions[2] as RandomInt).min.Value = 6;
            (fsm.GetState("Orb Antic").Actions[2] as RandomInt).max.Value = 10;
            (fsm.GetState("Orb Summon").Actions[2] as Wait).time.Value = 0.375f;
        }
    }
}