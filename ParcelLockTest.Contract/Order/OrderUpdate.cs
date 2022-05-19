﻿using System.ComponentModel.DataAnnotations;
#pragma warning disable CS8618

namespace ParcelLockTest.Contract.Order;

public class OrderUpdate {
    [Required]
    public List<string> Products { get; set; } = new();

    [Required]
    public decimal Price { get; set; }

    [Required]
    [RegularExpression(@"^\+7\d{3}-\d{3}-\d{2}-\d{2}$")]
    public string Phone { get; set; }

    [Required]
    public string FullName { get; set; }
}
