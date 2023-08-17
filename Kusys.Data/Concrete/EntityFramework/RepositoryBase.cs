namespace Kusys.Data.Concrete.EntityFramework;

public class RepositoryBase
{
    protected static KusysContext context;
    // Eşzamanlılık (concurrency) sorunlarını ele almak için kullanılan bir kilitleme mekanizması.
    protected static object _lockSync = new object();

    protected RepositoryBase()
    {
        CreateContext();
    }

    private static void CreateContext()
    {
        if (context == null)
        {
            // Eşzamanlı erişimde sorun yaşanmaması için lock kullanılır.
            lock (_lockSync)
            {
                // Tekrar kontrol edilir, çünkü başka bir thread context oluşturmuş olabilir.
                if (context == null)
                {
                    // Yeni bir KusysContext nesnesi oluşturulur.
                    context = new KusysContext();
                }
            }
        }
    }
}