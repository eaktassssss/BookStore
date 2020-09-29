using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BookStore.Entities
{

    [Table("Books")]
    public class Books: BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberOfPage { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Categories Categories { get; set; }

        public int AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public Authors Authors { get; set; }


        public int PublisherId { get; set; }
        [ForeignKey("PublisherId")]
        public Publishers Publishers { get; set; }
    }
}
