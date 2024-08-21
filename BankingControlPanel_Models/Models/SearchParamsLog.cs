using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingControlPanel_Models.Models
{
    public class SearchParamsLog
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Parameters { get; set; }
        public DateTime CreatedAt { get; set; }=DateTime.UtcNow;
    }
}
