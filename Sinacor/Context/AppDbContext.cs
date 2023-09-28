﻿using Microsoft.EntityFrameworkCore;
using Sinacor.Domain;

namespace Sinacor.Context
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Tarefa> Tarefas { get; set; }

    }
}
