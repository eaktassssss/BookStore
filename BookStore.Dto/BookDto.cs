using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Dto
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NumberOfPage { get; set; }
        public string CategoryId { get; set; }
        public string AuthorId { get; set; }
        public string PublisherId { get; set; }
    }
}
