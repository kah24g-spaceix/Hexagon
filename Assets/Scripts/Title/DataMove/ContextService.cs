public static class ContextService
{
    private static object context;

    public static void Register<T>(T data) where T : class
    {
        context = data;
    }

    public static T Resolve<T>() where T : class
    {
        var result = context as T;
        context = null; // 일회성 사용 후 데이터 제거
        return result;
    }
}
