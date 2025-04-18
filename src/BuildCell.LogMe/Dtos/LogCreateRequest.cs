﻿using BuildCell.LogMe.Constants;

namespace BuildCell.LogMe.Dtos
{
    public class LogCreateRequest
    {
        public string Name { get; set; } = "";
        public LogEnvironment Environment { get; set; }
        public string Trace { get; set; }
        public string Description { get; set; }
    }
}