namespace UntitledRadiance;
public partial class BeamSweeper : Module
{
    public BeamSweeper(UntitledRadiance untitledRadiance) : base(untitledRadiance)
    {
    }
    public override List<(string, string)> GetPreloadNames()
    {
        return new List<(string, string)>
        {
            ("GG_Radiance", "Boss Control")
        };
    }
    public override void LoadPrefabs(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects)
    {
        var bossControl = preloadedObjects["GG_Radiance"]["Boss Control"];
        var beamSweeper = bossControl.transform.Find("Beam Sweeper").gameObject;
        prefabs["beamSweeper"] = beamSweeper;
    }
    public override void UpdateHitInstance(HealthManager healthManager, HitInstance hitInstance)
    {
    }
    public override void UpdateFSM(PlayMakerFSM fsm)
    {
        var gameObject = fsm.gameObject;
        if (gameObject.scene.name == "GG_Radiance" && gameObject.name == "Beam Sweeper" && fsm.FsmName == "Control")
        {
            UnityEngine.Object.Instantiate((prefabs["beamSweeper"] as GameObject), GameObject.Find("Boss Control").transform);
            foreach (var direction in new List<string> { "L", "R", "L2", "R2" })
            {
                var name = direction;
                if (name == "L2")
                {
                    name = "L 2";
                }
                else if (name == "R2")
                {
                    name = "R 2";
                }
                var antiName = name;
                if (antiName.StartsWith("L"))
                {
                    antiName = antiName.Replace("L", "R");
                }
                else
                {
                    antiName = antiName.Replace("R", "L");
                }
                fsm.AddState("Beam Sweep " + direction + " Delay");
                fsm.ChangeTransition("Idle", "BEAM SWEEP " + direction, "Beam Sweep " + direction + " Delay");
                fsm.AddAction("Beam Sweep " + direction + " Delay", fsm.CreateWait(1.5f, fsm.GetFSMEvent("FINISHED")));
                fsm.AddTransition("Beam Sweep " + direction + " Delay", "FINISHED", "Beam Sweep " + antiName);
                if (direction == "L")
                {
                    (fsm.GetState("Beam Sweep " + name).Actions[5] as iTweenMoveBy).time = 3;
                }
                else
                {
                    (fsm.GetState("Beam Sweep " + name).Actions[6] as iTweenMoveBy).time = 3;
                }
            }
        }
        else if (gameObject.scene.name == "GG_Radiance" && gameObject.name == "Beam Sweeper(Clone)" && fsm.FsmName == "Control")
        {
            foreach (var direction in new List<string> { "L", "R", "L2", "R2" })
            {
                var name = direction;
                if (name == "L2")
                {
                    name = "L 2";
                }
                else if (name == "R2")
                {
                    name = "R 2";
                }
                if (direction == "L")
                {
                    (fsm.GetState("Beam Sweep " + name).Actions[5] as iTweenMoveBy).time = 3;
                }
                else
                {
                    (fsm.GetState("Beam Sweep " + name).Actions[6] as iTweenMoveBy).time = 3;
                }
            }
        }
    }
}