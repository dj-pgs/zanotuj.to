using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Transactions;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity.EntityFramework;
using Zanotuj.To.WebApplication.Controllers;
using Zanotuj.To.WebApplication.Controllers.Api;
using Zanotuj.To.WebApplication.Models;
using Zanotuj.To.WebApplication.Repository;

namespace Zanotuj.To.WebApplication.Services
{
    public class NoteService : INoteService
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IUserContext _userContext;

        public NoteService(IApplicationDbContext dbContext, IUserContext userContext)
        {
            _dbContext = dbContext;
            _userContext = userContext;
        }

        public List<NoteViewModel> GetNotes()
        {
            var notes =
                _dbContext.Notes.OrderByDescending(n => n.CreateTime).Take(30).Select(
                    note =>
                        new
                        {
                            Autor = note.User.UserName,
                            PhotoClaim = note.User.Claims.FirstOrDefault(c => c.ClaimType == "profile:photo:url"),
                            Title = note.Title,
                            Content = note.NoteContent.Substring(0, 160),
                            HashTags = note.HashTags.Select(h => h.Name),
                            CreateTime = note.CreateTime,
                            note.Id
                        }).ToList().Select(n => new NoteViewModel()
                        {
                            Content = n.Content,
                            Title = n.Title,
                            HashTags = n.HashTags,
                            Autor = n.Autor,
                            CreateTime = n.CreateTime.ToUniversalTime(),
                            PhotoUrl = GetUserPhotoUrl(n.PhotoClaim),
                            Id = n.Id
                        }).ToList();

            return notes;
        }

        private static string GetUserPhotoUrl(IdentityUserClaim photoClaim)
        {
            return photoClaim == null ? "/assets/avatar.jpg" : photoClaim.ClaimValue;
        }

