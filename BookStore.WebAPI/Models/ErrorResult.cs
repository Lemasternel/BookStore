using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace BookStore.WebAPI.Areas.Books.Model
{
    public class ErrorResult
    {
        public string Message { get; set; }

        public ErrorResult()
        {

        }

        public ErrorResult(string message)
        {
            Message = message;
        }
    }
}