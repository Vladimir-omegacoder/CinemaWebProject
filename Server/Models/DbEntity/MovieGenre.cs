﻿using System;
using System.Collections.Generic;

namespace Server.Models.DbEntity;

public partial class MovieGenre
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
}
