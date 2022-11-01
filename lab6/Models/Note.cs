﻿using System;

namespace visual_programming_lab6.Models
{
	public class Note
	{
        public Note(string name, string text, DateTimeOffset date)
        {
            Name = name;
            Text = text;
            Date = date;
        }
        public string Name { get; set; }
        public string Text { get; set; }
        public DateTimeOffset Date { get; set; }
    }
}
