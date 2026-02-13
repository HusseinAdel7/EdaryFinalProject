import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable, inject } from '@angular/core';
import type { CreateItemDto, ItemDto, ItemPagedRequestDto, UpdateItemDto } from '../../dtos/items/models';

@Injectable({
  providedIn: 'root',
})
export class ItemService {
  private restService = inject(RestService);
  apiName = 'Default';
  

  create = (input: CreateItemDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ItemDto>({
      method: 'POST',
      url: '/api/app/item',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/item/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ItemDto>({
      method: 'GET',
      url: `/api/app/item/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: ItemPagedRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<ItemDto>>({
      method: 'GET',
      url: '/api/app/item',
      params: { filter: input.filter, isActive: input.isActive, groupName: input.groupName, itemType: input.itemType, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: UpdateItemDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ItemDto>({
      method: 'PUT',
      url: `/api/app/item/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });
}