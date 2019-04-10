using LPAuditService.Models.Checking;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LPAuditService.Models.Calendar
{
    [Table("Tbl_Event")]
    public class Event
    {
        [Key]
        public int int_IdEvent { get; set; }
        public Checklist Checklist_Id { get; set; }

        public string chr_Title { get; set; }
        public DateTime dte_ScheduleDate { get; set; }

        /// <summary>
        ///  0 -> SCHEDULED (Evento o checklist pendiente)
        ///  1 -> NOSTANSWERED (Es cuando el evento ha caducado y el checklist no se contestó)
        ///  2 - > Answered (El checklist se contestó)
        /// </summary>
        public int int_State { get; set; } 
    }
}