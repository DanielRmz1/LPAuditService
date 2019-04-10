﻿using LPAuditService.Models.Account;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LPAuditService.Models.Checking
{
    //Modelo que ayudará a relacionar un checklist con los usuarios que pueden atenderlo
    [Table("Tbl_UsersChecklist")]
    public class UsersChecklist
    {
        [Key]
        public int int_IdUsersChecklist { get; set; }
        public Checklist Checklist { get; set; }
        public User User { get; set; }
    }
}