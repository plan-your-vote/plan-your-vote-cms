| id           | title        | Sider_label  |
| ------------ | ------------ | ------------ |
| localization | Localization | Localization |

The document explains the structure the localization of this project

## How localization works

In previous stage, MVC style of localizaiton was applied, but not completely done. And, it's hard to add another language to it.

As a result, localization has been completely rewritten to use one single shared resources files for each language. 

To add a language

- add configuration on ***startup.cs***
- create a new resource file following the convention `SharedResource.[culture code].resx` 
- copied the <data> tags in other resources files into the new resource file
- Translate the values of <data> </data> <data>` (name, value)` pairs

## Conventions for naming `name` in <data> Tag in Resource File

#### Data Annotation

> keep it the same as the name of a field of a Model, reuse it when it's used as foreign key in another Model

#### Views

>  `viewname_pagename_fieldname`, for example, the *title* of *index* page of *home* view should be 
>
> `home_index_title`

