using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ServiceResponse
{
    public class ServiceResponse<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; }   
        public string? Message {  get; set; }
        public string? Error {  get; set; }
        public List<string>? ErrorMessages { get; set; } = null;
    }
}
