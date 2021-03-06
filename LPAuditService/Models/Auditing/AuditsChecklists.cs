﻿using LPAuditService.Models.Checking;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LPAuditService.Models.Auditing
{
    [Table("Tbl_AuditsChecklists")]
    public class AuditsChecklists
    {
        [Key]
        public int int_IdAuditsChecklists { get; set; }

        public AuditConfig AuditConfig { get; set; }

        public Checklist Checklist { get; set; }

    }
}
