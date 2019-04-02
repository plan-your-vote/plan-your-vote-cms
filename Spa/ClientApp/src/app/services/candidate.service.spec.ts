// import { TestBed } from '@angular/core/testing';
// // import { HttpClientTestingModule } from '@angular/common/http/testing';
// import { CandidateService } from './candidate.service';
// import { HttpErrorResponse } from '@angular/common/http';

// describe('CandidateService', () => {
//   beforeEach(() => {
//       TestBed.configureTestingModule({
//           imports: [HttpClientTestingModule],
//           providers: [CandidateService],
//       });
//   });
// });

import {
  HttpClientTestingModule,
  HttpTestingController,
} from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';

import { CandidateService } from './candidate.service';

describe('CandidateService', () => {
  let service: CandidateService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
      TestBed.configureTestingModule({
          imports: [HttpClientTestingModule],
          providers: [CandidateService],
      });

      // inject the service
      service = TestBed.get(CandidateService);
      httpMock = TestBed.get(HttpTestingController);
  });

it('Should get the correct first Name of id 1', () => {
  service.getSingle(1).subscribe((data: any) => {
    expect(data.firstName).toBe('Jason');
  });

  const req = httpMock.expectOne(`https://localhost:5001/api/candidates/1`, 'call to api');
  expect(req.request.method).toBe('GET');

  req.flush({
    firstName: 'Jason'
  });

  httpMock.verify();
});



it('Should get the correct id 2', () => {
  service.getSingle(2).subscribe((data: any) => {
    expect(data.candidateId).toBe(2);
  });

  const req = httpMock.expectOne(`https://localhost:5001/api/candidates/2`, 'call to api');
  expect(req.request.method).toBe('GET');

  req.flush({
    candidateId: 2
  });

  httpMock.verify();
});

it('Should get the correct first Name of id 3', () => {
  service.getSingle(3).subscribe((data: any) => {
    expect(data.firstName).toBe('Wai');
  });

  const req = httpMock.expectOne(`https://localhost:5001/api/candidates/3`, 'call to api');
  expect(req.request.method).toBe('GET');

  req.flush({
    firstName: 'Wai'
  });

  httpMock.verify();
});
});


