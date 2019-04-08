// using Microsoft.AspNetCore.Builder;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.Extensions.DependencyInjection;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using VotingModelLibrary.Models;

// namespace Web.Data
// {
//     public static class DummyData
//     {
//         public static async Task Initialize(IApplicationBuilder app)
//         {
//             using (IServiceScope serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
//             {
//                 ApplicationDbContext context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

//                 UserManager<IdentityUser> userManager = serviceScope.ServiceProvider.GetService<UserManager<IdentityUser>>();
//                 RoleManager<IdentityRole> roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();


//                 context.Database.EnsureCreated();

//                 if (context.Candidates.Any()) { return; }

//                 await InsertUserAsync(userManager, roleManager);

//                 var elections = GetElections().ToArray();
//                 context.Elections.AddRange(elections);
//                 context.SaveChanges();

//                 var pollingStations = GetPollingStations().ToArray();
//                 context.PollingStations.AddRange(pollingStations);
//                 context.SaveChanges();

//                 var organizations = GetOrganizations().ToArray();
//                 context.Organizations.AddRange(organizations);
//                 context.SaveChanges();

//                 var races = GetRaces().ToArray();
//                 context.Races.AddRange(races);
//                 context.SaveChanges();

//                 var candidates = GetCandidates().ToArray();
//                 context.Candidates.AddRange(candidates);
//                 context.SaveChanges();

//                 var contacts = GetContacts().ToArray();
//                 context.Contacts.AddRange(contacts);
//                 context.SaveChanges();

//                 var candidateRaces = GetCandidateRaces().ToArray();
//                 context.CandidateRaces.AddRange(candidateRaces);
//                 context.SaveChanges();

//                 var ballotIssues = GetBallotIssues().ToArray();
//                 context.BallotIssues.AddRange(ballotIssues);
//                 context.SaveChanges();

//                 var issueOptions = GetIssueOptions().ToArray();
//                 context.IssueOptions.AddRange(issueOptions);
//                 context.SaveChanges();
//             }
//         }

//         public static async Task InsertUserAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)

//         {

//             var role1 = new IdentityRole
//             {
//                 Name = "Admin",
//                 NormalizedName = "Admin"
//             };

//             var role2 = new IdentityRole
//             {
//                 Name = "Member",
//                 NormalizedName = "Member"
//             };

//             if (await roleManager.FindByNameAsync(role1.Name) == null)
//             {
                
//                 await roleManager.CreateAsync(role1);
//             }
//             if (await roleManager.FindByNameAsync(role2.Name) == null)
//             {
//                 await roleManager.CreateAsync(role2);
//             }

//             var user = new IdentityUser
//             {
//                 Email = "a@a.a",
//                 UserName = "a@a.a",
//                 SecurityStamp = Guid.NewGuid().ToString()
//             };


//             var result = await userManager.CreateAsync(user, "P@$$w0rd");

//             if (result.Succeeded)
//             {
//                 await userManager.AddToRoleAsync(user, "Admin");
//             }

//             var user1 = new IdentityUser
//             {
//                 Email = "m@m.m",
//                 UserName = "m@m.m",
//                 SecurityStamp = Guid.NewGuid().ToString()
//             };

//             // var result = await userManager.CreateAsync(user);

//             var result1 = await userManager.CreateAsync(user1, "P@$$w0rd");

//             if (result.Succeeded)
//             {
//                 await userManager.AddToRoleAsync(user1, "Member");
//             }

//         }

//         private static List<Organization> GetOrganizations()
//         {
//             return new List<Organization>()
//             {
//                 new Organization()
//                 {
//                     Name = "The United People for Change",
//                     Description = "_DESCRIPTION_",
//                 },
//                 new Organization()
//                 {
//                     Name = "The Second Organization",
//                     Description = "_DESCRIPTION_",
//                 },
//                 new Organization()
//                 {
//                     Name = "Coalition Vancouver",
//                     Description = "Coalition is 100% for the people!",
//                 }
//             };
//         }

