﻿using System;

namespace ArtonitRestApi.Models
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class)]
    public class DatabaseNameAttribute : Attribute
    {
        public string Value { get; }

        public DatabaseNameAttribute(string value)
        {
            Value = value;
        }
    }
}
