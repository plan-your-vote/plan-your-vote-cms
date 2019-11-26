using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Models;
using Web.Data;

namespace Web
{
    [Authorize(Roles = Constants.Account.ROLE_ADMIN)]
    public class ElectionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ElectionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Elections
        public async Task<IActionResult> Index()
        {
            return View(await _context.Elections.ToListAsync());
        }

        // GET: Elections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var election = await _context.Elections
                .FirstOrDefaultAsync(m => m.ElectionId == id);
            if (election == null)
            {
                return NotFound();
            }

            return View(election);
        }

        // GET: Elections/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Elections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ElectionName,StartDate,EndDate,Description")] Election election)
        {
            if (ModelState.IsValid)
            {
                _context.Add(election);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(election);
        }

        // POST: Elections/Copy
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Copy(int? id)
        {
            // New Election
            var election = await _context.Elections.FindAsync(id);
            election.ElectionId = _context.Elections.OrderByDescending( e => e.ElectionId).FirstOrDefault().ElectionId + 1;
            election.ElectionName = "Copy of " + election.ElectionName;
            _context.Add(election);
            await _context.SaveChangesAsync();

            // Copy Races
            var races = _context.Races.Where(r => r.ElectionId == id);
            var raceId = _context.Races.OrderByDescending( r => r.ElectionId).FirstOrDefault().ElectionId + _context.Races.Count();
            var oldRaceIdCount = _context.Races.OrderByDescending( r => r.ElectionId).FirstOrDefault().ElectionId + _context.Races.Count();
            foreach(var r in races){
                var tempRace = r;
                tempRace.RaceId = ++raceId;
                tempRace.ElectionId = election.ElectionId;
                _context.Add(tempRace);
                await _context.SaveChangesAsync();
            }

            // Copy Candidate
            var candidates = _context.Candidates.Where(c => c.ElectionId == id);
            var candidatesId = _context.Candidates.OrderByDescending( c => c.ElectionId).FirstOrDefault().ElectionId + _context.Candidates.Count();
            foreach(var c in candidates){
                var tempCandidate = c;
                var cId = c.CandidateId;
                tempCandidate.CandidateId = ++candidatesId;
                tempCandidate.ElectionId = election.ElectionId;
                _context.Add(tempCandidate);
                await _context.SaveChangesAsync();

                // Copy CandidateDetails
                var candidateDetails = _context.CandidateDetails.Where(cd => cd.CandidateId == cId);
                var candidateDetailsId = _context.CandidateDetails.OrderByDescending( cd => cd.CandidateId).FirstOrDefault().CandidateId + _context.CandidateDetails.Count();
                foreach(var cd in candidateDetails){
                    var tempCandidateDetails = cd;
                    tempCandidateDetails.CandidateId = tempCandidate.CandidateId;
                    tempCandidateDetails.ID = ++candidateDetailsId;
                    _context.Add(tempCandidateDetails);
                    await _context.SaveChangesAsync();
                }

                // Copy Candidate Contacts
                var candidateContacts = _context.Contacts.Where(con => con.CandidateId == cId);
                var candidateContactsId = _context.Contacts.OrderByDescending( con => con.CandidateId).FirstOrDefault().CandidateId + _context.Contacts.Count();
                foreach(var con in candidateContacts){
                    var tempCandidateContacts = con;
                    tempCandidateContacts.CandidateId = tempCandidate.CandidateId;
                    tempCandidateContacts.ContactId = ++candidateContactsId;
                    _context.Add(tempCandidateContacts);
                    await _context.SaveChangesAsync();
                }

                // Copy Candidate Races
                var candidateRaces = _context.CandidateRaces.Where(r => r.CandidateId == cId);
                var candidateRacesId = _context.CandidateRaces.OrderByDescending( cr => cr.CandidateId).FirstOrDefault().CandidateId + _context.CandidateRaces.Count();

                foreach(var r in candidateRaces){
                    var tempRace = r;
                    tempRace.RaceId = oldRaceIdCount + r.RaceId;
                    tempRace.CandidateId = tempCandidate.CandidateId;
                    tempRace.CandidateRaceId = ++candidateRacesId;
                    _context.Add(tempRace);
                    await _context.SaveChangesAsync();
                }
            }

            // Copy Polling Places
            var pollingPlaces = _context.PollingPlaces.Where(c => c.ElectionId == id);
            var pollingPlacesId = _context.PollingPlaces.OrderByDescending( pp => pp.ElectionId).FirstOrDefault().ElectionId + _context.PollingPlaces.Count();
            foreach(var pp in pollingPlaces){
                var tempPp = pp;
                var ppId = pp.PollingPlaceId;
                tempPp.ElectionId = election.ElectionId;
                tempPp.PollingPlaceId = ++pollingPlacesId;
                _context.Add(tempPp);
                await _context.SaveChangesAsync();

                // Copy PollingPlaceDates
                var pollingPlaceDates = _context.PollingPlaceDates.Where(ppd => ppd.PollingPlaceId == ppId);
                var pollingDateId = _context.PollingPlaceDates.OrderByDescending( ppd => ppd.PollingPlaceId).FirstOrDefault().PollingPlaceId + _context.PollingPlaceDates.Count();
                foreach(var ppd in pollingPlaceDates){
                    var tempPpd = ppd;
                    tempPpd.PollingPlaceId = tempPp.PollingPlaceId;
                    tempPpd.PollingDateId = ++pollingDateId;
                    _context.Add(tempPpd);
                    await _context.SaveChangesAsync();
                }
            }
            
            // Copy BallotIssues
            var ballotIssues = _context.BallotIssues.Where(b => b.ElectionId == id);
            var ballotIssuesId = _context.BallotIssues.OrderByDescending( b => b.ElectionId).FirstOrDefault().ElectionId + _context.BallotIssues.Count();
            foreach(var b in ballotIssues){
                var tempB = b;
                var bId = b.BallotIssueId;
                tempB.ElectionId = election.ElectionId;
                tempB.BallotIssueId = ++ballotIssuesId;
                _context.Add(tempB);
                await _context.SaveChangesAsync();

                // IssueOptions
                var issueOptions = _context.IssueOptions.Where(i => i.BallotIssueId == bId);
                var issueOptionsId = _context.IssueOptions.OrderByDescending( i => i.BallotIssueId).FirstOrDefault().BallotIssueId + _context.IssueOptions.Count();
                foreach(var bi in issueOptions){
                    var tempBi = bi;
                    tempBi.BallotIssueId = tempB.BallotIssueId;
                    tempBi.IssueOptionId = ++issueOptionsId;
                    _context.Add(tempBi);
                    await _context.SaveChangesAsync();
                }

            }

            // Copy Social Medias
            var socialMedias = _context.SocialMedias.Where(c => c.ElectionId == id);
            var socialMediasId = _context.SocialMedias.OrderByDescending( sm => sm.ElectionId).FirstOrDefault().ElectionId + _context.SocialMedias.Count();
            foreach(var sm in socialMedias){
                var tempSM = sm;
                tempSM.ID = ++socialMediasId;
                tempSM.ElectionId = election.ElectionId;
                _context.Add(tempSM);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Elections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var election = await _context.Elections.FindAsync(id);
            if (election == null)
            {
                return NotFound();
            }
            return View(election);
        }

        // POST: Elections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ElectionId,ElectionName,StartDate,EndDate,Description")] Election election)
        {
            if (id != election.ElectionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(election);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ElectionExists(election.ElectionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(election);
        }

        // GET: Elections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var election = await _context.Elections
                .FirstOrDefaultAsync(m => m.ElectionId == id);
            if (election == null)
            {
                return NotFound();
            }

            return View(election);
        }

        // POST: Elections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var election = await _context.Elections.FindAsync(id);
            _context.Elections.Remove(election);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ElectionExists(int id)
        {
            return _context.Elections.Any(e => e.ElectionId == id);
        }
    }
}
