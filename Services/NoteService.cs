using System.Collections.Generic;
using System.Linq;
using AppNote.Data;
using Microsoft.EntityFrameworkCore;

namespace AppNote.Services
{
    public class NoteService
    {
        protected readonly IDbContextFactory<AppNoteDbContext> _dbContextFactory;

        public List<NoteClass> FindedNotes { get; private set; }

        public NoteService(IDbContextFactory<AppNoteDbContext> contextFactory)
        {
            _dbContextFactory = contextFactory;

            using AppNoteDbContext context = _dbContextFactory.CreateDbContext();
            FindedNotes = context.Notes.ToList();
        }

        public IList<NoteClass> DisplayNotes()
        {
            return FindedNotes;
        }

        public void FindNotes(string searchStr)
        {
            using AppNoteDbContext context = _dbContextFactory.CreateDbContext();
            if (string.IsNullOrWhiteSpace(searchStr))
                FindedNotes = context.Notes.ToList();
            else
            {
                IList<NoteClass> notes = context.Notes.ToList();

                //Find matching in title
                IList<NoteClass> matchingNotes = notes.Where(item => item.Title.ToLower().Contains(searchStr.ToLower())).ToList();
                //Find matching in text
                FindedNotes = matchingNotes.Concat(notes.Where(item => item.Text.ToLower().Contains(searchStr.ToLower()))).Distinct().ToList();
            }
        }

        public void AddNewNote(NoteClass newNote)
        {
            using AppNoteDbContext context = _dbContextFactory.CreateDbContext();
            context.Notes.Add(newNote);
            context.SaveChanges(); 
        }

        public void UpdateNote()
        {
            using AppNoteDbContext context = _dbContextFactory.CreateDbContext();
            context.SaveChanges();
        }
    }
}
