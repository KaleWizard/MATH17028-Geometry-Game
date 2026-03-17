public class Singleton<T> where T : class, new()
{
    private static T instance;
    public static T Instance => instance ??= new();
}