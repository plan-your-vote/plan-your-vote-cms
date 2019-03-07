# Localization
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
2. Let's say your component is `event.component.ts`.
3. Add `EVENT` key in [this file if you are English speaker](../Spa/ClientApp/src/assets/i18n/en.json).
4. Then add texts using meaningful key.
4. All good!