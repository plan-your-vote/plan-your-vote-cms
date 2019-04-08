# Candidate Local Storage
This document helps explain the functionality, purpose, and implementation of the candidate local storage.

## How to Use
Selected candidates will be saved to and retrieved from local storage as necessary. To view this in action, press f12 in Google Chrome,
navigate to "Application", and expand "Local Storage".

Implementation can be found at 'Spa/selection/selection.component.ts'.

### Consumers
To disable local storage for this application, open selection.component.ts and remove any lines containing 'localStorage.setItem(...)'.

### Developers
If the object model changes, the local storage functionality may need to be updated or re-done to work.