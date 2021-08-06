using System.Collections.Generic;
using System.Linq;
using AppNote.Data;

namespace AppNote.Services
{
    public class NoteService
    {
        protected readonly AppNoteDbContext _dbContext;

        public List<NoteClass> FindedNotes { get; private set; }

        public NoteService(AppNoteDbContext dbContext)
        {
            _dbContext = dbContext;
            FindedNotes = _dbContext.Notes.ToList();
        }

        public IList<NoteClass> DisplayNotes()
        {
            return FindedNotes;
        }

        public void FindNotes(string searchStr)
        {
            if (string.IsNullOrWhiteSpace(searchStr))
                FindedNotes = _dbContext.Notes.ToList();
            else
            {
                IList<NoteClass> notes = _dbContext.Notes.ToList();

                //Find matching in title
                IList<NoteClass> matchingNotes = notes.Where(item => item.Title.ToLower().Contains(searchStr.ToLower())).ToList();
                //Find matching in text
                FindedNotes =  matchingNotes.Concat(notes.Where(item => item.Text.ToLower().Contains(searchStr.ToLower()))).ToList();
            }
        }

        public void AddNewNote(NoteClass newNote)
        {
            _dbContext.Notes.Add(newNote);
            _dbContext.SaveChanges(); 
        }

        public void UpdateNote()
        {
            _dbContext.SaveChanges();
        }
    }
}
