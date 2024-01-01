namespace RespositoryPatternWithUOW.Core.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Author Author { get; set; }
        public int AuthorID { get; set; }
    }
}
