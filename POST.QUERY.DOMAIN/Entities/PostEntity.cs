using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POST.QUERY.DOMAIN.Entities
{
    [Table("Post")]
    public class PostEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Author { get; set; }
        public DateTime DataPosted { get; set; }
        public string Message { get; set; }
        public int Likes { get; set; }
        public virtual ICollection<CommentEntity> Comments { get; set; }
    }
}