        public Result<int> GetNotes(NoteAddViewModel model)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions { IsolationLevel = IsolationLevel.Serializable }))
            {
                var note = new Note()
                {
                    Title = model.Title,
                    NoteContent = model.Content,
                    ApplicationUserId = _userContext.UserId
                };
                _dbContext.Notes.Add(note);

                var hashTags = model.HashTags?.Split(',');
                if (hashTags != null)
                {
                    foreach (var hashTagName in hashTags)
                    {
                        var hashTag = _dbContext.HashTags.FirstOrDefault(h => h.Name == hashTagName);
                        if (hashTag == null)
                        {
                            hashTag = new HashTag() { Name = hashTagName };
                            _dbContext.HashTags.Add(hashTag);

                        }
                        note.HashTags.Add(hashTag);
                    }
                }
                _dbContext.SaveChanges();
                scope.Complete();
                return new Result<int>(note.Id);

            }


        }

        public NoteViewViewModel GetNoteForView(int id)
        {
            var note = _dbContext.Notes.Include(n => n.User).Include(n => n.HashTags).Single(n => n.Id == id);
            var noteViewViewModel = new NoteViewViewModel()
            {
                Id = note.Id,
                Title = note.Title,
                Content = note.NoteContent,
                PhotoUrl = GetUserPhotoUrl(note.User.Claims.FirstOrDefault(c => c.ClaimType == "profile:photo:url")),
                Author = note.User.UserName,
                CreateTime = note.CreateTime.ToUniversalTime(),
                HashTags = note.HashTags.Select(h => "#" + h.Name),
                DaysAgo = (int)(DateTime.Now - note.CreateTime).TotalDays
            };
            return noteViewViewModel;
        }

        public NoteEditViewModel GetNoteForEditViewModel(int id)
        {
            var note = _dbContext.Notes.Include(n => n.User).Include(n => n.HashTags).Single(n => n.Id == id);
            var noteViewViewModel = new NoteEditViewModel()
            {
                Title = note.Title,
                Content = note.NoteContent,
                HashTags = string.Join(",", note.HashTags.Select(h => h.Name))

            };
            return noteViewViewModel;
        }

        public Result<int> EditNote(NoteEditViewModel note, int id)
        {
            var noteEntity = _dbContext.Notes.Include(n => n.User).Include(n => n.HashTags).Single(n => n.Id == id);

            if (_userContext.UserId != noteEntity.ApplicationUserId)
            {
                return new Result<int>(0) { IsSuccess = false };
            }
            noteEntity.Title = note.Title;
            noteEntity.NoteContent = note.Content;
            foreach (var hashtag in noteEntity.HashTags.ToList())
            {
                noteEntity.HashTags.Remove(hashtag);
            }
          
            if (note.HashTags != null)
            {
                var hashtags = note.HashTags.Split(',');
                foreach (var hashtag in hashtags)
                {
                    var hashTag = _dbContext.HashTags.FirstOrDefault(h => h.Name == hashtag);
                    if (hashTag == null)
                    {
                        hashTag = new HashTag() { Name = hashtag };
                        _dbContext.HashTags.Add(hashTag);

                    }
                    noteEntity.HashTags.Add(hashTag);

                }
            }
            _dbContext.SaveChanges();
            return new Result<int>(0) { IsSuccess = true };
        }

        public List<NoteViewModel> GetNotes(SearchDto searchModel)
        {
            if (searchModel.PageSize == 0 || searchModel.Page==0)
            {
                searchModel.Page = 1;
                searchModel.PageSize = 30;
            }
            var notesCtx = _dbContext.Notes.AsQueryable();
            foreach (var phrase in searchModel.Phrase)
            {
                notesCtx = notesCtx.Where(note => note.Title.Contains(phrase) || note.NoteContent.Contains(phrase));
            }
            foreach (var hashtag in searchModel.Hashtags)
            {
                notesCtx = notesCtx.Where(note => note.HashTags.Any(h => h.Name == hashtag));
            }
            foreach (var author in searchModel.Authors)
            {
                notesCtx = notesCtx.Where(note => note.User.UserName==author);
            }
            notesCtx = notesCtx.OrderByDescending(n=>n.CreateTime).Skip((searchModel.Page-1)*searchModel.PageSize).Take(searchModel.PageSize);
            List<NoteViewModel> notes = notesCtx

                .Select(note => new
                {
                    Autor = note.User.UserName,
                    PhotoClaim = note.User.Claims.FirstOrDefault(c => c.ClaimType == "profile:photo:url"),
                    Title = note.Title,
                    Content = note.NoteContent.Substring(0, 160),
                    HashTags = note.HashTags.Select(h => h.Name),
                    CreateTime = note.CreateTime,
                    note.Id
                }).ToList().Select(n => new NoteViewModel()
                {
                    Content = n.Content,
                    Title = n.Title,
                    HashTags = n.HashTags,
                    Autor = n.Autor,
                    CreateTime = n.CreateTime.ToUniversalTime(),
                    PhotoUrl = GetUserPhotoUrl(n.PhotoClaim),
                    Id = n.Id
                }).ToList();


            return notes;
        }

        public List<NoteViewModel> GetNotes(string query)
        {
            var searchmodel=new SearchDto();
            if (query == null || query.Length < 3)
            {
                return GetNotes();
            }
            switch (query[0])
            {
                case '#':
                    searchmodel.Hashtags.Add(query.Substring(1));
                    break;
                case '@':
                    searchmodel.Authors.Add(query.Substring(1));
                    break;
                default:
                    searchmodel.Phrase.Add(query.Substring(1));
                    break;
            }
            return GetNotes(searchmodel);
        }

        public void Delete(int id)
        {
            var noteEntity = _dbContext.Notes.Include(n => n.User).Include(n => n.HashTags).Single(n => n.Id == id);

            if (_userContext.UserId != noteEntity.ApplicationUserId)
            {
                return;
            }

            _dbContext.Notes.Remove(noteEntity);
            _dbContext.SaveChanges();
        }
    }

    public interface IUserContext
    {
        string Nick { get; }

        bool IsAuthenticated { get; }

        string ProfilePhotoUrl { get; }
        string UserId { get; }
    }

    public interface INoteService
    {
        List<NoteViewModel> GetNotes();
        Result<int> GetNotes(NoteAddViewModel model);
        NoteViewViewModel GetNoteForView(int id);
        NoteEditViewModel GetNoteForEditViewModel(int id);
        Result<int> EditNote(NoteEditViewModel note, int id);
        List<NoteViewModel> GetNotes(SearchDto searchModel);
        List<NoteViewModel> GetNotes(string query);
        void Delete(int id);
    }

    public class Result<T>
    {

        public Result(T data)
        {
            Data = data;
        }

        public bool IsSuccess { get; set; }

        public T Data { get; set; }
    }

    public class NoteViewModel
    {
        public string Autor { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public IEnumerable<string> HashTags { get; set; }
        public DateTime CreateTime { get; set; }
        public string PhotoUrl { get; set; }
        public int Id { get; set; }
    }
}