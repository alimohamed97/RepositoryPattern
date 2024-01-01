using RespositoryPatternWithUOW.Core.Models;

namespace RespositoryPatternWithUOW.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<Author> Authors { get; }
        IBaseRepository<Book> Books { get; }
        int Complete();

    }
}
