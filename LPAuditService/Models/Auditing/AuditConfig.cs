﻿using LPAuditService.Models.Areas;
using LPAuditService.Models.Checking;
using System;
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

        /// <summary>
        ///  d for Day
        ///  m for Month
        ///  w for Week
        ///  q for quincena
        /// </summary>
        [Display(Name = "Period")]
        [Column("int_idPeriod")]
        public Period int_Period { get; set; }

        [NotMapped]
        public string SelectedPeriod { get; set; }

        [NotMapped]
        public string[] Days { get; set; }

        /// <summary>
        /// Aqui se guardará la ultima fecha en que se crearón los eventos en base a la configuración de esta auditoria y del checklist,
        /// esto con el fin de que el servicio pueda comparar contra la fecha actual y crear nuevos eventos.
        /// </summary>
        /// [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode=true)]
        [DataType(DataType.Date)]
        public DateTime dte_LastDateCreated { get; set; }
        
        public Audit Audit { get; set; }
        
        public Area Area { get; set; }

        public List<UsersAudits> UsersAudits { get; set; }

        public List<AuditsChecklists> AuditsChecklists { get; set; }

    }
}