using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Core.Entities
{
    public class TaskEntity : EntityBase
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public TaskStatus Status { get; set; }
        public int AssingedToUserId { get; set; }
        public int CreatedByUserId { get; set; }
        public int UpdatedByUserId { get; set; }
        public int TeamId { get; set; }
        public DateTime DueDate { get; set; }

        [NotMapped]
        [ForeignKey("AssingedToUserId")]
        public UserEntity AssingedToUser{ get; set; }
        
        [NotMapped]
        [ForeignKey("CreatedByUserId")]
        public UserEntity CreatedByUser { get; set; }

        [NotMapped]
        [ForeignKey("UpdatedByUserId")]
        public UserEntity UpdatedByUser { get; set; }

        [NotMapped]
        [ForeignKey("TeamId")]
        public TeamEntity AssignedTeam { get; set; }
    }
}
