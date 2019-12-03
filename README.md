# OpenVoting
Open Voting Tool

## Table of Contents
- [What is Open Voting Tool?](#what-is-open-voting-tool)
- [Feature Documents](#feature-documents)
- [Branch Structure](#branch-structure)
- [Links for Live Sites](#links-for-live-sites)

## What is Open Voting Tool
Open Voting Tool is a open source project created by Medhat & BCIT CST Information Systems students. Originally City of Vancouver has an app https://vancouver.ca/plan-your-vote/index.aspx, but the app was built on jQuery with HTML & CSS, without any modern web frameworks.

The purpose of this project is to develop a web app look like the one that City of Vancouver currently has, but can be used by anyone using CMS. The application must be used and implemented by anyone and any organization

## Feature Documents
- [PDF Generator](./doc/PDF.md)
- [Database Config](./doc/DBConfig.md)
- [Accessibility](./doc/Accessibility.md)
- [Docker](./doc/Dockerization.md)
- [Unit Tests](./doc/unit-test.md)


## Branch Structure
Developers must not push their changes directly to `master`. They can push currently to `develop`, but we recommend protect `develop` so that they cannot push directly. We do not have naming conventions for branches as well, but recommend setup conventions.

`master` - the current live sites are automatically deployed from master whenever a change is made. This should be carefully maintained and only have tested code pushed to it

`develop` - currently develop branch is not protected from the direct pushing, but we recommend protect this branch as well. We would recommend setup testing live sites as well on develop, so that feature owners can merge their changes develop and do the testing right away.

`user branches` - branch off of develop, keep to small contained features

## Links for Live Sites
- [Front end single page app](http://pyvspa.azurewebsites.net/)
- [Backend CMS](https://pyv.azurewebsites.net/)
