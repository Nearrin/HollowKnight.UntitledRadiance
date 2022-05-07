namespace UntitledRadiance;
public class EyeBeamsRotator : MonoBehaviour
{
    public bool rotating = false;
    public float degreesPerSecond = 30;
    private void Update()
    {
        if (rotating)
        {
            gameObject.transform.Rotate(0, 0, Time.deltaTime * degreesPerSecond);
        }
    }
}