//         private static List<Race> GetRaces()
//         {
//             return new List<Race>
//             {
//                 new Race
//                 {
//                     PositionName = "Mayor",
//                     NumberNeeded = 1,
//                     ElectionId = 1
//                 },
//                 new Race
//                 {
//                     PositionName = "Councillor",
//                     NumberNeeded = 10,
//                     ElectionId = 1
//                 },
//                 new Race
//                 {
//                     PositionName = "Park Commisioner",
//                     NumberNeeded = 7,
//                     ElectionId = 1
//                 },
//                 new Race
//                 {
//                     PositionName = "School Trustee",
//                     NumberNeeded = 9,
//                     ElectionId = 1
//                 }
//             };
//         }

//         private static List<Candidate> GetCandidates()
//         {
//             return new List<Candidate>
//             {
//                 new Candidate
//                 {
//                     FirstName = "Jason",
//                     LastName= "Lamarche",
//                     Picture = "https://vancouver.ca/plan-your-vote/img/mayor0.jpg",
//                     Biography = @"Jason Lamarche loves Vancouver and has lived here for 21 years. Jason was born in Ottawa, Ontario.

// Jason attended Langara and UBC with eight years experience at TD Bank, HSBC, CIBC. Jason was an executive for Canada's largest retail Cannabis distributor.

