using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Response
{
    public class ServiceResponse<T> where T : class, new()
    {
        public bool IsSuccessful { get; set; }
        public T Entity { get; set; }
        public string Message { get; set; }
    }
}
