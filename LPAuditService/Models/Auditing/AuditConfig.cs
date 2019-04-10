using LPAuditService.Models.Checking;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LPAuditService.Models.Auditing
{
    [Table("Tbl_AuditConfig")]
    public class AuditConfig
    {
        [Key]
        public int int_IdAuditConfig { get; set; }

        public int int_Level { get; set; }

        public Checklist int_Checklist { get; set; }
        
        public Audit Audit { get; set; }
        
    }
}