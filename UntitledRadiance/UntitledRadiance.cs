namespace UntitledRadiance;
[Serializable]
public class Settings
{
    public int status = 0;
}
public class UntitledRadiance : Mod, IGlobalSettings<Settings>, IMenuMod
{
    public static UntitledRadiance untitledRadiance;
    private Control control;
    public GameObject absoluteRadiance;
    private Teleport teleport;
    private AttackChoices attackChoices;
    public AttackCommands attackCommands;
    private PhaseControl phaseControl;
    private SpikeControl spkieControl;
    private RadiantSpike radiantSpike;
    private RadiantNail radiantNail;
    private RadiantNailComb radiantNailComb;
    private RadiantOrb radiantOrb;
    private BeamSweeper beamSweeper;
    public List<Module> modules = new();
    private Settings settings_ = new();
    public bool ToggleButtonInsideMenu => true;
    public UntitledRadiance() : base("UntitledRadiance")
    {
        untitledRadiance = this;
        control = new(this);
        teleport = new(this);
        attackChoices = new(this);
        attackCommands = new(this);
        phaseControl = new(this);
        spkieControl = new(this);
        radiantSpike = new(this);
        radiantNail = new(this);
        radiantNailComb = new(this);
        radiantOrb = new(this);
        beamSweeper = new(this);
    }
    public override string GetVersion() => "1.0.0.0";
    public override List<(string, string)> GetPreloadNames()
    {
        List<(string, string)> preloadNames = new();
        foreach (var module in modules)
        {
            foreach (var name in module.GetPreloadNames())
            {
                preloadNames.Add(name);
            }
        }
        return preloadNames;
    }
    public override void Initialize(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects)
    {
        ModHooks.HeroUpdateHook += HeroUpdateHook;
        ModHooks.LanguageGetHook += LanguageGetHook;
        On.EnemyDreamnailReaction.RecieveDreamImpact += RecieveDreamImpact;
        On.HealthManager.TakeDamage += HealthManagerTakeDamage;
        On.PlayMakerFSM.OnEnable += PlayMakerFSMOnEnable;
        if (preloadedObjects != null)
        {
            foreach (var module in modules)
            {
                module.LoadPrefabs(preloadedObjects);
            }
        }
    }
    private List<Module> GetActiveModules()
    {
        if (settings_.status == 0)
        {
            return modules;
        }
        else
        {
            return new List<Module>() { };
        }
    }
    private void HeroUpdateHook()
    {
        try
        {
            if (Input.GetKeyDown(KeyCode.F3))
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("GG_Radiance");
            }
        }
        catch (Exception exception)
        {
            LogError(exception.Message);
        }
    }
    private string LanguageGetHook(string key, string sheet, string text)
    {
        try
        {
            foreach (var module in GetActiveModules())
            {
                text = module.UpdateText(key, sheet, text);
            }
        }
        catch (Exception exception)
        {
            LogError(exception.Message);
        }
        return text;
    }
    private void RecieveDreamImpact(On.EnemyDreamnailReaction.orig_RecieveDreamImpact receiveDreamImpact, EnemyDreamnailReaction enemyDreamnailReaction)
    {
        try
        {
            foreach (var module in GetActiveModules())
            {
                if (module.UpdateDreamnailReaction(enemyDreamnailReaction))
                {
                    return;
                }
            }
            receiveDreamImpact(enemyDreamnailReaction);
        }
        catch (Exception exception)
        {
            LogError(exception.Message);
        }
    }
    private void HealthManagerTakeDamage(On.HealthManager.orig_TakeDamage takeDamage, HealthManager healthManager, HitInstance hitInstance)
    {
        try
        {
            foreach (var module in GetActiveModules())
            {
                module.UpdateHitInstance(healthManager, hitInstance);
            }
            takeDamage(healthManager, hitInstance);
        }
        catch (Exception exception)
        {
            LogError(exception.Message);
        }
    }
    private void PlayMakerFSMOnEnable(On.PlayMakerFSM.orig_OnEnable onEnable, PlayMakerFSM fsm)
    {
        try
        {
            foreach (var module in GetActiveModules())
            {
                module.UpdateFSM(fsm);
            }
            onEnable(fsm);
        }
        catch (Exception exception)
        {
            LogError(exception.Message);
        }
    }
    public void OnLoadGlobal(Settings settings) => settings_ = settings;
    public Settings OnSaveGlobal() => settings_;
    public List<IMenuMod.MenuEntry> GetMenuData(IMenuMod.MenuEntry? menu)
    {
        List<IMenuMod.MenuEntry> menus = new();
        menus.Add(
            new()
            {
                Values = new string[]
                {
                    Language.Language.Get("MOH_ON", "MainMenu"),
                    Language.Language.Get("MOH_OFF", "MainMenu"),
                },
                Saver = i => settings_.status = i,
                Loader = () => settings_.status
            }
        );
        return menus;
    }
}
