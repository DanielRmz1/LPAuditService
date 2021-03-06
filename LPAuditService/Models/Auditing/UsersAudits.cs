﻿using LPAuditService.Models.Account;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LPAuditService.Models.Auditing
{
    //Modelo que ayudará a relacionar un checklist con los usuarios que pueden atenderlo
    [Table("Tbl_UsersAudits")]
    public class UsersAudits
    {
        [Key]
        public int int_IdUsersAudit { get; set; }
        public AuditConfig AuditConfig { get; set; }
        public User User { get; set; }
    }
}