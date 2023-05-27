﻿using Microsoft.EntityFrameworkCore;
using SistamaDeContatos.Models;

namespace SistamaDeContatos.Data
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options) 
        { 
        }

        public DbSet<ContatoModel> Contatos { get; set; }
    }
}
