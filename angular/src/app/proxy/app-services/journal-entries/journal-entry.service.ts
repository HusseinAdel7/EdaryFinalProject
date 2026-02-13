import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable, inject } from '@angular/core';
import type { CreateJournalEntryDto, GetJournalEntryListInput, JournalEntryDto, UpdateJournalEntryDto } from '../../dtos/journal-entries/models';

@Injectable({
  providedIn: 'root',
})
export class JournalEntryService {
  private restService = inject(RestService);
  apiName = 'Default';
  

  create = (input: CreateJournalEntryDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, JournalEntryDto>({
      method: 'POST',
      url: '/api/app/journal-entry',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/journal-entry/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, JournalEntryDto>({
      method: 'GET',
      url: `/api/app/journal-entry/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: GetJournalEntryListInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<JournalEntryDto>>({
      method: 'GET',
      url: '/api/app/journal-entry',
      params: { filter: input.filter, currency: input.currency, notes: input.notes, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: UpdateJournalEntryDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, JournalEntryDto>({
      method: 'PUT',
      url: `/api/app/journal-entry/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });
}