﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Net1711_231_ASM3_SE172092_NGUYENHOANGSON.Data.Models;

public partial class Size
{
    public Guid Id { get; set; }

    public Guid? KoiFishId { get; set; }

    public decimal? SizeInCm { get; set; }

    public decimal? SizeInGram { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public bool IsDeleted { get; set; }

    public string Note { get; set; }

    public virtual KoiFish KoiFish { get; set; }
}