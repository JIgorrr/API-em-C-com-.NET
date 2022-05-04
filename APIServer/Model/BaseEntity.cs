using System.ComponentModel.DataAnnotations;

namespace APIServer.Model
{
    public class BaseEntity
    {
        [Key]
        public long Id { get; set; }
    }
}
