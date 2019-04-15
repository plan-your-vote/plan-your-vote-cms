# Localization on the front end
This document helps you to set resource variables on `json` files by different components.

## Table of Contents
- [Translate Service](#translate-service)
- [How to Use](#how-to-use)
    - [Consumers](#consumers)
    - [Developers](#developers)

## Angular Library
Translate Service files are [this one](../Spa/ClientApp/src/app/services/translate.service.ts) and [this one](../Spa/ClientApp/src/app/pipes/translate.pipe.ts).

## How to Use
It's very simple!
### Consumers
1. I guess you only need to translate the texts.

### Developers
1. Make sure you have a component you want to add.
2. If you want to add new languages, you need to add language code on [this one](../Spa/ClientApp/src/app/services/translate.service.ts), and json file in [this folder](../Spa/ClientApp/src/assets/i18n/)
3. Let's say your component is `event.component.ts`.
4. Add `EVENT` key in [this file if you are English speaker](../Spa/ClientApp/src/assets/i18n/en.json). This is also a naming convention. If you're component's name is `event`, your key for texts should be `EVENT`. The following is the sample, and of course, key should be capitalized.
```
...
"EVENT": {
    "TITLE": "Event",
    "DESCRIPTION": "This is the event."
},
...
```
5. Then add texts using meaningful key.
6. All good!
