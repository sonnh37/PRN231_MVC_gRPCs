﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace KoiOrderingSystem.Data.Models;

public partial class FarmCategory
{
    public Guid FarmId { get; set; }

    public Guid CategoryId { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Category Category { get; set; }

    public virtual Farm Farm { get; set; }
}