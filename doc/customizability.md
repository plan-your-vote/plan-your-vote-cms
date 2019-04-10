# Customizability

Customizability was intended to allow system administrators to select one of the predefined templates. 

The scope and requirements given were:

`Use Case`

`As a system administrator, I want to be able to change to a professional theme via dropdown without knowing any HTML, CSS, JS, etc.`

It is up to the developers of this application to have predefined templates. If the system administrator is a `developer`, they could extend the application as they wished without much trouble.

Aside: `developer` in this instance is someone who knows component-based frameworks (e.g. an Angular developer) and is comfortable with editing the code directly.

On the back-end, `Customizability` is essentially a bunch of strings (data in the Theme and Image tables) associated to a 'theme' (a string). That theme string is an agreed upon contract that the front-end uses to determine which css file (which is on front-end) to render (e.g. 'Maple' is the PoC theme template). 

Notes: 

- We left the styling unfinished due to running out of time but the PoC (proof-of-concept) is completed. 
- [Aside] I completely just ripped the home page off their website because we were running out of time: https://vancouver.ca/plan-your-vote/index.aspx

## Table of Contents

- [List of Files](#list-of-files)
- [Future Steps](#future-steps)

## List of Files

### Front-End

All files for this function are under `Spa/ClientApp/src/`.

#### Static Resource Files

- `assets/css/`: Contains CSS files to use for the entire SPA

#### Function Files

- `index.html`
  - `<link id="theme" href="" rel="stylesheet">`
- `app/app.component.ts`
- `app/services/theme.service.ts`

### Back-End

`Themes` is implement using the following files:

- M
  - Backend
    - VotingModelLibrary\Models\Theme
    - VotingModelLibrary\Models\Image
- V
  - Backend
    - votingtool\Web\Views\Images\
    - votingtool\Web\Views\Themes\
- C
  - Backend controller (For Backend / CMS)
    - votingtool\Web\CmsControllers\ThemesController
    - votingtool\Web\CmsControllers\ImagesController
  - Frontend controller (REST API)
    - votingtool\Web\ApiControllers\ThemesController

DummyData is generated using 

- Web\Data\ThemesInit.cs

Notes:

- ApplicationDbContext was modified
  - Images need a composite key (See comment in Images model class)
- Backend only stores image links, not images

## Future Steps

- Known issue: Need to refresh after initialization to render logo images
  - Need to make components initialized after receiving logo image URLs
- Suggestion: Making this function work with SVG images might be a good idea for front-end performance

### People who worked on this feature

- Ryan Liang 
  - ryalia888@gmail.com
- Amy Hong