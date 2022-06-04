using System;

namespace PL_API.Models
{
    public class NewsModel
    {
        public int Id { get; set; }


        public int NewsId { get; set; }

        public DateTime Date { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public int AuthorId { get; set; }

        public int RubricId { get; set; }


        public int TagId { get; set; }

        public string TagName { get; set; }
    }
}