// Jason has ten years experience in Municipal, Provincial, Federal politics; earning 37,286 votes in 2011 Vancouver Council election.",
//                     OrganizationId = 1,
//                     ElectionId = 1
//                 },
//                 new Candidate
//                 {
//                     FirstName = "Mike",
//                     LastName = "Hansen",
//                     Picture = "https://vancouver.ca/plan-your-vote/img/mayor1.jpg",
//                     Biography = @"Been in housing construction most of my life, truck driver, stock promoter and various other business. Founded the Canadian Hemp Growers Assoc. in 1996. 2005 on record at Senate Committee on national defence saying, ""terrorists"" are smuggling guns/drugs into Canada. 2005 Harm Reduction Pilot Project/monopolizing heroin to save lives, Men On Down Town East Society and Two Ravens Opioid Project.",
//                     OrganizationId = 2,
//                     ElectionId = 1
//                 },
//                 new Candidate
//                 {
//                     FirstName = "Wai",
//                     LastName = "Young",
//                     Picture = "https://vancouver.ca/plan-your-vote/img/mayor12.jpg",
//                     Biography = @"Wai has lived in Vancouver for over 50 years and is a community advocate, business owner, and past Member of Parliament with over three decades of civic and policy leadership. She is a birth mother of twins and a foster parent to seven children. She holds a degree in Sociology with post graduate work in Urban Planning and Mass Communications.",
//                     OrganizationId = 3,
//                     ElectionId = 1
//                 },
//                 new Candidate
//                 {
//                     FirstName = "Tony",
//                     LastName = "Pacheco",
//                     Picture = "https://vancouver.ca/plan-your-vote/img/mayor9.jpg",
//                     Biography = @"That's not actually a picture of Tony JSYK.",
//                     OrganizationId = 2,
//                     ElectionId = 1
//                 }
//                 ,
//                 new Candidate
//                 {
//                     FirstName = "Danny",
//                     LastName = "Di Iorio",
//                     Picture = "https://vancouver.ca/plan-your-vote/img/mayor9.jpg",
//                     Biography = @"This may ACTUALLY be a picture of Danny; Who can really say.",
//                     OrganizationId = 1,
//                     ElectionId = 1
//                 },
//                 new Candidate
//                 {
//                     FirstName = "STEWART ",
//                     LastName = "Kennedy",
//                     Picture = "https://vancouver.ca/plan-your-vote/img/mayor7.jpg",
//                     Biography = @"I've lived in Vancouver since 1989. I moved from Nova Scotia with only $100 and went on to serve as an MP (2011-2018), elected with Jack Layton's NDP.
//                                 I am an SFU policy professor, specializing in cities and housing and have a Ph.D. from the London School of Economics.
//                                 I'm a renter and live downtown with my wife Jeanette.",
//                     OrganizationId = 2,
//                     ElectionId = 1
//                 },
//                 new Candidate
//                 {
//                     FirstName = "YANO ",
//                     LastName = "John",
//                     Picture = "https://vancouver.ca/plan-your-vote/img/mayor2.jpg",
//                     Biography = @"Born in Niagara Falls, to parents who were born in Vancouver, whose parents were born in Japan. Studied organic chemistry, is a journeyman chef. Environmental and Social Justice Advocate, former HEU shop steward, member of local executive and 2 provincial union committees. Member of federal and provincial NDP riding executives and former BCNDP Provincial Executive Member. Out, Proud, Gay man.",
//                     OrganizationId = 1,
//                     ElectionId = 1
//                 },
//                 new Candidate
//                 {
//                     FirstName = "David",
//                     LastName = "CHEN",
//                     Picture = "https://vancouver.ca/plan-your-vote/img/mayor3.jpg",
//                     Biography = @"I offer a tremendous amount of relevant and technical experience as a father of 3 (two with permanent disabilities), a certified financial planner® with 13 years experience, small business owner, and 25 years experience working with my community as a strata president, a Strathcona PAC chair, a Vancouver Foundation granting DSEF committee member, VP/Treasurer of Beauty Night Society and more.",
//                     OrganizationId = 2,
//                     ElectionId = 1
//                 },
//                 new Candidate
//                 {
//                     FirstName = "SYLVESTER",
//                     LastName = "Shauna",
//                     Picture = "https://vancouver.ca/plan-your-vote/img/mayor4.jpg",
//                     Biography = @"With 30 years of leadership experience and numerous awards, Shauna stands as an independent, principled and pragmatic Mayoral candidate. She has served on the boards of Vancity, MEC and BC Assessment and has worked with cities on housing, transportation, finance, climate change and innovation. Shauna understands Vancouver needs a bridge builder who can deliver affordable housing and financial accountability.",
//                     OrganizationId = 2,
//                     ElectionId = 1
//                 },
//                 new Candidate
//                 {
//                     FirstName = "CASSIDY ",
//                     LastName = "Sean",
//                     Picture = "https://vancouver.ca/plan-your-vote/img/mayor8.jpg",
//                     Biography = @"Born/raised in Vancouver, high school Vancouver College, UBC (Political science/Econ), MBA, Fordham University NYC, visiting MBA University of Washington. I am an FMP GE's renowned finance program. Worked on Bay St. doing government and municipal finance across Canada with RBC DS. Represented Canada on Wall St with CMHC, Acting Trade Commissioner S America in Vancouver, Venture Capital advisor.",
//                     OrganizationId = 1,
//                     ElectionId = 1
//                 },
//                 new Candidate
//                 {
//                     FirstName = "BREMNER",
//                     LastName = "Hector",
//                     Picture = "https://vancouver.ca/plan-your-vote/img/mayor10.jpg",
//                     Biography = @"Hector Bremner is the only candidate with Vancouver City council experience running for Mayor. Councillor Bremner's life and career have been marked by recognizing good ideas, getting things done, and bringing people together. He feels honoured to be able to serve all communities across our great city and promises to citizens that he will get the job done.",
//                     OrganizationId = 1,
//                     ElectionId = 1
//                 },
//                 new Candidate
//                 {
//                     FirstName = "FOGAL",
//                     LastName = "Connie",
//                     Picture = "https://vancouver.ca/plan-your-vote/img/mayor11.jpg",
//                     Biography = @"Connie Fogal, a retired lawyer and widow to Harry Rankin, Vancouver's outspoken alderman, has served as Vancouver Park Board Commissioner, leader of the Canadian Action Party, spokesperson for Citizens Against Gambling Expansion, elected representative to Kitsilano's and Vancouver's Resources Boards, and plaintiff in lawsuits against globalization and the militarization of Nanoose Bay.",
//                     OrganizationId = 1,
//                     ElectionId = 1
//                 },
//                 new Candidate
//                 {
//                     FirstName = "BASRA",
//                     LastName = "Nycki",
//                     Picture = "https://vancouver.ca/plan-your-vote/img/councillor2.jpg",
//                     Biography = @"Nycki has policed in the Downtown Eastside but is presently an articling student completing her requirements to become a lawyer. She has degrees in Communication and Law and has extensive experience working on large projects at the local, national and international level. She volunteers for various organizations and is a community activist therefore is well versed in Vancouver's issues.",
//                     OrganizationId = 2,
//                     ElectionId = 1
//                 },
//                 new Candidate
//                 {
//                     FirstName = "RAUNET",
//                     LastName = "Françoise",
//                     Picture = "https://vancouver.ca/plan-your-vote/img/councillor9.jpg",
//                     Biography = @"I am a public school teacher and life-long advocate for social change. Growing up in Vancouver in the eighties, I was raised in the peace, labour, and environmental activist movements. Now a mom trying to raise two kids in an increasingly unaffordable city, my frustration with decades of government inaction has driven me into politics.",
//                     OrganizationId = 2,
//                     ElectionId = 1
//                 },
//                 new Candidate
//                 {
//                     FirstName = "ROBERTS",
//                     LastName = "Anne",
//                     Picture = "https://vancouver.ca/plan-your-vote/img/councillor15.jpg",
//                     Biography = @"Anne Roberts is a former COPE city councillor and school board trustee. She taught journalism at Langara College after a career as a reporter and radio producer. A long-time community activist and feminist, Anne worked to improve schools, parks, pedestrian safety and neighbourhood shopping in Kensington-Cedar Cottage, where she and her family have lived for the past 40 years.",
//                     OrganizationId = 2,
//                     ElectionId = 1
//                 },
//                 new Candidate
//                 {
//                     FirstName = "BLIGH",
//                     LastName = "Rebecca",
//                     Picture = "https://vancouver.ca/plan-your-vote/img/councillor22.jpg",
//                     Biography = @"Rebecca has spent 20 years working with Vancouver startups & NGOs. Rebecca is a passionate supporter of education and the environment and has championed global leadership development programs. Rebecca, skilled in building trust, is a community activator and volunteers with the Dr. Peter Centre. Rebecca rents in East Vancouver with her wife, Laura, and her two teenagers Holly and Jackson.",
//                     OrganizationId = 2,
//                     ElectionId = 1
//                 },
//                 new Candidate
//                 {
//                     FirstName = "GREWAL",
//                     LastName = "David",
//                     Picture = "https://vancouver.ca/plan-your-vote/img/councillor40.jpg",
//                     Biography = @"An engineer and lifelong Vancouverite, David co-founded Absolute Energy Inc., BC's largest natural gas brokerage firm and served as Chair of a community association where he advocated for greater transparency and engagement with citizens. He is committed to making Vancouver a fiscal, social, and innovation leader where working families and seniors can live and small businesses can thrive.",
//                     OrganizationId = 2,
//                     ElectionId = 1
//                 },
//                 new Candidate
//                 {
//                     FirstName = "LI",
//                     LastName = "Morning",
//                     Picture = "https://vancouver.ca/plan-your-vote/img/councillor59.jpg",
//                     Biography = @"In 2004, I immigrated to Vancouver, graduated with computer major, worked as IT engineer, salesperson, later opened my computer company.

