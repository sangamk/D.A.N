public class Shared
{
    private static Shared instance;

    public static Shared GetInstance()
    {
        if(instance == null)
        {
            instance = new Shared(); 
        }
        return instance;
    }

    public float speed = 20f;
    public float distance = 40f;
}
