﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Models;

public class Content
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Desc { get; set; }
    public int Dislike { get; set; }
    public int Like { get; set; }

}