//                                     With $2000, I worked as truck driver, cleaner, mover, computer engineer, etc.

//                                     I tried many small businesses including moving, cleaning, IT, shipping, courier, trading, income tax, furniture, Realtor.

//                                     I concern homelessness problem, housing market, affordable housing crisis, public safety.",
//                     OrganizationId = 2,
//                     ElectionId = 1
//                 },
//                 new Candidate
//                 {
//                     FirstName = "ZARUDINA",
//                     LastName = "Olga",
//                     Picture = "https://vancouver.ca/plan-your-vote/img/park0.jpg",
//                     Biography = @"Olga Zarudina is a licensed REALTOR® and a certified translator.
//                                     Olga holds Bachelor's and Master's Degrees in Linguistics and Translation/Interpretation. 
//                                     She has served as a Director-at-Large on the Board of Directors of the Society of Translators and Interpreters of British Columbia, and is a current Director of the Riley Park Hillcrest Community Centre Association.",
//                     OrganizationId = 3,
//                     ElectionId = 1
//                 },
//                 new Candidate
//                 {
//                     FirstName = "NEMETZ",
//                     LastName = "Steven L",
//                     Picture = "https://vancouver.ca/plan-your-vote/img/park5.jpg",
//                     Biography = @"Steven's roots in the city of Vancouver are deep. Steven is the fourth generation of his family to live in Vancouver. (See nemetzfamily.ca) All his schooling, up through UBC, took place in Vancouver. Steven has a J. D. and an LL.M from Osgoode Hall Law School, and an MBA from Schulich School of Business. Steven is a father and grandfather.",
//                     OrganizationId = 3,
//                     ElectionId = 1
//                 },
//                 new Candidate
//                 {
//                     FirstName = "COUPAR ",
//                     LastName = "John",
//                     Picture = "https://vancouver.ca/plan-your-vote/img/park10.jpg",
//                     Biography = @"Coupar is seeking his 3rd Term as a Park Commissioner and is well known for his successful effort to keep the Bloedel Conservatory open in QE Park. He was a founding director of the Friends of the Bloedel, and since his election in 2011 has been responsible for continuing efforts to restore and promote the iconic Bloedel Conservatory.",
//                     OrganizationId = 3,
//                     ElectionId = 1
//                 },
//                 new Candidate
//                 {
//                     FirstName = "MCGARRIGLE ",
//                     LastName = "Kathy",
//                     Picture = "https://vancouver.ca/plan-your-vote/img/park11.jpg",
//                     Biography = @"Kathy McGarrigle is a proud resident of Vancouver, respected for her longtime commitment to serving others. She brings her deep knowledge of the city and extensive business experience and is a voice for positive change. She has a passion for making a difference in the community and lends her time to issues related to poverty, women, children, and seniors.",
//                     OrganizationId = 3,
//                     ElectionId = 1
//                 },
//                 new Candidate
//                 {
//                     FirstName = "MACKINNON",
//                     LastName = "Stuart",
//                     Picture = "https://vancouver.ca/plan-your-vote/img/park31.jpg",
//                     Biography = @"Stuart Mackinnon is currently the Chair of the Park Board. Stuart was first elected a Park Board Commissioner with the Green Party from 2008-2011. He teaches and is currently the Special Education Department Head and School Based Resource Teacher at Killarney Secondary in Vancouver. Stuart lives, works, and shops in the Champlain/Fraserlands neighbourhood of Vancouver.",
//                     OrganizationId = 3,
//                     ElectionId = 1
//                 },


