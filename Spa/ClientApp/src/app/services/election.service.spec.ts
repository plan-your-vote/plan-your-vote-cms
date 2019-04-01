import { TestBed } from '@angular/core/testing';
import { HttpClientModule } from '@angular/common/http'; 
import { ElectionService } from './election.service';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

imports: [
  HttpClientModule,
];

describe('ElectionService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  //it('should be created', () => {
  //  const service: ElectionService = TestBed.get(ElectionService);
  //  expect(service).toBeTruthy();
  //});
});
