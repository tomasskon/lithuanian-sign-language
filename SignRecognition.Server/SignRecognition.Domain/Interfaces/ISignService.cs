﻿using SignRecognition.Domain.Models;

namespace SignRecognition.Domain.Interfaces;

public interface ISignService
{
    Task AddSignsAsync(IEnumerable<Sign> signs);
}