//             };
//         }

//         private static List<Contact> GetContacts()
//         {
//             return new List<Contact>
//             {
//                 new Contact()
//                 {
//                     CandidateId = 1,
//                     ContactMethod = "Tel",
//                 },
//                 new Contact()
//                 {
//                     CandidateId = 1,
//                     ContactMethod = "Email",
//                     ContactValue = "MAYORLAMARCHE@protonmail.com",
//                 },
//                 new Contact()
//                 {
//                     CandidateId = 2,
//                     ContactMethod = "Tel",
//                     ContactValue = "604 700 1652",
//                 },
//                 new Contact()
//                 {
//                     CandidateId = 2,
//                     ContactMethod = "Email",
//                     ContactValue = "mikec.hansen@gmail.com",
//                 },
//                 new Contact()
//                 {
//                     CandidateId = 3,
//                     ContactMethod = "Tel",
//                     ContactValue = "555-555-5555"
//                 },
//                 new Contact()
//                 {
//                     CandidateId = 3,
//                     ContactMethod = "Email",
//                     ContactValue = "wai@young.ca"
//                 },
//                 new Contact()
//                 {
//                     CandidateId = 4,
//                     ContactMethod = "Tel",
//                     ContactValue = "222-222-5252"
//                 },
//                 new Contact()
//                 {
//                     CandidateId = 5,
//                     ContactMethod = "Email",
//                     ContactValue = "dd@dd.dd"
//                 }
//             };
//         }

//         private static List<CandidateRace> GetCandidateRaces()
//         {
//             return new List<CandidateRace>()
//             {
//                 new CandidateRace()
//                 {
//                     RaceId = 1,
//                     CandidateId = 1,
//                     PositionName = "Mayor",
//                     PlatformInfo = @"If Jason Lamarche is elected Mayor of Vancouver he will use his Democratic support and Executive Powers to impose strict Rent Control with select 1 bedroom rents capped at $500 per month.

