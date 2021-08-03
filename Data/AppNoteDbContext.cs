using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AppNote.Data
{
    public class AppNoteDbContext : DbContext
    {
        public AppNoteDbContext(DbContextOptions<AppNoteDbContext> options) : base(options)
        {
        }

        public DbSet<NoteClass> Notes { get; set; }
    }
}
