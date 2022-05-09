namespace UntitledRadiance;
public class RotatingBurstRotator : MonoBehaviour
{
    public float degreesPerSecond = 30;
    private void Update()
    {
        gameObject.transform.Rotate(0, 0, Time.deltaTime * degreesPerSecond);
    }
}