using ContactManage.Repository.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManage.Repository.Models
{
    public class LogInfo
    {
        public int Id { get; set; }               
        public int RecordId { get; set; }         
        public ActionType Action { get; set; }  
        public string AppName { get; set; } = "Contact API";
        public string? UserId { get; set; }   
        public string UserName { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}
