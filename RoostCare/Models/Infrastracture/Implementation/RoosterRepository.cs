using AutoMapper;
using RoostCare.Models.Domain;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RoostCare.Models.ViewModel;
using RoostCare.Models.Services;
using System.Security.Cryptography.Xml;

namespace RoostCare.Models.Infrastracture.Implementation
{
    public class RoosterRepository:BaseRepository<Rooster>, IRoosterRepository
    {
        private readonly ApplicationDbContext _db;
        public RoosterRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
        /*
         public async Task<IEnumerable<RoosterViewModel>> RoosterRecords()
         {
             var roosters = _db.Roosters.ToList();
             var breeds = _db.Breeds.ToList();
             var fHistory = _db.FightHistories.ToList();
             return roosters
                 .Join(breeds,
                 rooster => rooster.BreedId,
                 breed => breed.Id,
                 (rooster, breed) => new
                 {
                     Rooster = rooster,
                     Breed = breed
                 })
                 .Join(fHistory,
                 rooster => rooster.Rooster.Id,
                 fhist => fhist.RoosterId,
                 (rooster, fhist) => new RoosterViewModel
                 {
                     Rooster = rooster.Rooster,
                     Breed = rooster.Breed,
                     FightHistory = fhist
                 }).ToList();
         }

        public async Task<IEnumerable<RoosterViewModel>> RoosterRecords()
        {
            var roosters = _db.Roosters.ToList();
            var breeds = _db.Breeds.ToList();
            var fHistory = _db.FightHistories.ToList();

            var result = roosters
                .Join(breeds,
                    rooster => rooster.BreedId,
                    breed => breed.Id,
                    (rooster, breed) => new
                    {
                        Rooster = rooster,
                        Breed = breed
                    })
                .GroupJoin(fHistory,
                    rooster => rooster.Rooster.Id,
                    fhist => fhist.RoosterId,
                    (rooster, fhists) => new
                    {
                        Rooster = rooster.Rooster,
                        Breed = rooster.Breed,
                        FightHistories = fhists.DefaultIfEmpty()
                    })
                .SelectMany(
                    x => x.FightHistories.Select(fhist => new RoosterViewModel
                    {
                        Rooster = x.Rooster,
                        Breed = x.Breed,
                        FightHistory = fhist
                    })
                ).ToList();

            return result;
        }

        */
        public async Task<IEnumerable<RoosterViewModel>> RoosterRecords()
        {
            var roosters = _db.Roosters.ToList();
            var breeds = _db.Breeds.ToList();
            var fHistory = _db.FightHistories.ToList();
            var incomes = _db.Incomes.ToList();

            var fResult = roosters
                .Join(breeds,
                r => r.BreedId,
                b => b.Id,
                (r, b) => new
                {
                    Rooster = r,
                    Breed = b
                })
                .GroupJoin(fHistory,
                r => r.Rooster.Id,
                f => f.RoosterId,
                (r, f) => new
                {
                    Rooster = r.Rooster,
                    Breed = r.Breed,
                    FightHistory = f
                })
                .GroupJoin(incomes,
                r => r.Rooster.Id,
                i => i.RoosterId,
                (r, i) => new
                {
                    Rooster = r.Rooster,
                    Breed = r.Breed,
                    FightHistory = r.FightHistory,
                    Income = i
                })
                .Select(x => new RoosterViewModel
                {
                    Rooster = x.Rooster,
                    Breed = x.Breed,
                    FightHistory = x.FightHistory.FirstOrDefault(),
                    Income = x.Income.FirstOrDefault()
                }).ToList();
            return fResult;
        }   

        public async Task<IEnumerable<RoosterReportViewModel>> RoosterReportRecords(DateTime? from,DateTime? to)
        {
            var roosters =  _db.Roosters.ToList();
            var breeds =  _db.Breeds.ToList();
            var fightHistories = _db.FightHistories.ToList();

            var rRecords = roosters
                .Join(breeds,
                r => r.BreedId,
                b => b.Id,
                (r, b) => new
                {
                    Rooster = r,
                    Breed = b
                })
                .Join(fightHistories,
                r => r.Rooster.Id,
                f => f.RoosterId,
                (r, f) => new
                {
                    Rooster = r.Rooster,
                    Breed = r.Breed,
                    FightHistory = f
                })
                .GroupBy(x => x.Rooster.Id)
                .Select(r => new RoosterReportViewModel
                {
                    Rooster = r.First().Rooster,
					Breed = r.First().Breed,
					CountWin = r.Count(x => x.FightHistory != null && x.FightHistory.FightResult == FightResult.Win && (from != null ? x.FightHistory?.DateOfFight.Date >= from?.Date : true) && (to != null ? x.FightHistory?.DateOfFight.Date <= to?.Date : true)),
					CountDraw = r.Count(x => x.FightHistory != null && x.FightHistory.FightResult == FightResult.Draw && (from != null ? x.FightHistory?.DateOfFight.Date >= from?.Date : true) && (to != null ? x.FightHistory?.DateOfFight.Date <= to?.Date : true)),
					CountLoss = r.Count(x => x.FightHistory != null && x.FightHistory.FightResult == FightResult.Loss && (from != null ? x.FightHistory?.DateOfFight.Date >= from?.Date : true) && (to != null ? x.FightHistory?.DateOfFight.Date <= to?.Date : true)),

				})
                .ToList();
            return rRecords;
        }

        
    }
}
