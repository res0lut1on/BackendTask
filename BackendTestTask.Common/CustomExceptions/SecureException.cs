﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTestTask.AspNetExtensions.Models
{
    public class SecureException : Exception
    {
        public SecureException(string message) : base(message) { }
        public string? QueryParameters { get; set; }
        public string BodyParameters { get; set; }
    }
}
