namespace UntitledRadiance;
public class TrackingBurstRotator : MonoBehaviour
{
    private float duration = 1f;
    private Vector3 initalAngles;
    private GameObject radiantBeam;
    private bool fire = false;
    private float accumulatedAngles;
    private int direction;
    private void Awake()
    {
        initalAngles = gameObject.transform.rotation.eulerAngles;
        radiantBeam = gameObject.transform.Find("Radiant Beam").gameObject;
    }
    private void Update()
    {
        var state = radiantBeam.LocateMyFSM("Control").ActiveStateName;
        if (fire)
        {
            if (state != "Fire")
            {
                fire = false;
            }
            else
            {
                var angle = Time.deltaTime * direction * 90 / duration;
                if (Math.Abs(accumulatedAngles + angle) <= 90)
                {
                    accumulatedAngles += angle;
                    gameObject.transform.Rotate(0, 0, angle);
                }
            }
        }
        else
        {
            if (state != "Fire")
            {
                if (state == "Inert")
                {
                    var rotation = gameObject.transform.rotation;
                    rotation.eulerAngles = initalAngles;
                    gameObject.transform.rotation = rotation;
                }
            }
            else
            {
                fire = true;
                accumulatedAngles = 0;
                var bossControl = GameObject.Find("Boss Control").gameObject;
                var absoluteRadiance = bossControl.transform.Find("Absolute Radiance").gameObject;
                if (absoluteRadiance.transform.position.x < HeroController.instance.transform.position.x)
                {
                    direction = 1;
                }
                else
                {
                    direction = -1;
                }
            }
        }

    }
}