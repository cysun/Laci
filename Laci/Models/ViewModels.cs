using System;

namespace Laci.Models
{
    public class RecordViewModel : Record
    {
        public int? NewCases { get; set; }
    }

    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
