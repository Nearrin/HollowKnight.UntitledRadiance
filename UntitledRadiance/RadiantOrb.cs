namespace UntitledRadiance;
public partial class RadiantOrb : Module
{
    public RadiantOrb(UntitledRadiance untitledRadiance) : base(untitledRadiance)
    {
    }
    public override List<(string, string)> GetPreloadNames()
    {
        return new List<(string, string)>
        {
            ("GG_Hollow_Knight", "Battle Scene"),
        };
    }
    public override void LoadPrefabs(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects)
    {
        var battleScene = preloadedObjects["GG_Hollow_Knight"]["Battle Scene"].gameObject;
        var focusBlasts = battleScene.transform.Find("Focus Blasts").gameObject;
        var hKPrimeBlast = focusBlasts.transform.Find("HK Prime Blast").gameObject;
        prefabs["hKPrimeBlast"] = hKPrimeBlast;
    }
    public override void UpdateHitInstance(HealthManager healthManager, HitInstance hitInstance)
    {
    }
    public override void UpdateFSM(PlayMakerFSM fsm)
    {
        var gameObject = fsm.gameObject;
        if (gameObject.scene.name == "DontDestroyOnLoad" && gameObject.name == "Radiant Orb(Clone)" && fsm.FsmName == "Orb Control")
        {
            fsm.AddState("Blast Begin");
            fsm.AddState("Blast End");
            fsm.ChangeTransition("Impact", "FINISHED", "Blast Begin");
            fsm.ChangeTransition("Stop Particles", "FINISHED", "Blast Begin");
            fsm.AddAction("Blast Begin", fsm.CreateWait(8, fsm.GetFSMEvent("FINISHED")));
            var blast = () =>
            {
                var hKPrimeBlast = UnityEngine.Object.Instantiate(prefabs["hKPrimeBlast"] as GameObject);
                hKPrimeBlast.transform.position = gameObject.transform.position;
                hKPrimeBlast.LocateMyFSM("Control").SetState("Blast");
                fsm.AccessGameObjectVariable("hKPrimeBlast").Value = hKPrimeBlast;
            };
            fsm.AddCustomAction("Blast Begin", blast);
            fsm.AddTransition("Blast Begin", "FINISHED", "Blast End");
            fsm.AddCustomAction("Blast End", () =>
            {
                var hKPrimeBlast = fsm.AccessGameObjectVariable("hKPrimeBlast").Value;
                UnityEngine.Object.Destroy(hKPrimeBlast);
                fsm.SetState("Recycle");
            });
        }
    }
}