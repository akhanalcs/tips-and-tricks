using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class ServiceCallResultViewModel
    {
        public ServiceCallResultViewModel()
        {
            Log = new List<string>();
        }

        public bool Success { get; set; }

        public List<string> Log { get; set; }

        public void Combine(ServiceCallResultViewModel another)
        {
            if (another == null) throw new ArgumentNullException(nameof(another));
            if (!another.Success) Success = false;
            if (another.Log.Count > 0) Log.AddRange(another.Log);
        }
    }
}
