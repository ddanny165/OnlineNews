using System;

namespace BBL.DTO
{
    public class NewsDTO
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public int AuthorId { get; set; }

        public int RubricId { get; set; }
    }
}
