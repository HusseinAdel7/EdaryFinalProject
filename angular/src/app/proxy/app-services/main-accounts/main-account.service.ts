import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable, inject } from '@angular/core';
import type { ChartOfAccountNodeDto, CreateMainAccountDto, MainAccountDto, MainAccountPagedRequestDto, UpdateMainAccountDto } from '../../dtos/main-accounts/models';

@Injectable({
  providedIn: 'root',
})
export class MainAccountService {
  private restService = inject(RestService);
  apiName = 'Default';
  

  create = (input: CreateMainAccountDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, MainAccountDto>({
      method: 'POST',
      url: '/api/app/main-account',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/main-account/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, MainAccountDto>({
      method: 'GET',
      url: `/api/app/main-account/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getChartOfAccounts = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, ChartOfAccountNodeDto[]>({
      method: 'GET',
      url: '/api/app/main-account/chart-of-accounts',
    },
    { apiName: this.apiName,...config });
  

  getList = (input: MainAccountPagedRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<MainAccountDto>>({
      method: 'GET',
      url: '/api/app/main-account',
      params: { filter: input.filter, isActive: input.isActive, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: UpdateMainAccountDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, MainAccountDto>({
      method: 'PUT',
      url: `/api/app/main-account/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });
}