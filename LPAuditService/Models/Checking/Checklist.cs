﻿using LPAuditService.Models.Account;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LPAuditService.Models.Checking
{
    [Table("Tbl_Checklist")]
    public class Checklist
    {
        [Key]
        public int int_IdList { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Code")]
        [Index(IsUnique = true)]
        [RegularExpression(@"^\S*$", ErrorMessage = "No white space allowed in code field.")]
        public string chr_Clave { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Name")]
        public string chr_Name { get; set; }

        [StringLength(200)]
        [Display(Name = "Description")]
        public string chr_Description { get; set; }
        
        public User int_Owner { get; set; }
        
        public List<Question> Questions { get; set; }
        
    }
}