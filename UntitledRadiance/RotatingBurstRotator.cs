namespace UntitledRadiance;
public class RotatingBurstRotator : MonoBehaviour
{
    public float degreesPerSecond = 15;
    private void Update()
    {
        gameObject.transform.Rotate(0, 0, Time.deltaTime * degreesPerSecond);
    }
}