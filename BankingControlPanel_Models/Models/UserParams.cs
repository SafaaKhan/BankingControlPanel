using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingControlPanel_Models.Models;

public class UserParams
{
    private const int MaxPageSize = 10;

    public int PageNumber { get; set; } = 1;

    private int _pageSize = 5;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }
    public int ParamsNum { get; set; }
    public string Sex { get; set; }
    public string CreatedAt { get; set; }//orderby
    public string City { get; set; }
}
