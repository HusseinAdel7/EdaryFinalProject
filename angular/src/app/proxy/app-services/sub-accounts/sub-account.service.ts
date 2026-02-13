import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable, inject } from '@angular/core';
import type { CreateSubAccountDto, SubAccountDto, SubAccountPagedRequestDto, UpdateSubAccountDto } from '../../dtos/sub-accounts/models';

@Injectable({
  providedIn: 'root',
})
export class SubAccountService {
  private restService = inject(RestService);
  apiName = 'Default';
  

  create = (input: CreateSubAccountDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, SubAccountDto>({
      method: 'POST',
      url: '/api/app/sub-account',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/sub-account/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, SubAccountDto>({
      method: 'GET',
      url: `/api/app/sub-account/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: SubAccountPagedRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<SubAccountDto>>({
      method: 'GET',
      url: '/api/app/sub-account',
      params: { filter: input.filter, isActive: input.isActive, mainAccountId: input.mainAccountId, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: UpdateSubAccountDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, SubAccountDto>({
      method: 'PUT',
      url: `/api/app/sub-account/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });
}