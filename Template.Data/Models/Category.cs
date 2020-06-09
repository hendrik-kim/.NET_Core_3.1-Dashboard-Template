﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace Template.Data.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string CategName { get; set; }

        public IEnumerable<Post> Posts { get; set; }
    }
}

