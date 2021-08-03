using System.Collections.Generic;
using System.Linq;
using AppNote.Data;

namespace AppNote.Services
{
    public class NoteService
    {
        protected readonly AppNoteDbContext _dbContext;

        public NoteService(AppNoteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<NoteClass> DisplayNotes()
        {
            return _dbContext.Notes.ToList();
        }

        public void AddNewNote(NoteClass newNote)
        {
            _dbContext.Notes.Add(newNote);
            _dbContext.SaveChanges(); 
        }
    }
}