// Jason will launch new Illegal Immigration Control Enforcement teams managed by Vancouver Police Department.

// Jason will help Canadian citizens start, run, and expand small businesses and other development projects.

// Jason is a true independent and will not accept donations from voters or businesses.",
//                     TopIssues = @"RENT CONTROL 1 BR $500
// ILLEGAL MIGRANT CONTROL
// //PRO SMALL CANADA BUSINESS",
//                 },
//                 new CandidateRace()
//                 {
//                     RaceId = 1,
//                     CandidateId = 3, 
//                     PositionName = "Mayor",
//                     PlatformInfo = @"Wai Young and Coalition Vancouver are 100% for the People. With over 35 years of policy experience working with community, governments & business she is ready to lead. She is proven, experienced, and inclusive. As a former MP who brought many important infrastructure projects to the city, she understands the comprehensive issues facing Vancouver. She will bring back mutual respect on the streets and clean up our city."
//                 },
//                 new CandidateRace()
//                 {
//                     RaceId = 1,
//                     CandidateId = 4,
//                     PositionName = "Mayor",
//                     PlatformInfo = @"If elected, Tony promises change for everyone. And not just small change, but like, Loonies and Toonies and stuff."
//                 },
//                 new CandidateRace()
//                 {
//                     RaceId = 1,
//                     CandidateId = 5,
//                     PositionName = "Mayor",
//                     PlatformInfo = @"If elected, Danny will have made sure that Tony could not consolidate power for himself."
//                 }

//             };
//         }

//         private static List<BallotIssue> GetBallotIssues()
//         {
//             return new List<BallotIssue>()
//             {
//                 new BallotIssue()
//                 {
//                     BallotIssueTitle = "TRANSPORTATION AND TECHNOLOGY",
//                     Description = @"This question seeks authority to borrow funds to be used in carrying out the basic capital works program with respect to transportation and technology.

// Are you in favour of Council having the authority, without further assent of the electors, to pass bylaws between January 1, 2019, and December 31, 2022, to borrow an aggregate $100,353,000 for the following purposes?",
//                     ElectionId = 1
//                 },
//                 new BallotIssue()
//                 {
//                     BallotIssueTitle = "CAPITAL MAINTENANCE AND RENOVATION PROGRAMS FOR EXISTING COMMUNITY FACILITIES, CIVIC FACILITIES, AND PARKS",
//                     Description = @"This question seeks authority to borrow funds to be used in carrying out the basic capital works program with respect to capital maintenance and renovation programs for existing community facilities, civic facilities, and parks.

// Are you in favour of Council having the authority, without further assent of the electors, to pass bylaws between January 1, 2019, and December 31, 2022, to borrow an aggregate $99,557,000 for the following purposes?",
//                     ElectionId = 1
//                 },
//             };
//         }

//         private static List<IssueOption> GetIssueOptions()
//         {
//             return new List<IssueOption>()
//             {
//                 new IssueOption()
//                 {
//                     BallotIssueId = 1,
//                     IssueOptionTitle = "How you plan to answer Question 1. Transportation and technology",
//                     IssueOptionInfo = "Yes",
//                 },
//                 new IssueOption()
//                 {
//                     BallotIssueId = 1,
//                     IssueOptionTitle = "How you plan to answer Question 1. Transportation and technology",
//                     IssueOptionInfo = "No",
//                 },
//                 new IssueOption()
//                 {
//                     BallotIssueId = 2,
//                     IssueOptionTitle = "How you plan to answer Question 2. Capital maintenance and renovation programs for existing community facilities, civic facilities, and parks",
//                     IssueOptionInfo = "Yes",
//                 },
//                 new IssueOption()
//                 {
//                     BallotIssueId = 2,
//                     IssueOptionTitle = "How you plan to answer Question 2. Capital maintenance and renovation programs for existing community facilities, civic facilities, and parks",
//                     IssueOptionInfo = "No",
//                 },
//             };
//         }

