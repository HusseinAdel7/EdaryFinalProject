import type { FullAuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface CreateWarehouseDto {
  warehouseName: string;
  location?: string | null;
  managerName?: string | null;
  notes?: string | null;
  isActive?: boolean;
  warehouseNameEn?: string | null;
  managerNameEn?: string | null;
}

export interface UpdateWarehouseDto {
  warehouseName: string;
  location?: string | null;
  managerName?: string | null;
  notes?: string | null;
  isActive?: boolean;
  warehouseNameEn?: string | null;
  managerNameEn?: string | null;
}

export interface WarehouseDto extends FullAuditedEntityDto<string> {
  warehouseCode?: string;
  warehouseName?: string;
  location?: string;
  managerName?: string;
  notes?: string;
  isActive?: boolean;
  warehouseNameEn?: string;
  managerNameEn?: string;
  tenantId?: string | null;
}

export interface WarehousePagedRequestDto extends PagedAndSortedResultRequestDto {
  filter?: string | null;
  isActive?: boolean | null;
}
