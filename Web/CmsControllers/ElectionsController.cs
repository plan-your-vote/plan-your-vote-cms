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
            var raceId = _context.Races.OrderByDescending( r => r.RaceId).FirstOrDefault().RaceId;
            var i = 1;
            foreach(var r in races){
                var tempRace = r;
                tempRace.RaceId = raceId + i;
                tempRace.ElectionId = election.ElectionId;
                _context.Add(tempRace);
                await _context.SaveChangesAsync();
                i++;
            }

            // Copy Steps
            var steps = _context.Steps.Where(s => s.ElectionId == id);
            var stepsId = _context.Steps.OrderByDescending( s => s.ID).FirstOrDefault().ID;
            foreach(var s in steps){
                var tempS = s;
                tempS.ID = ++stepsId;
                tempS.ElectionId = election.ElectionId;
                _context.Add(tempS);
                await _context.SaveChangesAsync();
            }

            // Copy Candidate
            var candidates = _context.Candidates.Where(c => c.ElectionId == id);
            var candidatesId = _context.Candidates.OrderByDescending( c => c.CandidateId).FirstOrDefault().CandidateId;
            i = 1;
            foreach(var c in candidates){
                var tempCandidate = new Candidate{
                        CandidateId = candidatesId + i,
                        ElectionId = election.ElectionId,
                        Name = c.Name,
                        Picture = "images/default.jpg",
                        OrganizationId = c.OrganizationId
                    };
                _context.Add(tempCandidate);
                await _context.SaveChangesAsync();
                i++;

                // Copy CandidateDetails
                var candidateDetails = _context.CandidateDetails.Where(cd => cd.CandidateId == c.CandidateId);
                var candidateDetailsId = _context.CandidateDetails.OrderByDescending( cd => cd.ID).FirstOrDefault().ID;
                var j = 1;
                foreach(var cd in candidateDetails){
                    var tempCandidateDetails = new CandidateDetail{
                        ID = candidateDetailsId + j,
                        CandidateId = tempCandidate.CandidateId,
                        Title = cd.Title,
                        Text = cd.Text,
                        Format = cd.Format,
                        Lang = cd.Lang
                    };
                    _context.Add(tempCandidateDetails);
                    await _context.SaveChangesAsync();
                    j++;
                }

                // Copy Candidate Contacts
                var candidateContacts = _context.Contacts.Where(con => con.CandidateId == c.CandidateId);
                var candidateContactsId = _context.Contacts.OrderByDescending( con => con.ContactId).FirstOrDefault().ContactId;
                j = 1;
                foreach(var con in candidateContacts){
                    var tempCandidateContacts = new Contact{
                        ContactId = candidateContactsId + j,
                        ContactMethod = con.ContactMethod,
                        ContactValue = con.ContactValue,
                        CandidateId = tempCandidate.CandidateId
                    };
                    _context.Add(tempCandidateContacts);
                    await _context.SaveChangesAsync();
                    j++;
                }

                // Copy Candidate Races
                var newRaces = _context.Races.Where(r => r.ElectionId == election.ElectionId);
                var k = 0;
                foreach(var r in races){
                    var candidateRaces = _context.CandidateRaces.Where(cr => cr.CandidateId == c.CandidateId && cr.RaceId == r.RaceId);
                    var candidateRacesId = _context.CandidateRaces.OrderByDescending( cr => cr.CandidateRaceId).FirstOrDefault().CandidateRaceId;
                    j = 1;
                    foreach(var cr in candidateRaces){
                        var tempCR = new CandidateRace{
                            CandidateRaceId = candidateRacesId + j,
                            CandidateId = tempCandidate.CandidateId,
                            RaceId = newRaces.Skip(k).First().RaceId,
                            BallotOrder = cr.BallotOrder
                        };
                        _context.Add(tempCR);
                        await _context.SaveChangesAsync();
                        j++;
                    }
                    k++;
                }
            }

            // Copy Polling Places
            var pollingPlaces = _context.PollingPlaces.Where(c => c.ElectionId == id);
            var pollingPlacesId = _context.PollingPlaces.OrderByDescending( pp => pp.PollingPlaceId).FirstOrDefault().PollingPlaceId;
            i = 1;
            foreach(var pp in pollingPlaces){
                var tempPp = new PollingPlace{
                    PollingPlaceId = pollingPlacesId + i,
                    ElectionId = election.ElectionId,
                    PollingPlaceName = pp.PollingPlaceName,
                    PollingStationName = pp.PollingStationName,
                    Address = pp.Address,
                    WheelchairInfo = pp.WheelchairInfo,
                    ParkingInfo = pp.ParkingInfo,
                    Latitude = pp.Latitude,
                    Longitude = pp.Longitude,
                    AdvanceOnly = pp.AdvanceOnly,
                    LocalArea = pp.LocalArea,
                    Phone = pp.Phone,
                    Email = pp.Email
                };
                _context.Add(tempPp);
                await _context.SaveChangesAsync();
                i++;

                // Copy PollingPlaceDates
                var pollingPlaceDates = _context.PollingPlaceDates.Where(ppd => ppd.PollingPlaceId == pp.PollingPlaceId);
                var pollingDateId = _context.PollingPlaceDates.OrderByDescending( ppd => ppd.PollingDateId).FirstOrDefault().PollingDateId;
                var j = 1;
                foreach(var ppd in pollingPlaceDates){
                    var tempPpd = new PollingPlaceDate{
                        PollingDateId = pollingDateId + j,
                        PollingPlaceId = tempPp.PollingPlaceId,
                        PollingDate = ppd.PollingDate,
                        StartTime = ppd.StartTime,
                        EndTime = ppd.EndTime
                    };
                    _context.Add(tempPpd);
                    await _context.SaveChangesAsync();
                    j++;
                }
            }
            
            // Copy BallotIssues
            var ballotIssues = _context.BallotIssues.Where(b => b.ElectionId == id);
            var ballotIssuesId = _context.BallotIssues.OrderByDescending( b => b.BallotIssueId).FirstOrDefault().BallotIssueId;
            i = 1;
            foreach(var b in ballotIssues){
                var tempB = new BallotIssue{
                    BallotIssueId = ballotIssuesId + i,
                    ElectionId = election.ElectionId,
                    BallotIssueTitle = b.BallotIssueTitle,
                    Description = b.Description
                };
                _context.Add(tempB);
                await _context.SaveChangesAsync();
                i++;

                // IssueOptions
                var issueOptions = _context.IssueOptions.Where(io => io.BallotIssueId == b.BallotIssueId);
                var issueOptionsId = _context.IssueOptions.OrderByDescending( io => io.IssueOptionId).FirstOrDefault().IssueOptionId;
                var j = 1;
                foreach(var bi in issueOptions){
                    var tempBi = new IssueOption{
                        IssueOptionId = issueOptionsId + j,
                        IssueOptionInfo = bi.IssueOptionInfo,
                        BallotIssueId = tempB.BallotIssueId
                    };
                    _context.Add(tempBi);
                    await _context.SaveChangesAsync();
                    j++;
                }

            }

            // Copy Social Medias
            var socialMedias = _context.SocialMedias.Where(c => c.ElectionId == id);
            var socialMediasId = _context.SocialMedias.OrderByDescending( sm => sm.ID).FirstOrDefault().ID;
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
            // Need to catch in the case of this being the last election, 'nextElection' line will crash
            try{
                var nextElection = _context.Elections.Where(e => e.ElectionId != id).First();
                var newState = _context.StateSingleton.Find(State.STATE_ID);
                newState.RunningElectionID = nextElection.ElectionId;
                newState.ManagedElectionID = nextElection.ElectionId;
                await _context.SaveChangesAsync();

                var election = await _context.Elections.FindAsync(id);
                _context.Elections.Remove(election);
                await _context.SaveChangesAsync();
            } catch {}
            return RedirectToAction(nameof(Index));
        }

        private bool ElectionExists(int id)
        {
            return _context.Elections.Any(e => e.ElectionId == id);
        }
    }
}