//         private static List<Election> GetElections()
//         {
//             return new List<Election>()
//             {
//                 new Election()
//                 {
//                     Name = "City of Vancouver 2018 Municiple Election",
//                     DateEnd = "October 21 2018",
//                     DateStart = "October 21 2018",
//                     Description = "Voting in an election is one of the most important things a citizen can do in their community and country."
//                 },
//                 new Election()
//                 {
//                     Name = "Canadian Federal Election, 2019",
//                     DateEnd = "October 21 2019",
//                     DateStart = "October 21 2019",
//                     Description = "The 2019 Canadian federal election is scheduled to take place on or before October 21, 2019. The October 21 date of the vote is determined by the fixed-date procedures in the Canada Elections Act"
//                 },
//                 new Election()
//                 {
//                     Name = "City of Vancouver 2020 Municiple Election",
//                     DateEnd = "October 21 2020",
//                     DateStart = "October 21 2020",
//                     Description = "Voting in an election is one of the most important things a citizen can do in their community and country."
//                 }
//             };
//         }

//         private static List<PollingStation> GetPollingStations()
//         {
//             return new List<PollingStation>()
//             {
//                 new PollingStation()
//                 {
                   
//                     ElectionId = 1,
//                     Name = "Holy Trinity Anglican Church",
//                     AdditionalInfo = "",
//                     Latitude = 49.27376,
//                     Longitute = -123.127989,
//                     Address = "1440 W 12th Avenue",
//                     WheelchairInfo = "main entrance on West 12th Ave",
//                     ParkingInfo = "street and church parking lot",
//                     WashroomInfo = "",
//                     GeneralAccessInfo = ""
//                 },
//                 new PollingStation()
//                 {
                    
//                     ElectionId = 1,
//                     Name = "Grace Vancouver Church",
//                     AdditionalInfo = "",
//                     Latitude = 48.888,
//                     Longitute = -121.00,
//                     Address = "1696 W 7th Avenue",
//                     WheelchairInfo = "via ramp at the side entrance to the sanctuary, on West 7th Ave",
//                     ParkingInfo = "street",
//                     WashroomInfo = "",
//                     GeneralAccessInfo = ""
//                 },
//                 new PollingStation()
//                 {
                   
//                     ElectionId = 2,
//                     Name = "Lord Tennyson Elementary School",
//                     AdditionalInfo = "",
//                     Latitude = 49.4444,
//                     Longitute = -122.555,
//                     Address = "1936 W 10th Avenue",
//                     WheelchairInfo = "south side entrance on West 11th Ave",
//                     ParkingInfo = "street",
//                     WashroomInfo = "wheelchair accessible washrooms are on the 3rd floor via chair lift",
//                     GeneralAccessInfo = ""
//                 },
//                 new PollingStation()
//                 {
                  
//                     ElectionId = 2,
//                     Name = "Gathering Place Community Centre",
//                     AdditionalInfo = "Handicap parking unavailable",
//                     Latitude = 50.110,
//                     Longitute = -123.444,
//                     Address = "609 Helmcken St",
//                     WheelchairInfo = "main entrance at the corner of Helmcken St and Seymour St",
//                     ParkingInfo = "street",
//                     WashroomInfo = "",
//                     GeneralAccessInfo = ""
//                 },
//                 new PollingStation()
//                 {
                 
//                     ElectionId = 3,
//                     Name = "False Creek Community Centre",
//                     AdditionalInfo = "",
//                     Latitude = 49.5677,
//                     Longitute = -121.127989,
//                     Address = "1318 Cartwright St",
//                     WheelchairInfo = "main entrance on Cartwright St",
//                     ParkingInfo = "street and community centre parking",
//                     WashroomInfo = "",
//                     GeneralAccessInfo = ""
//                 },
//                 new PollingStation()
//                 {
                   
//                     ElectionId = 3,
//                     Name = "Roundhouse Community Arts Centre",
//                     AdditionalInfo = "",
//                     Latitude = 49.6999,
//                     Longitute = -124.127989,
//                     Address = "181 Roundhouse Mews",
//                     WheelchairInfo = "main entrance on Roundhouse Mews",
//                     ParkingInfo = "street parking, underground parking off Drake St",
//                     WashroomInfo = "",
//                     GeneralAccessInfo = ""
//                 }
//             };
//         }
//     }
// }