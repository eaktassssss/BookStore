using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Dto
{
    public class BookListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberOfPage { get; set; }
        public string CategoryName { get; set; }
        public string AuthorName { get; set; }
        public string PublisherName { get; set; }
    }
}
